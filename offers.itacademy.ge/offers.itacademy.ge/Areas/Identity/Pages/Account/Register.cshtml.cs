#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using offers.itacademy.ge.Web.Areas.Identity.Pages.Account;
using offers.itacademy.ge.Web.Models;
using offers.itacademy.ge.Domain.entities;
using offers.itacademy.ge.Persistance.Data;
using offers.itacademy.ge.Application.services;
using offers.itacademy.ge.Application.Dtos;

namespace offers.itacademy.ge.Web.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserRegistrationService _userRegistrationService;
        private readonly SignInManager<Client> _signInManager;
        private readonly UserManager<Client> _userManager;
        private readonly IUserStore<Client> _userStore;
        private readonly IUserEmailStore<Client> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly ApplicationDbContext _context;

        public RegisterModel(
            UserManager<Client> userManager,
            IUserStore<Client> userStore,
            SignInManager<Client> signInManager,
            ILogger<RegisterModel> logger,
            ApplicationDbContext context,
            UserRegistrationService userRegistrationService)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            _userRegistrationService = userRegistrationService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Registering as")]
            public UserType UserType { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                CreateClientDto createClientDto = new CreateClientDto
                {
                    Email = Input.Email,
                    Password = Input.Password,
                    UserType = Input.UserType,

                };
                var result = await _userRegistrationService.Registration(createClientDto);

                if (result.IdentityResult.Succeeded)
                {
                    await _signInManager.SignInAsync(result.Client, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.IdentityResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }  



        private IUserEmailStore<Client> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<Client>)_userStore;
        }
    }
}
