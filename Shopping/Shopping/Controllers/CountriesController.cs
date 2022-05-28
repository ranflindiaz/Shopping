using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Models;
using Shopping.Entities;
using Shopping.Data;
using Microsoft.AspNetCore.Authorization;
using Vereyon.Web;
using static Shooping.Repositories.ModalHelper;
using Shooping.Repositories;

namespace Shopping.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CountriesController : Controller
    {
        private readonly DataContext _context;
        private readonly IFlashMessage _flashMessage;

        public CountriesController(DataContext context, IFlashMessage flashMessage)
        {
            _context = context;
            _flashMessage = flashMessage;
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Countries
                .Include(x => x.States)
                .ThenInclude(x => x.Cities)
                .ToListAsync());
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .Include(x => x.States)
                .ThenInclude(s => s.Cities)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: State/Details/3
        public async Task<IActionResult> DetailState(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            State state = await _context.States
                .Include(s => s.Country)
                .Include(s => s.Cities)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }

        // GET: City/Details/2
        public async Task<IActionResult> DetailCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.Cities
                .Include(c => c.State)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        [NoDirectAccess]
        public async Task<IActionResult> AddState(int id)
        {
            Country country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            var model = new StateViewModel()
            {
                CountryId = country.Id,
            };

            return View(model);
        }

        // POST: State/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddState(StateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var state = new State()
                    {
                        Cities = new List<City>(),
                        Country = await _context.Countries.FindAsync(model.CountryId),
                        Name = model.Name
                    };

                    _context.Add(state);
                    await _context.SaveChangesAsync();
                    Country country = await _context.Countries
                        .Include(c => c.States)
                        .ThenInclude(s => s.Cities)
                        .FirstOrDefaultAsync(c => c.Id == model.CountryId);
                    _flashMessage.Info($"State {state.Name} created");
                    return Json(new { isValid = true, html = ModalHelper.RenderRazorViewToString(this, "_ViewAllStates", country) });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        _flashMessage.Danger("There is a State with the same name in this Country");
                    }
                    else
                    {
                        _flashMessage.Danger(dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    _flashMessage.Danger(exception.Message);
                }
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "AddState", model) });

        }

        // GET: State/Create
        public async Task<IActionResult> AddCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var state = await _context.States.FindAsync(id);

            if (state == null)
            {
                return NotFound();
            }

            var model = new CityViewModel()
            {
                StateId = state.Id,
            };

            return View(model);
        }

        // POST: State/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCity(CityViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var city = new City()
                    {
                        State = await _context.States.FindAsync(model.StateId),
                        Name = model.Name
                    };

                    _context.Add(city);
                    await _context.SaveChangesAsync();
                    _flashMessage.Info($"City {city.Name} created");
                    return RedirectToAction(nameof(DetailState), new { Id = model.StateId });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        _flashMessage.Danger("There is a City with the same name in this State.");
                    }
                    else
                    {
                        _flashMessage.Danger(dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    _flashMessage.Danger(exception.Message);
                }
            }
            return View(model);

        }

        [NoDirectAccess]
        public async Task<IActionResult> EditState(int id)
        {
            State state = await _context.States
                .Include(s => s.Country)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (state == null)
            {
                return NotFound();
            }

            var model = new StateViewModel()
            {
                CountryId = state.Country.Id,
                Id = state.Id,
                Name = state.Name,
            };

            return View(model);
        }

        // POST: State/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditState(int id, StateViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    State state = new()
                    {
                        Id = model.Id,
                        Name = model.Name,
                    };
                    _context.Update(state);
                    Country country = await _context.Countries
                        .Include(c => c.States)
                        .ThenInclude(s => s.Cities)
                        .FirstOrDefaultAsync(c => c.Id == model.CountryId);
                    await _context.SaveChangesAsync();
                    _flashMessage.Confirmation($"State {state.Name} updated.");
                    return Json(new { isValid = true, html = ModalHelper.RenderRazorViewToString(this, "_ViewAllStates", country) });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        _flashMessage.Danger("There is a State with the same name in this Country.");
                    }
                    else
                    {
                        _flashMessage.Danger(dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    _flashMessage.Danger(exception.Message);
                }
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "EditState", model) });
        }

        // GET: State/Edit/5
        public async Task<IActionResult> EditCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.Cities
                .Include(c => c.State)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            var model = new CityViewModel()
            {
                StateId = city.State.Id,
                Id = city.Id,
                Name = city.Name,
            };

            return View(model);
        }

        // POST: State/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCity(int id, CityViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var city = new City()
                    {
                        Id = model.Id,
                        Name = model.Name,
                    };

                    _context.Update(city);
                    await _context.SaveChangesAsync();
                    _flashMessage.Info($"City {city.Name} edited.");
                    return RedirectToAction(nameof(DetailState), new { Id = model.StateId });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        _flashMessage.Danger("There is a Cityu with the same name in this State.");
                    }
                    else
                    {
                        _flashMessage.Danger(dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    _flashMessage.Danger(exception.Message);
                }
            }
            return View(model);
        }

        [NoDirectAccess]
        public async Task<IActionResult> Delete(int id)
        {
            Country country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            try
            {
                _context.Countries.Remove(country);
                await _context.SaveChangesAsync();
                _flashMessage.Info("Record deleted.");
            }
            catch
            {
                _flashMessage.Danger("Can not delete a Country that has related records.");
            }

            return RedirectToAction(nameof(Index));
        }

        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Country());
            }
            else
            {
                Country country = await _context.Countries.FindAsync(id);
                if (country == null)
                {
                    return NotFound();
                }

                return View(country);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, Country country)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (id == 0) //Insert
                    {
                        _context.Add(country);
                        await _context.SaveChangesAsync();
                        _flashMessage.Info("Record created.");
                    }
                    else //Update
                    {
                        _context.Update(country);
                        await _context.SaveChangesAsync();
                        _flashMessage.Info("Record updated.");
                    }
                    return Json(new
                    {
                        isValid = true,
                        html = ModalHelper.RenderRazorViewToString(
                            this,
                            "_ViewAll",
                            _context.Countries
                                .Include(c => c.States)
                                .ThenInclude(s => s.Cities)
                                .ToList())
                    });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        _flashMessage.Danger("There is a country with the same name.");
                    }
                    else
                    {
                        _flashMessage.Danger(dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    _flashMessage.Danger(exception.Message);
                }
            }

            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "AddOrEdit", country) });
        }

        [NoDirectAccess]
        public async Task<IActionResult> DeleteState(int? id)
        {
            State state = await _context.States
                .Include(s => s.Country)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (state == null)
            {
                return NotFound();
            }

            try
            {
                _context.States.Remove(state);
                await _context.SaveChangesAsync();
                _flashMessage.Info("Record deleted.");
            }
            catch
            {
                _flashMessage.Danger("Can not delete State because has related records.");
            }

            return RedirectToAction(nameof(Details), new { id = state.Country.Id });
        }

        // GET: City/Delete/1
        public async Task<IActionResult> DeleteCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.Cities
                .Include(c => c.State)
                .FirstOrDefaultAsync(c => c.Id == id);

            return View(city);
        }

        // POST: City/Delete/1
        [HttpPost, ActionName("DeleteCity")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCityConfirmed(int id)
        {
            var city = await _context.Cities
                .Include(c => c.State)
                .FirstOrDefaultAsync(c => c.Id == id);
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
            _flashMessage.Info($"City {city.Name} deleted.");
            return RedirectToAction(nameof(DetailState), new { city.State.Id });
        }
    }
}
