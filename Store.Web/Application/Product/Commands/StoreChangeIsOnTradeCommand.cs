using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Store.Web.Application.Product.Commands
{
    public class StoreChangeIsOnTradeCommand : IRequest
    {
        public Guid? Id { get; set; }
        public ClaimsPrincipal? User { get; set; }
    }
}
