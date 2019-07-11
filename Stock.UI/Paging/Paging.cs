using System.Collections.Generic;
using System.Data;
using System.Linq;
using Stock.Model;

namespace Stock.UI.Paging
{
    public class Paging
    {
        private DataTable _pagedList = new DataTable();
        public int PageIndex { get; set; }

        public DataTable SetPaging(IList<Vender> listToPage, int recordsPerPage)
        {
            var pageGroup = PageIndex * recordsPerPage;

            IList<Vender> pagedList = listToPage.Skip(pageGroup).Take(recordsPerPage).ToList();
            var finalPaging = PagedTable(pagedList);

            return finalPaging;
        }

        private DataTable PagedTable<T>(IList<T> sourceList)
        {
            var columnType = typeof(T);
            var tableToReturn = new DataTable();

            foreach (var column in columnType.GetProperties())
            {
                tableToReturn.Columns.Add(column.Name, column.PropertyType);
            }

            foreach (object item in sourceList)
            {
                var returnTableRow = tableToReturn.NewRow();
                foreach (var column in columnType.GetProperties())
                {
                    returnTableRow[column.Name] = column.GetValue(item);
                }
                tableToReturn.Rows.Add(returnTableRow);
            }

            return tableToReturn;
        }

        public DataTable Next(IList<Vender> listToPage, int recordsPerPage)
        {
            PageIndex++;
            if (PageIndex >= listToPage.Count / recordsPerPage)
            {
                PageIndex = listToPage.Count / recordsPerPage;
            }
            _pagedList = SetPaging(listToPage, recordsPerPage);

            return _pagedList;
        }

        public DataTable Previous(IList<Vender> listToPage, int recordsPerPage)
        {
            PageIndex--;
            if (PageIndex <= 0)
            {
                PageIndex = 0;
            }
            _pagedList = SetPaging(listToPage, recordsPerPage);

            return _pagedList;
        }

        public DataTable First(IList<Vender> listToPage, int recordsPerPage)
        {
            PageIndex = 0;
            _pagedList = SetPaging(listToPage, recordsPerPage);

            return _pagedList;
        }

        public DataTable Last(IList<Vender> listToPage, int recordsPerPage)
        {
            PageIndex = listToPage.Count / recordsPerPage;
            _pagedList = SetPaging(listToPage, recordsPerPage);

            return _pagedList;
        }
    }
}
