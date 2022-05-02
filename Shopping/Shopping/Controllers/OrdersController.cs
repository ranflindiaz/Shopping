using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Data;
using Shopping.Entities;
using Shopping.Enums;
using Shopping.Interface;
using Vereyon.Web;

namespace Shopping.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly DataContext _context;
        private readonly IFlashMessage _flashMessage;
        private readonly IOrderHelper _orderHelper;

        public OrdersController(DataContext context, IFlashMessage flashMessage, IOrderHelper orderHelper)
        {
            _context = context;
            _flashMessage = flashMessage;
            _orderHelper = orderHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order order = await _context.Orders
                .Include(s => s.User)
                .Include(s => s.OrderDetails)
                .ThenInclude(sd => sd.Product)
                .ThenInclude(p => p.ProductImages)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public async Task<IActionResult> Dispatch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            if (order.OrderStatus != OrderStatus.New)
            {
                _flashMessage.Danger("Only can be dispatch order with status 'New'.");
            }
            else
            {
                order.OrderStatus = OrderStatus.Fullfilled;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                _flashMessage.Confirmation("Your order status has been changed to 'Fullfilled'.");
            }

            return RedirectToAction(nameof(Details), new { order.Id });
        }

        public async Task<IActionResult> Send(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            if (order.OrderStatus != OrderStatus.Fullfilled)
            {
                _flashMessage.Danger("Only can be send order with status 'Fullfilled'.");
            }
            else
            {
                order.OrderStatus = OrderStatus.Sent;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                _flashMessage.Confirmation("Your order status has been changed to 'Sent'.");
            }

            return RedirectToAction(nameof(Details), new { order.Id });
        }

        public async Task<IActionResult> Confirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            if (order.OrderStatus != OrderStatus.Sent)
            {
                _flashMessage.Danger("Only can be confirmed orders with status 'Sent'.");
            }
            else
            {
                order.OrderStatus = OrderStatus.Confirmed;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                _flashMessage.Confirmation("Your order status has been changed to 'Confirmed'.");
            }

            return RedirectToAction(nameof(Details), new { order.Id });
        }

        public async Task<IActionResult> Cancel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            if (order.OrderStatus == OrderStatus.Cancelled)
            {
                _flashMessage.Danger("Can't cancel order with a status of 'Cancelled'.");
            }
            else
            {
                await _orderHelper.CancelOrderAsync(order.Id);
                _flashMessage.Confirmation("Status of this order has been changed to 'Cancelled'.");
            }

            return RedirectToAction(nameof(Details), new { order.Id });
        }

    }
}
