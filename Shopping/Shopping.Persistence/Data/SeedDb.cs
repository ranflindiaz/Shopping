using Shopping.Domain.Entities;

namespace Shopping.Persistence.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            //Create data base and apply migrations
            await _context.Database.EnsureCreatedAsync();

            await CheckCategoriesAsync();
            await CheckCountriesAsync();
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
            }

            await _context.SaveChangesAsync();
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Tecnology" });
                _context.Categories.Add(new Category { Name = "Clothes" });
                _context.Categories.Add(new Category { Name = "Shoes" });
                _context.Categories.Add(new Category { Name = "Nutrition" });
                _context.Categories.Add(new Category { Name = "Sports" });
                _context.Categories.Add(new Category { Name = "Apple" });
                _context.Categories.Add(new Category { Name = "Pets" });
                await _context.SaveChangesAsync();
            }
        }
    }
}
