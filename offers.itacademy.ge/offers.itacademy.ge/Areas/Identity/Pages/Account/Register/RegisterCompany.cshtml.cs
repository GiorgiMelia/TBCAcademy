using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Domain.entities;
using offers.itacademy.ge.Application.Interfaces;

namespace offers.itacademy.ge.Web.Areas.Identity.Pages.Account.Register
{
    public class RegisterCompanyModel : PageModel
    {
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly SignInManager<Client> _signInManager;

        public RegisterCompanyModel(IUserRegistrationService userRegistrationService, SignInManager<Client> signInManager)
        {
            _userRegistrationService = userRegistrationService;
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string CompanyName { get; set; }

            public string? Description { get; set; }

            public string? PhotoUrl { get; set; }

            [Required, EmailAddress]
            public string Email { get; set; }

            [Required, DataType(DataType.Password)]
            [StringLength(100, MinimumLength = 3)]
            public string Password { get; set; }

            [Required, DataType(DataType.Password)]
            [Compare("Password")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl,CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return Page();

            var dto = new CreateClientDto
            {
                Email = Input.Email,
                Password = Input.Password,
                UserType = UserType.Company,
                Company = new Company
                {
                    Name = Input.CompanyName,
                    Description = Input.Description,
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
