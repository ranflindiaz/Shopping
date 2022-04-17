using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shopping.Data;
using Shopping.Entities;
using Shopping.Interface;

namespace Shopping.Repositories
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboCategoriesAsync()
        {
            List<SelectListItem> categories = await _context.Categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
            })
                .OrderBy(c => c.Text)
                .ToListAsync();

            categories.Insert(0, new SelectListItem { Text = "Select a Category...", Value = "0" });

            return categories;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboCitiesAsync(int stateId)
        {
            List<SelectListItem> city = await _context.Cities
                .Where(c => c.State.Id == stateId)
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                })
                .OrderBy(c => c.Text)
                .ToListAsync();

            city.Insert(0, new SelectListItem { Text = "Select a City...", Value = "0" });

            return city;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboStatesAsync(int countryId)
        {
            List<SelectListItem> states = await _context.States
                .Where(c => c.Country.Id == countryId)
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                })
                .OrderBy(c => c.Text)
                .ToListAsync();

            states.Insert(0, new SelectListItem { Text = "Select a State...", Value = "0" });

            return states;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboCountriesAsync()
        {
            List<SelectListItem> country = await _context.Countries.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
            })
                .OrderBy(c => c.Text)
                .ToListAsync();

            country.Insert(0, new SelectListItem { Text = "Select a Country...", Value = "0" });

            return country;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboCategoriesAsync(IEnumerable<Category> filter)
        {
            List<Category> categories = await _context.Categories.ToListAsync();
            List<Category> categoriesFiltered = new();
            foreach (Category category in categories)
            {
                if (!filter.Any(c => c.Id == category.Id))
                {
                    categoriesFiltered.Add(category);
                }
            }

            List<SelectListItem> list = categoriesFiltered.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
            })
                .OrderBy(c => c.Text)
                .ToList();

            list.Insert(0, new SelectListItem { Text = "Select a Category...", Value = "0" });

            return list;
        }
    }
}
