using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Domain.entities;
using offers.itacademy.ge.Application.Interfaces;

namespace offers.itacademy.ge.Web.Areas.Identity.Pages.Account.Register
{
    public class RegisterBuyerModel : PageModel
    {
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly SignInManager<Client> _signInManager;

        public RegisterBuyerModel(IUserRegistrationService userRegistrationService, SignInManager<Client> signInManager)
        {
            _userRegistrationService = userRegistrationService;
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            [Required]
            public string Name { get; set; } = null!;

            [Required]
            public string Surname { get; set; } = null!;

            [Required]
            public string Address { get; set; } = null!;

            public string? PhotoUrl { get; set; }

            [Required, EmailAddress]
            public string Email { get; set; } = null!;

            [Required, DataType(DataType.Password)]
            [StringLength(100, MinimumLength = 3, ErrorMessage = "Password must be at least 3 characters.")]
            public string Password { get; set; } = null!;

            [Required, DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; } = null!;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return Page();

            var dto = new CreateClientDto
            {
                Email = Input.Email,
                Password = Input.Password,
                UserType = UserType.Buyer,
                Buyer = new Buyer
                {
                    Name = Input.Name,
                    Surname = Input.Surname,
                    Address = Input.Address,
                    PhotoUrl = Input.PhotoUrl
                }
            };

            var result = await _userRegistrationService.Registration(dto);

            if (result.IdentityResult.Succeeded)
            {
                await _signInManager.SignInAsync(result.Client, isPersistent: false);
                return LocalRedirect(returnUrl ?? "/");
            }

            foreach (var error in result.IdentityResult.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return Page();
        }
    }
}