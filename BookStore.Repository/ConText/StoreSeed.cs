using BookStore.Core.Entities.Books;
using BookStore.Core.Entities.Orders;
using System.Text.Json;


namespace BookStore.Repository.ConText
{
    public static class StoreSeed
    {
        public async static Task SeedAsync(StoreContext storeContext)
        {
            if (storeContext.Categories.Count() == 0)
            {
                //1-get BrandsData (jsonFile Path)
                var categoryData = File.ReadAllText("../BookStore.Repository/Data/DataSeed/categories.json");
                //2-Deserialize from json to list
                var categories = JsonSerializer.Deserialize<List<Category>>(categoryData);

                //3-add to data base
                if (categories?.Count() > 0)
                {

                    foreach (var category in categories)
                    {
                        storeContext.Categories.Add(category);
                    }
                    await storeContext.SaveChangesAsync();
                }
            }
            if (storeContext.Books.Count() == 0)
            {
                //1-get BrandsData (jsonFile Path)
                var bookData = File.ReadAllText("../BookStore.Repository/Data/DataSeed/books.json");
                //2-Deserialize from json to list
                var books = JsonSerializer.Deserialize<List<Book>>(bookData);

                //3-add to data base
                if (books?.Count() > 0)
                {

                    foreach (var book in books)
                    {
                        storeContext.Books.Add(book);
                    }
                    await storeContext.SaveChangesAsync();
                }
            }
            if (storeContext.DeliveryMethods.Count() == 0)
            {
                //1-get BrandsData (jsonFile Path)
                var deliveryData = File.ReadAllText("../BookStore.Repository/Data/DataSeed/delivery.json");
                //2-Deserialize from json to list
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);

                //3-add to data base
                if (deliveryMethods?.Count() > 0)
                {

                    foreach (var method in deliveryMethods)
                    {
                        storeContext.DeliveryMethods.Add(method);
                    }
                    await storeContext.SaveChangesAsync();
                }
            }
        }
    }
}
