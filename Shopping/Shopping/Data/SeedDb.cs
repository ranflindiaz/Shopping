using Microsoft.EntityFrameworkCore;
using Shopping.Entities;
using Shopping.Enums;
using Shopping.Interface;

namespace Shopping.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;

        public SeedDb(DataContext context, IUserHelper userHelper, IBlobHelper blobHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
        }

        public async Task SeedAsync()
        {
            //Create data base and apply migrations
            await _context.Database.EnsureCreatedAsync();

            await CheckCategoriesAsync();
            await CheckCountriesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Ranflin", "Diaz", "ranflin@yopmail.com", "333 211 4035",
                "Calle South 3rd", "ranflin.jpg", UserType.Admin);
            await CheckUserAsync("2020", "Brad", "Pitt", "brad@yopmail.com", "323 210 4055",
                "Calle 5th Avenue", "bard.png", UserType.User);
            await CheckUserAsync("3030", "Angelina", "Jolie", "angelina@yopmail.com", "488 213 4515",
                "Calle Lexintong Avenue", "angelina.png", UserType.User);
            await CheckUserAsync("4040", "Bob", "Marley", "bobmarley@yopmail.com", "718 289 3254",
                "Calle Park Avenue", "bobmarley.png", UserType.User);
            await CheckProductsAsync();
        }

        private async Task CheckProductsAsync()
        {
            if (!_context.Products.Any())
            {
                await AddProductAsync("AirPods", 1300000M, 12F, new List<string>() { "Tecnology", "Apple" }, new List<string>() { "airpods.png", "airpods2.png" });
                await AddProductAsync("Helmet Bike", 820000M, 12F, new List<string>() { "Sport" }, new List<string>() { "bikehelmet.png"});
                await AddProductAsync("iPad", 2300000M, 6F, new List<string>() { "Tecnology", "Apple" }, new List<string>() { "ipad.png" });
                await AddProductAsync("iPhone 13", 5200000M, 6F, new List<string>() { "Tecnology", "Apple" }, new List<string>() { "iphone13.png", "iphone13-2.png" });
                await AddProductAsync("New Balance 530", 180000M, 12F, new List<string>() { "Shoes", "Sport" }, new List<string>() { "newbalance530.png" });
                await AddProductAsync("Nike Air", 233000M, 12F, new List<string>() { "Shoes", "Sport" }, new List<string>() { "nike_air.png" });
                await AddProductAsync("Weight Protein", 252000M, 12F, new List<string>() { "Nutritión" }, new List<string>() { "whey_protein.png" });
                await AddProductAsync("Pets Moths", 25000M, 12F, new List<string>() { "Pets" }, new List<string>() { "arnes_mascota.png" });
                await AddProductAsync("Pets Beds", 99000M, 12F, new List<string>() { "Pets" }, new List<string>() { "petbed.png" });
                await AddProductAsync("Gamer Keyboard", 67000M, 12F, new List<string>() { "Gamer", "Tecnology" }, new List<string>() { "teclado_gamer.png" });
                await AddProductAsync("Gamer Chair", 980000M, 12F, new List<string>() { "Gamer", "Tecnology" }, new List<string>() { "silla_gamer.png" });
                await AddProductAsync("Mouse Gamer", 132000M, 12F, new List<string>() { "Gamer", "Tecnology" }, new List<string>() { "mousegamer.png" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task AddProductAsync(string name, decimal price, float stock, List<string> categories, List<string> images)
        {
            Product prodcut = new()
            {
                Description = name,
                Name = name,
                Price = price,
                Stock = stock,
                ProductCategories = new List<ProductCategory>(),
                ProductImages = new List<ProductImage>()
            };

            foreach (string? category in categories)
            {
                prodcut.ProductCategories.Add(new ProductCategory { Category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == category) });
            }


            foreach (string? image in images)
            {
                Guid imageId = await _blobHelper.UploadBlobAsync($"{Environment.CurrentDirectory}\\wwwroot\\img\\products\\{image}", "products");
                prodcut.ProductImages.Add(new ProductImage { ImageId = imageId });
            }

            _context.Products.Add(prodcut);
        }


        public async Task<User> CheckUserAsync(
            string document,
            string firstName,
            string lastName,
            string email,
            string phone,
            string address,
            string image,
            UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                Guid imageId = await _blobHelper.UploadBlobAsync($"{Environment.CurrentDirectory}" +
                    $"\\wwwroot\\img\\users\\{image}", "users");

                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = _context.Cities.FirstOrDefault(),
                    UserType = userType,
                    ImageId = imageId
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    Name = "USA",
                    States = new List<State>()
                    {
                        new State {
                            Name = "New York",
                            Cities = new List<City>()
                            {
                                new City { Name = "Manhattan"},
                                new City { Name = "Brooklyn"},
                                new City { Name = "Queens"},
                                new City { Name = "Bronx"},
                                new City { Name = "Long Island"},
                                new City { Name = "Albany"},
                            }
                        },
                        new State {
                        Name ="Florida",
                        Cities=new List<City>()
                            {
                                new City { Name = "Fort Lauderdale"},
                                new City { Name = "Key West"},
                                new City { Name = "Orlando"},
                                new City { Name = "Miami"},
                                new City { Name = "Tampa"},
                            }
                        },
                        new State
                        {
                            Name = "Texas",
                             Cities = new List<City>
                             {
                                new City { Name = "Houston"},
                                new City { Name = "San Antonio"},
                                new City { Name = "Dallas"},
                                new City { Name = "Austin"},
                                new City { Name = "El Paso"},
                             }
                        }
                    }
                });

                _context.Countries.Add(new Country
                {
                    Name = "Dominican Republic",
                    States = new List<State>()
                    {
                        new State {
                            Name = "Santo Domingo",
                            Cities = new List<City>()
                            {
                                new City { Name = "Santo Domingo Este"},
                                new City { Name = "Santo Domingo Norte"},
                                new City { Name = "Santo Domingo Oeste"},
                                new City { Name = "Los Alcarrizos"},
                                new City { Name = "Boca Chica"},
                            }
                        }
                    }
                });

                _context.Countries.Add(new Country
                {
                    Name = "Ecuador",
                    States = new List<State>()
                    {
                        new State()
                        {
                            Name = "Pichincha",
                            Cities = new List<City>() {
                                new City() { Name = "Quito" },
                            }
                        },
                        new State()
                        {
                            Name = "Esmeraldas",
                            Cities = new List<City>() {
                                new City() { Name = "Esmeraldas" },
                            }
                        }
                    }
                });

                _context.Countries.Add(new Country
                {
                    Name = "Colombia",
                    States = new List<State>()
            {
                new State()
                {
                    Name = "Antioquia",
                    Cities = new List<City>() {
                        new City() { Name = "Medellín" },
                        new City() { Name = "Itagüí" },
                        new City() { Name = "Envigado" },
                        new City() { Name = "Bello" },
                        new City() { Name = "Sabaneta" },
                        new City() { Name = "La Ceja" },
                        new City() { Name = "La Union" },
                        new City() { Name = "La Estrella" },
                        new City() { Name = "Copacabana" },
                    }
                },
                new State()
                {
                    Name = "Bogotá",
                    Cities = new List<City>() {
                        new City() { Name = "Usaquen" },
                        new City() { Name = "Champinero" },
                        new City() { Name = "Santa fe" },
                        new City() { Name = "Usme" },
                        new City() { Name = "Bosa" },
                    }
                },
                new State()
                {
                    Name = "Valle",
                    Cities = new List<City>() {
                        new City() { Name = "Calí" },
                        new City() { Name = "Jumbo" },
                        new City() { Name = "Jamundí" },
                        new City() { Name = "Chipichape" },
                        new City() { Name = "Buenaventura" },
                        new City() { Name = "Cartago" },
                        new City() { Name = "Buga" },
                        new City() { Name = "Palmira" },
                    }
                },
                new State()
                {
                    Name = "Santander",
                    Cities = new List<City>() {
                        new City() { Name = "Bucaramanga" },
                        new City() { Name = "Málaga" },
                        new City() { Name = "Barrancabermeja" },
                        new City() { Name = "Rionegro" },
                        new City() { Name = "Barichara" },
                        new City() { Name = "Zapatoca" },
                    }
                },
            }
                });


                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCategoriesAsync()
            {
                if (!_context.Categories.Any())
                {
                    _context.Categories.Add(new Category { Name = "Tecnology" });
                    _context.Categories.Add(new Category { Name = "Clothe" });
                    _context.Categories.Add(new Category { Name = "Gamer" });
                    _context.Categories.Add(new Category { Name = "Beauty" });
                    _context.Categories.Add(new Category { Name = "Nutritión" });
                    _context.Categories.Add(new Category { Name = "Shoes" });
                    _context.Categories.Add(new Category { Name = "Sport" });
                    _context.Categories.Add(new Category { Name = "Pets" });
                    _context.Categories.Add(new Category { Name = "Apple" });

                    await _context.SaveChangesAsync();
                }
            }

    } 
}
