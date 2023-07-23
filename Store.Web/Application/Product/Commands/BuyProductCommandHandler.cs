using MediatR;
using Microsoft.AspNetCore.Identity;
using Store.Web.Abstractions.Data;


namespace Store.Web.Application.Product.Commands
{
    public class BuyProductCommandHandler : IRequestHandler<BuyProductCommand>
    {
        private readonly IProductRepo<Models.Product> _productRepo;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHistoryNoteRepo<Models.HistoryNote> _historyNoteRepo;
        private readonly ILogger<BuyProductCommandHandler> _logger;

        public BuyProductCommandHandler(IProductRepo<Models.Product> repo,
                                        UserManager<IdentityUser> userManager,
                                        IHistoryNoteRepo<Models.HistoryNote> historyNoteRepo,
                                        ILogger<BuyProductCommandHandler> logger)
        {
            _productRepo = repo;
            _userManager = userManager;
            _historyNoteRepo = historyNoteRepo;
            _logger = logger;
        }

        public async Task Handle(BuyProductCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
            {
                _logger.LogWarning("Id = null");
                return;
            }

            Models.Product product = null;

            try
            {
                product = await _productRepo.GetProductById(request.Id.Value.ToString());
                _logger.LogInformation("MainView: product successfully received");
            }
            catch (Exception e)
            {
                _logger.LogError($"MainView: cannot get product. Reason: {e.Message}");
            }

            if (product == null)
            {
                _logger.LogWarning("product = null");
                return;
            }

            if (product.Amount > 0)
            {
                product.Amount = product.Amount - 1;

                Models.HistoryNote note = new Models.HistoryNote()
                {
                    Message = $"Продукт {product.Name} был куплен. Пользователь:{request.User.Identity.Name}",
                    Date = DateTime.Now,
                    UserId = _userManager.GetUserId(request.User)
                };

                try
                {
                    await _productRepo.UpdateProduct(product);
                    await _historyNoteRepo.CreateHistoryNote(note);
                    _logger.LogInformation("MainView: product is successfully bought");
                }
                catch (Exception e)
                {
                    _logger.LogError($"MainView: cannot buy product or note that. Reason: {e.Message}");
                }
            }
        }
    }
}
