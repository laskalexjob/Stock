using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows.Input;
using Stock.DataAccess;
using Stock.DataAccess.Repositories;
using Stock.Model;
using Stock.UI.Converters;
using Stock.UI.Services.Interfaces;
using Stock.UI.Utils;

namespace Stock.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private IVenderDataService _venderDataService;

        private string _titleLable;
        public string TitleLable
        {
            get => _titleLable;
            set
            {
                _titleLable = value;
                OnPropertyChanged(nameof(TitleLable));
            }
        }

        private DataView _venderDataView;
        public DataView VenderDataView
        {
            get => _venderDataView;
            set
            {
                _venderDataView = value;
                _venderDataView.Table.AcceptChanges();
                OnPropertyChanged(nameof(VenderDataView));
            }
        }

        private List<int> _recordsToShow = new List<int> { };
        public List<int> RecordsToShow
        {
            get => _recordsToShow;
            set
            {
                _recordsToShow = value;
                OnPropertyChanged(nameof(RecordsToShow));
            }
        }

        private int _numberOfRecords;
        public int NumberOfRecords
        {
            get => _numberOfRecords;
            set
            {
                _numberOfRecords = value;

                VenderDataView = PagedTable.First(Venders, value).DefaultView;

                TitleLable = PageNumberDisplay();

                OnPropertyChanged(nameof(NumberOfRecords));
            }
        }

        private ObservableCollection<Vender> _venders;
        public ObservableCollection<Vender> Venders
        {
            get => _venders;
            set
            {
                _venders = value;
                OnPropertyChanged(nameof(_venders));
            }
        }

        private static readonly Paging.Paging PagedTable = new Paging.Paging();
        private static readonly EfGenericRepository<Vender> VenderRepo = new EfGenericRepository<Vender>(new StockDbContext());

        public MainViewModel()
        {
            CommandNext = new RelayCommand(NextButton_Click);
            CommandBack = new RelayCommand(PreviousButton_Click);
            CommandFirst = new RelayCommand(FirstButton_Click);
            CommandLast = new RelayCommand(LastButton_Click);
            CommandSaveChanges = new RelayCommand(SaveChanges_Click);

            Venders = new ObservableCollection<Vender>();

            PopulateVenders(VenderRepo.GetWithInclude(x => x.Items).ToList(), Venders);

            PagedTable.PageIndex = 0; //Sets the Initial Index to a default value

            RecordsToShow = new List<int> { 5, 10, 20, 30, 50 }; //This Array can be any number of groups

            NumberOfRecords = 10; //Initialize the ComboBox
        }

        public void PopulateVenders(IEnumerable<Vender> items, ObservableCollection<Vender> venders)
        {
            venders.Clear();

            foreach (var item in items)
            {
                venders.Add(item);
            }
        }

        public string PageNumberDisplay()
        {
            var pagedNumber = NumberOfRecords * (PagedTable.PageIndex + 1);
            if (pagedNumber > Venders.Count)
            {
                pagedNumber = Venders.Count;
            }

            return "Showing " + pagedNumber + " of " + Venders.Count;
        }

        public ICommand CommandNext { get; }

        private void NextButton_Click(object sender)
        {
            VenderDataView = PagedTable.Next(Venders, NumberOfRecords).DefaultView;
            TitleLable = PageNumberDisplay();
        }

        public ICommand CommandBack { get; }

        private void PreviousButton_Click(object sender)
        {
            VenderDataView = PagedTable.Previous(Venders, NumberOfRecords).DefaultView;
            TitleLable = PageNumberDisplay();
        }

        public ICommand CommandFirst { get; }

        private void FirstButton_Click(object sender)
        {
            VenderDataView = PagedTable.First(Venders, NumberOfRecords).DefaultView;
            TitleLable = PageNumberDisplay();
        }

        public ICommand CommandLast { get; }

        private void LastButton_Click(object sender)
        {
            VenderDataView = PagedTable.Last(Venders, NumberOfRecords).DefaultView;
            TitleLable = PageNumberDisplay();
        }

        public ICommand CommandSaveChanges { get; }

        private void SaveChanges_Click(object sender)
        {
            var changedRows = VenderDataView.Table.GetChanges();

            if (changedRows != null)
            {
                var venders = this.DataTableToList(changedRows);

                VenderRepo.UpdateAll(venders);

                VenderDataView.Table.AcceptChanges();

                Venders.Clear();

                PopulateVenders(VenderRepo.GetWithInclude(x => x.Items).ToList(), Venders);
            }
        }
    }
}
