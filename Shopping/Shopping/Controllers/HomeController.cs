﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Data;
using Shopping.Entities;
using Shopping.Models;
using System.Diagnostics;

namespace Shopping.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
                List<Product>? products = await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductCategories)
                .ThenInclude(p => p.Category)
                .ToListAsync();

            List<ProductsHomeViewModel> productsHome = new() { new ProductsHomeViewModel() };

            int i = 1;
            foreach (Product? product in products)
            {
                if (i == 1)
                {
                    productsHome.LastOrDefault().Product1 = product;
                }
                if (i == 2)
                {
                    productsHome.LastOrDefault().Product2 = product;
                }
                if (i == 3)
                {
                    productsHome.LastOrDefault().Product3 = product;
                }
                if (i == 4)
                {
                    productsHome.LastOrDefault().Product4 = product;
                    productsHome.Add(new ProductsHomeViewModel());
                    i = 0;
                }
                i++;
            }

            return View(productsHome);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("error/404")]
        public IActionResult Error404()
        {
            return View();
        }
    }
}