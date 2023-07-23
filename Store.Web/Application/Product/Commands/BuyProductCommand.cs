using MediatR;
using System.Security.Claims;

namespace Store.Web.Application.Product.Commands
{
    public class BuyProductCommand : IRequest
    {
        public Guid? Id { get; set; }
        public ClaimsPrincipal? User { get; set; }
    }
}
