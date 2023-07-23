using MediatR;
using Microsoft.AspNetCore.Identity;
using NuGet.Protocol.Plugins;
using Store.Web.Abstractions.Data;
using Store.Web.Models;

namespace Store.Web.Application.Product.Commands
{
    public class StoreChangeIsOnTradeCommandHandler : IRequestHandler<StoreChangeIsOnTradeCommand>
    {
        private readonly ILogger<StoreChangeIsOnTradeCommandHandler> _logger;
        private readonly IProductRepo<Models.Product> _repoProduct;
        private readonly IHistoryNoteRepo<HistoryNote> _repoHistory;
        private readonly UserManager<IdentityUser> _userManager;

        public StoreChangeIsOnTradeCommandHandler(ILogger<StoreChangeIsOnTradeCommandHandler> logger,
                                                  IProductRepo<Models.Product> repoProduct,
                                                  IHistoryNoteRepo<HistoryNote> repoHistory,
                                                  UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _repoProduct = repoProduct;
            _repoHistory = repoHistory;
            _userManager = userManager;
        }

        public async Task Handle(StoreChangeIsOnTradeCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == null || request.User == null)
            {
                _logger.LogInformation("ChangeIsOnTrade: id = null or User = null");
                return;
            }

            Models.Product product = null;

            try
            {
                product = await _repoProduct.GetProductById(request.Id.Value.ToString());
                _logger.LogInformation("ChangeIsOnTrade: product was received");
            }
            catch (Exception e)
            {
                _logger.LogError($"ChangeIsOnTrade: product was not received. Reason: {e.Message}");
                return;
            }

            if (product == null)
            {
                _logger.LogInformation("Product if null");
                return;
            }

            HistoryNote note;

            if (product.IsOnTrade)
            {
                product.IsOnTrade = !product.IsOnTrade;

                try
                {
                    await _repoProduct.UpdateProduct(product);
                    _logger.LogInformation($"ChangeIsOnTrade: product was successfully updated");
                }
                catch (Exception e)
                {
                    _logger.LogError($"ChangeIsOnTrade: unable to update product, Reason: {e.Message}");
                    return;
                }

                note = new HistoryNote()
                {
                    Message = $"Продукт {product.Name} перестал продаваться. Пользователь:{request.User.Identity.Name}",
                    Date = DateTime.Now,
                    UserId = _userManager.GetUserId(request.User)
                };

                try
                {
                    await _repoHistory.CreateHistoryNote(note);
                    _logger.LogInformation($"ChangeIsOnTrade: note was succesfully created");
                }
                catch (Exception e)
                {
                    _logger.LogError($"ChangeIsOnTrade: unable to create note, Reason: {e.Message}");
                    return;
                }

                return;
            }

            if (DateTime.Now >= product.ExpireDate)
            {
                _logger.LogInformation($"Product is corrupted. Product.ExpireDate = {product.ExpireDate}");
                return;
            }

            product.IsOnTrade = !product.IsOnTrade;

            note = new HistoryNote()
            {
                Message = $"Продукт {product.Name} начал продаваться. Пользователь:{request.User.Identity.Name}",
                Date = DateTime.Now,
                UserId = _userManager.GetUserId(request.User)
            };

            try
            {
                await _repoProduct.UpdateProduct(product);
                await _repoHistory.CreateHistoryNote(note);
                _logger.LogInformation($"ChangeIsOnTrade: product was successfulyy updated and note was succesfully created");
            }
            catch (Exception e)
            {
                _logger.LogError($"ChangeIsOnTrade: unable to create note or update product, Reason: {e.Message}");
                return;
            }
        }
    }
}
