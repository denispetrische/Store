// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Store.Web.Abstractions.Data;
using Store.Web.Data;
using Store.Web.Models;

namespace Store.Web.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly IHistoryNoteRepo<HistoryNote> _historyNoteRepo;
        private readonly UserManager<IdentityUser> _userManager;

        public LogoutModel(SignInManager<IdentityUser> signInManager, 
                           ILogger<LogoutModel> logger,
                           IHistoryNoteRepo<HistoryNote> historyNoteRepo,
                           UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _historyNoteRepo = historyNoteRepo;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            HistoryNote note = new HistoryNote()
            {
                Message = $"Пользователь вышел из аккаунта. Пользователь:{User.Identity.Name}",
                Date = DateTime.Now,
                UserId = _userManager.GetUserId(HttpContext.User)
            };

            try
            {
                _historyNoteRepo.CreateHistoryNote(note);
            }
            catch (Exception e)
            {
                _logger.LogError($"Can't add history note while user logout. Reason: {e.Message}");
            }

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.
                return RedirectToPage();
            }
        }
    }
}
