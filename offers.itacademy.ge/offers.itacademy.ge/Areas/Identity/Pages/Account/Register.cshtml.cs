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
using offers.itacademy.ge.Web.Models;
using offers.itacademy.ge.Persistance.Data;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Domain.entities;
using offers.itacademy.ge.Web.Areas.Identity.Pages.Account;

namespace offers.itacademy.ge.Web.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
