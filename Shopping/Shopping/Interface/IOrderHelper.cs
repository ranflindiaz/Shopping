using Shopping.Common;
using Shopping.Models;

namespace Shopping.Interface
{
    public interface IOrderHelper
    {
        Task<Response> ProcessOrderAsync(ShowCartViewModel model);
    }
}
