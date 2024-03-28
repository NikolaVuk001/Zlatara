// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Zlatara.Data;
using Zlatara.Models;

namespace Zlatara.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityRadnik> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly ZlataraContext _baza;

        public LogoutModel(SignInManager<IdentityRadnik> signInManager, ILogger<LogoutModel> logger, ZlataraContext baza)
        {
            _signInManager = signInManager;
            _logger = logger;
            _baza = baza;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            //Brisanje Korpe
            var UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<Korpa> Korpa = _baza.Korpas.Where(k => k.RadnikUser == UserID).ToList();
            _baza.Korpas.RemoveRange(Korpa);
            _baza.SaveChanges();
            
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
