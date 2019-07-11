using Stock.Model;

namespace Stock.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Stock.DataAccess.StockDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(StockDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.Items.AddOrUpdate(
                i => i.Id,
                    new Item { Id = 1, VenderId = 1, Name = "Item 1" },
                    new Item { Id = 2, VenderId = 2, Name = "Item 2" },
                    new Item { Id = 3, VenderId = 3, Name = "Item 3" },
                    new Item { Id = 4, VenderId = 4, Name = "Item 4" },
                    new Item { Id = 5, VenderId = 5, Name = "Item 5" },
                    new Item { Id = 6, VenderId = 6, Name = "Item 6" },
                    new Item { Id = 7, VenderId = 7, Name = "Item 7" },
                    new Item { Id = 8, VenderId = 8, Name = "Item 8" },
                    new Item { Id = 9, VenderId = 9, Name = "Item 9" },
                    new Item { Id = 10, VenderId = 10, Name = "Item 10" },
                    new Item { Id = 11, VenderId = 11, Name = "Item 11" },
                    new Item { Id = 12, VenderId = 12, Name = "Item 12" },
                    new Item { Id = 13, VenderId = 13, Name = "Item 13" },
                    new Item { Id = 14, VenderId = 14, Name = "Item 14" },
                    new Item { Id = 15, VenderId = 15, Name = "Item 15" });

            context.Venders.AddOrUpdate(
                v => v.Id,
                    new Vender { Id = 1, Name = "Vender 1", Address = "Address 1" },
                    new Vender { Id = 2, Name = "Vender 2", Address = "Address 2" },
                    new Vender { Id = 3, Name = "Vender 3", Address = "Address 3" },
                    new Vender { Id = 4, Name = "Vender 4", Address = "Address 4" },
                    new Vender { Id = 5, Name = "Vender 5", Address = "Address 5" },
                    new Vender { Id = 6, Name = "Vender 6", Address = "Address 6" },
                    new Vender { Id = 7, Name = "Vender 7", Address = "Address 7" },
                    new Vender { Id = 8, Name = "Vender 8", Address = "Address 8" },
                    new Vender { Id = 9, Name = "Vender 9", Address = "Address 9" },
                    new Vender { Id = 10, Name = "Vender 10", Address = "Address 10" },
                    new Vender { Id = 11, Name = "Vender 11", Address = "Address 11" },
                    new Vender { Id = 12, Name = "Vender 12", Address = "Address 12" },
                    new Vender { Id = 13, Name = "Vender 13", Address = "Address 13" },
                    new Vender { Id = 14, Name = "Vender 14", Address = "Address 14" },
                    new Vender { Id = 15, Name = "Vender 15", Address = "Address 15" });

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
