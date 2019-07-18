using System.Collections.Generic;
using System.Data;
using Stock.Model;
using Stock.UI.ViewModels;

namespace Stock.UI.Converters
{
    public static class TableConverter
    {
        public static IEnumerable<Vender> DataTableToList(this ViewModelBase vm, DataTable dt)
        {
            var modelList = new List<Vender>();
            var rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                for (var n = 0; n < rowsCount; n++)
                {
                    var model = new Vender
                    {
                        Id = (int)dt.Rows[n]["Id"],
                        Name = dt.Rows[n]["Name"].ToString(),
                        Address = dt.Rows[n]["Address"].ToString(),
                        Phone = dt.Rows[n]["Phone"].ToString(),
                        Email = dt.Rows[n]["Email"].ToString(),
                    };

                    modelList.Add(model);
                }
            }

            return modelList;
        }
    }
}
