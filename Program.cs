using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqProject
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Category> categories = new List<Category>() { 
            
                // Category listesini oluşturalım
                new Category{CategoryId=1,CategoryName="Bilgisayar"},
                new Category{CategoryId=2,CategoryName="Telefon"}


            };

            List<Product> products = new List<Product>() {

                new Product{
                    ProductId=1,
                    CategoryId=1,
                    ProductName="Acer Loptop",
                    QuantityPerUnit="32 GB Ram",
                    UnitPrice=10000,
                    UnitsInStok=5},


                  new Product{
                    ProductId=2,
                    CategoryId=1,
                    ProductName="Asus Loptop",
                    QuantityPerUnit="16 GB Ram",
                    UnitPrice=18000,
                    UnitsInStok=3},


                    new Product{
                    ProductId=3,
                    CategoryId=1,
                    ProductName="Hp Loptop",
                    QuantityPerUnit="8 GB Ram",
                    UnitPrice=18000,
                    UnitsInStok=2},


                      new Product{
                    ProductId=4,
                    CategoryId=2,
                    ProductName="Samsung ",
                    QuantityPerUnit="4 GB Ram",
                    UnitPrice=5000,
                    UnitsInStok=15},

                        new Product{
                    ProductId=5,
                    CategoryId=2,
                    ProductName="Apple ",
                    QuantityPerUnit="4 GB Ram",
                    UnitPrice=8000,
                    UnitsInStok=0},

            };
            //Test(products);

            // GetProducts(products);


            // AnyTest(products);

            // FindTest(products);

            // FindAllTest(products);

            // AscDescTest(products);

            // LinqIlkKullanımTest(products);


            // ClassicLinqDTOTest(products);


            //JoinLinqTest(categories, products);

        }

        private static void JoinLinqTest(List<Category> categories, List<Product> products)
        {
            // categories ve products listelerini join ile birleştirelim
            // ProductDTO oluşturalım, categories ile products ilişkilendirmek için CategoryId alanını kullanırız.
            // 

            var result = from p in products
                         join c in categories // categories ile products daki her p yi,
                         on p.CategoryId equals c.CategoryId  // CategoryId sine göre birleştir,
                         where p.UnitPrice > 6000  // 6000 den büyük olanları getir
                         orderby p.UnitPrice descending  // birim fiyata göre azalan sırala
                         select new ProductDTO
                         {
                             ProductId = p.ProductId,
                             ProductName = p.ProductName,
                             CategoryName = c.CategoryName,
                             UnitPrice = p.UnitPrice
                         };

            foreach (var productDTO in result)
            {
                Console.WriteLine("Markası \t:{0}  \n Kategorisi \t:{1} \n Fiyatı \t:{2} ", productDTO.ProductName, productDTO.CategoryName, productDTO.UnitPrice);
            }
        }

        private static void ClassicLinqDTOTest(List<Product> products)
        {
            // ProductDTO dizisi verir
            var result = from p in products  // ürünler listesindeki her p den 
                         where p.UnitPrice > 5000 && p.UnitPrice < 15000
                         select new ProductDTO
                         {
                             ProductId = p.ProductId,
                             ProductName = p.ProductName,
                             UnitPrice = p.UnitPrice
                         };
            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        private static void LinqIlkKullanımTest(List<Product> products)
        {
            // Linq ilk kullanım şekli

            var result = from p in products  // ürünler listesindeki her p den 
                         where p.UnitPrice > 5000 && p.UnitPrice < 15000
                         select p; // p leri seç getir.
            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        private static void AscDescTest(List<Product> products)
        {
            //  
            var result = products.Where(p => p.ProductName.Contains("top")) //içinde top metni geçen 
                .OrderByDescending(p => p.UnitPrice) // ürün fiyatları azalan 
                .ThenByDescending(p => p.ProductName); // Z--> A ya doğru sıralar 

            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        private static void FindAllTest(List<Product> products)
        {
            // koşula uyan tüm elemanları getirecektir. 
            // içerisinde top oaln ürünleri listeler.
            // Contains("aranılan metin")--> içeirisnde bulunan 
            var result = products.FindAll(p => p.ProductName.Contains("top"));
        }

        private static void FindTest(List<Product> products)
        {
            // Find metodu dönüş olarak Product döner,aranılan elemanın kendisini verir.
            // olamayan bir ürün null döner
            var result = products.Find(p => p.ProductId == 2);
            Console.WriteLine(result.ProductName);
        }

        private static void AnyTest(List<Product> products)
        {
            // liste içerisinde bir eleman var mı? yok mu ? sorgusunu  bool olarak true döner.
            var result = products.Any(p => p.ProductName == "Acer Loptop");
            Console.WriteLine(result);
        }

        private static void Test(List<Product> products)
        {
            Console.WriteLine("Algoritmik olarak gösterim \n");
            foreach (var product in products)
            {
                if (product.UnitPrice > 5000 && product.UnitsInStok > 3)
                {
                    Console.WriteLine(product.ProductName);
                }
            }

            Console.WriteLine("\n Linq olarak gösterim\n");


            // birim fiyatı 5 binden büyük ve stok adedi 3 den büyük olan ürünleri getir sorgusu
            // Bu sorgu sonucu liste döner
            var result = products.Where(p => p.UnitPrice > 5000 && p.UnitsInStok > 3);
            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        // ürünleri listeleyen operasyon Algoritmik olarak 
        static List<Product> GetProducts(List<Product> products)
        {

            // filtrelenmiş ürünleri ekleyeceğimiz liste
            List<Product> filteredProducts = new List<Product>();
            foreach (var product in products)
            {
                if (product.UnitPrice > 5000 && product.UnitsInStok > 3)
                {
                    filteredProducts.Add(product);
                }

            }

            return filteredProducts;
        }

        static List<Product> GetProductsLinq(List<Product> products)
        {
            // liste olarak filtrelenmiş biçimde dönecek
            return products.Where(p => p.UnitPrice > 5000 && p.UnitsInStok > 3).ToList();
        }
    }

    class ProductDTO
    {
        // DTO: Data transer object

        public int ProductId { get; set; }
        public string  ProductName { get; set; }
        public string CategoryName { get; set; } // Product class içinde bu alanın id değeri var ismi yok 
        public decimal UnitPrice { get; set; }

    }

}
