using Shopping.Common;
using Shopping.Data;
using Shopping.Entities;
using Shopping.Enums;
using Shopping.Interface;
using Shopping.Models;

namespace Shopping.Repositories
{
    public class OrderHelper : IOrderHelper
    {
        private readonly DataContext _context;

        public OrderHelper(DataContext context)
        {
            _context = context;
        }

        public async Task<Response> ProcessOrderAsync(ShowCartViewModel model)
        {
            Response response = await CheckInventoryAsync(model);
            if (!response.IsSuccess)
            {
                return response;
            }

            Order order = new()
            {
                Date = DateTime.UtcNow,
                User = model.User,
                Remarks = model.Remarks,
                SaleDetails = new List<OrderDetail>(),
                OrderStatus = OrderStatus.New
            };

            foreach (TemporalSale item in model.TemporalSales)
            {
                order.SaleDetails.Add(new OrderDetail
                {
                    Product = item.Product,
                    Quantity = item.Quantity,
                    Remarks = item.Remarks,
                });

                Product product = await _context.Products.FindAsync(item.Product.Id);
                if (product != null)
                {
                    product.Stock -= item.Quantity;
                    _context.Products.Update(product);
                }

                _context.TemporalSales.Remove(item);
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return response;

        }

        private async Task<Response> CheckInventoryAsync(ShowCartViewModel model)
        {
            Response response = new() { IsSuccess = true };
            foreach (TemporalSale item in model.TemporalSales)
            {
                Product product = await _context.Products.FindAsync(item.Product.Id);
                if (product == null)
                {
                    response.IsSuccess = false;
                    response.Message = $"The product {item.Product.Name}, is not available";
                    return response;
                }
                if (product.Stock < item.Quantity)
                {
                    response.IsSuccess = false;
                    response.Message = $"Sorry, we don't have the item {item.Product.Name}, in stock. please lower your quantity or sustitue with another product.";
                    return response;
                }
            }
            return response;
        }
    }
}
