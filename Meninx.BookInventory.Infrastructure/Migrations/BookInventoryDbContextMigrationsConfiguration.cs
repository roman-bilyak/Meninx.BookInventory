using System;
using System.Data.Entity.Migrations;

namespace Meninx.BookInventory.Migrations
{
    public sealed class BookInventoryDbContextMigrationsConfiguration : DbMigrationsConfiguration<BookInventoryDbContext>
    {
        public BookInventoryDbContextMigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BookInventoryDbContext context)
        {
            Category dotnetCategory = new Category
            {
                Id = Guid.Parse("a29c0c4d-94c1-4f5a-8c3b-605ae17c8a7f"),
                Name = ".NET",
                Description = "Books related to .NET technologies"
            };

            Category aspnetCategory = new Category
            {
                Id = Guid.Parse("b8d0b7e2-5a0e-48f2-9f77-1f3f2120e3c9"),
                Name = "ASP.NET",
                Description = "Books related to ASP.NET technologies"
            };

            Category mssqlCategory = new Category
            {
                Id = Guid.Parse("c3146b12-df9d-4c9c-ae82-8399e286bc8a"),
                Name = "MS SQL",
                Description = "Books related to Microsoft SQL Server"
            };

            Category[] categories = new[] { dotnetCategory, aspnetCategory, mssqlCategory };
            context.Categories.AddOrUpdate(x => x.Id, categories);

            Book[] books = new Book[]
            {
                new Book
                 {
                     Id = new Guid("f5747a07-91f9-4e1f-b2c2-1db966c3d987"),
                     Title = "CLR via C#",
                     Author = "Jeffrey Richter",
                     ISBN = "978-0735667457",
                     PublicationYear = 2012,
                     Quantity = 12,
                     CategoryId = dotnetCategory.Id
                 },
                 new Book
                 {
                     Id = new Guid("0b0e9faa-149a-4c0e-8e1a-589c3c68e5a1"),
                     Title = "Clean Code: A Handbook of Agile Software Craftsmanship",
                     Author = "Robert C. Martin",
                     ISBN = "978-0132350884",
                     PublicationYear = 2008,
                     Quantity = 15,
                     CategoryId = dotnetCategory.Id
                 },
                 new Book
                 {
                     Id = new Guid("6e0863c6-25fb-4e61-8950-aa3b2222ce79"),
                     Title = "Pro C# 9 and .NET 6",
                     Author = "Andrew Troelsen, Philip Japikse",
                     ISBN = "978-1484268229",
                     PublicationYear = 2021,
                     Quantity = 7,
                     CategoryId = dotnetCategory.Id
                 },
                 new Book
                 {
                     Id = new Guid("f5f89e45-475a-45f4-944e-65ff0d2c6050"),
                     Title = "Modern API Design with ASP.NET Core 6",
                     Author = "Fanie Reynders",
                     ISBN = "978-1803236709",
                     PublicationYear = 2021,
                     Quantity = 8,
                     CategoryId = aspnetCategory.Id
                 },
                 new Book
                 {
                     Id = new Guid("3e5b3ea9-ee2e-415f-b7eb-bc4a792c47ce"),
                     Title = "Professional C# 10 and .NET 7",
                     Author = "Christian Nagel",
                     ISBN = "978-1119701856",
                     PublicationYear = 2022,
                     Quantity = 9,
                     CategoryId = dotnetCategory.Id
                 },
                 new Book
                 {
                     Id = new Guid("9b2e6f11-e1e7-4175-9a8d-32e68872e776"),
                     Title = "ASP.NET Core 5 and Angular 12",
                     Author = "Valerio De Sanctis",
                     ISBN = "978-1803236457",
                     PublicationYear = 2021,
                     Quantity = 10,
                     CategoryId = aspnetCategory.Id
                 },
                 new Book
                 {
                     Id = new Guid("ac2e874d-b6e5-42c1-94d1-e57f586b2d2c"),
                     Title = "SQL Performance Explained",
                     Author = "Markus Winand",
                     ISBN = "978-3950307827",
                     PublicationYear = 2018,
                     Quantity = 8,
                     CategoryId = mssqlCategory.Id
                 },
                 new Book
                 {
                     Id = new Guid("f857a0b7-cbc4-409a-9381-6204ff062f52"),
                     Title = "SQL Performance Tuning",
                     Author = "Peter Gulutzan, Trudy Pelzer",
                     ISBN = "978-0201791692",
                     PublicationYear = 2002,
                     Quantity = 5,
                     CategoryId = mssqlCategory.Id
                 },
                 new Book
                 {
                     Id = new Guid("7bca6c18-74ea-4b69-b7eb-33a1234d0fb6"),
                     Title = "Design Patterns: Elements of Reusable Object-Oriented Software",
                     Author = "Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides",
                     ISBN = "978-0201633610",
                     PublicationYear = 1994,
                     Quantity = 18,
                     CategoryId = dotnetCategory.Id
                 },
                 new Book
                 {
                     Id = new Guid("d0b79b1f-8ab2-4b97-971b-31f7efeeb0ed"),
                     Title = "Pro ASP.NET Core MVC 2",
                     Author = "Adam Freeman",
                     ISBN = "978-1484231490",
                     PublicationYear = 2017,
                     Quantity = 13,
                     CategoryId = aspnetCategory.Id
                 },
                 new Book
                 {
                     Id = new Guid("da4be84a-e2e1-4b07-b1d1-1f48c0b378b3"),
                     Title = "Pro SQL Server Relational Database Design and Implementation",
                     Author = "Louis Davidson, Jessica Moss",
                     ISBN = "978-1484258855",
                     PublicationYear = 2019,
                     Quantity = 6,
                     CategoryId = mssqlCategory.Id
                 },
                 new Book
                 {
                     Id = new Guid("84f4d3b3-e503-49df-9e4f-3f914c9a3290"),
                     Title = "Entity Framework Core in Action",
                     Author = "Jon Smith",
                     ISBN = "978-1617294565",
                     PublicationYear = 2018,
                     Quantity = 6,
                     CategoryId = dotnetCategory.Id
                 },
                 new Book
                 {
                     Id = new Guid("0d44a971-1921-4b4d-82d0-95b603a6e6d6"),
                     Title = "Pro ASP.NET Core Identity",
                     Author = "Adam Freeman",
                     ISBN = "978-1484238284",
                     PublicationYear = 2019,
                     Quantity = 9,
                     CategoryId = aspnetCategory.Id
                 },
                 new Book
                 {
                     Id = new Guid("f7a33027-3f87-427b-800a-9908c8aa2c1e"),
                     Title = "SQL Server 2019 Query Performance Tuning",
                     Author = "Grant Fritchey",
                     ISBN = "978-1484251665",
                     PublicationYear = 2020,
                     Quantity = 3,
                     CategoryId = mssqlCategory.Id
                 },
                 new Book
                 {
                     Id = new Guid("63c2725e-39c4-4a33-867d-413c35f9246d"),
                     Title = "C# in Depth",
                     Author = "Jon Skeet",
                     ISBN = "978-1617294534",
                     PublicationYear = 2019,
                     Quantity = 11,
                     CategoryId = dotnetCategory.Id
                 },
                 new Book
                 {
                     Id = new Guid("01b877c5-2e08-4d10-9d5c-07d9e44a1a08"),
                     Title = "Pro ASP.NET Core Web API",
                     Author = "Adam Freeman",
                     ISBN = "978-1484254505",
                     PublicationYear = 2021,
                     Quantity = 8,
                     CategoryId = aspnetCategory.Id
                 },
                 new Book
                 {
                     Id = new Guid("7a1d654d-9e7d-47ad-9072-665e44c44c2d"),
                     Title = "Mastering Microsoft SQL Server 2019",
                     Author = "Hannes Du Preez",
                     ISBN = "978-1803237799",
                     PublicationYear = 2021,
                     Quantity = 4,
                     CategoryId = mssqlCategory.Id
                 },
                 new Book
                 {
                     Id = new Guid("3b9733a3-78d3-4a07-b2f9-8b180965a11f"),
                     Title = "Pro C# 7: With .NET and .NET Core",
                     Author = "Andrew Troelsen, Philip Japikse",
                     ISBN = "978-1484230172",
                     PublicationYear = 2017,
                     Quantity = 14,
                     CategoryId = dotnetCategory.Id
                 },
                 new Book
                 {
                     Id = new Guid("cfdff15f-eb20-47bb-bc65-36b6bf2ff3ca"),
                     Title = "ASP.NET Core 3.1 MVC and Razor Pages for Beginners",
                     Author = "Jonas Fagerberg",
                     ISBN = "979-8607793300",
                     PublicationYear = 2020,
                     Quantity = 9,
                     CategoryId = aspnetCategory.Id
                 },
                 new Book
                 {
                     Id = new Guid("a2fc5d4e-b013-4d18-aae4-56aa987c6f10"),
                     Title = "Microsoft SQL Server 2019: A Beginner's Guide",
                     Author = "Dusan Petkovic",
                     ISBN = "978-1260459615",
                     PublicationYear = 2020,
                     Quantity = 5,
                     CategoryId = mssqlCategory.Id
                 }
            };
            context.Books.AddOrUpdate(x => x.Id, books);

            context.SaveChanges();

        }
    }
}