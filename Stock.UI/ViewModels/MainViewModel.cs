using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Stock.DataAccess;
using Stock.DataAccess.Repositories;
using Stock.Model;
using Stock.UI.Utils;

namespace Stock.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
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

        private DataView _dataVenderTable;
        public DataView DataVenderTable
        {
            get => _dataVenderTable;
            set
            {
                _dataVenderTable = value;
                OnPropertyChanged(nameof(DataVenderTable));
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

                DataVenderTable = PagedTable.First(_myList, value).DefaultView;

                TitleLable = PageNumberDisplay();

                OnPropertyChanged(nameof(NumberOfRecords));
            }
        }

        public ICommand CommandNext { get; }
        public ICommand CommandBack { get; }
        public ICommand CommandFirst { get; }
        public ICommand CommandLast { get; }

        public ObservableCollection<Vender> Venders { get; set; }

        private static readonly Paging.Paging PagedTable = new Paging.Paging();
        private static readonly EFGenericRepository<Vender> VenderRepo = new EFGenericRepository<Vender>(new StockDbContext());

        private readonly IList<Vender> _myList = VenderRepo.GetWithInclude(x => x.Items).ToList();

        public MainViewModel()
        {
            CommandNext = new CustomCommand(NextButton_Click);
            CommandBack = new CustomCommand(PreviousButton_Click);
            CommandFirst = new CustomCommand(FirstButton_Click);
            CommandLast = new CustomCommand(LastButton_Click);

            PagedTable.PageIndex = 0; //Sets the Initial Index to a default value

            RecordsToShow = new List<int> { 5, 10, 20, 30, 50, 100 }; //This Array can be any number of groups

            NumberOfRecords = 10; //Initialize the ComboBox
        }

        public string PageNumberDisplay()
        {
            var pagedNumber = NumberOfRecords * (PagedTable.PageIndex + 1);
            if (pagedNumber > _myList.Count)
            {
                pagedNumber = _myList.Count;
            }
            return "Showing " + pagedNumber + " of " + _myList.Count; //This dramatically
                                                                      //reduced the number of times I had to write this string statement
        }

        private void NextButton_Click(object sender)
        {
            DataVenderTable = PagedTable.Next(_myList, NumberOfRecords).DefaultView;
            TitleLable = PageNumberDisplay();
        }

        private void PreviousButton_Click(object sender)
        {
            DataVenderTable = PagedTable.Previous(_myList, NumberOfRecords).DefaultView;
            TitleLable = PageNumberDisplay();
        }

        private void FirstButton_Click(object sender)
        {
            DataVenderTable = PagedTable.First(_myList, NumberOfRecords).DefaultView;
            TitleLable = PageNumberDisplay();
        }

        private void LastButton_Click(object sender)
        {
            DataVenderTable = PagedTable.Last(_myList, NumberOfRecords).DefaultView;
            TitleLable = PageNumberDisplay();
        }

        private void NumberOfRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataVenderTable = PagedTable.First(_myList, NumberOfRecords).DefaultView;
            TitleLable = PageNumberDisplay();
        }
    }
}
