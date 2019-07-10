using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.Model
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }

        [ForeignKey(nameof(Vender))]
        public int VenderId { get; set; }
        public Vender Vender { get; set; }
    }
}
