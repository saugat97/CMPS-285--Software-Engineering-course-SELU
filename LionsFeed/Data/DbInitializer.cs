using CryptoHelper;
using LionsFeed.Models;
using System.Linq;

namespace LionsFeed.Data
{
    public class DbInitializer
    {
        public static void SeedData(LionsContext _context)
        {
            _context.Database.EnsureCreated();

            if (_context.Users.Any())
            {
                return;
            }

            var Users = new ApplicationUser[]
            {
                new ApplicationUser
                {
                    FirstName="Anish",
                    LastName="Manandhar",
                    Email = "w0658737@selu.edu",
                    Password = Crypto.HashPassword("password"),
                    Role = "Admin",
                    Gender="Male",
                    ImageUrl="/upload/img/default.png"
                },
                new ApplicationUser
                {
                    FirstName="Krishna",
                    LastName="Paudel",
                    Email = "w0671970@selu.edu",
                    Password = Crypto.HashPassword("password"),
                    Role = "User",
                    Gender="Male",
                    ImageUrl="/upload/img/default.png"

                },
                new ApplicationUser
                {
                    FirstName="Brianne",
                    LastName="Vigeant",
                    Email = "w0673860@selu.edu",
                    Password = Crypto.HashPassword("password"),
                    Role = "User",
                    Gender="Female",
                    ImageUrl="/upload/img/female.png"

                }
            };
            foreach (ApplicationUser u in Users)
            {
                _context.Users.Add(u);

            }
            _context.SaveChanges();

        }
    }
}
