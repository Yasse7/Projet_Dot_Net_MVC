using AchatEnLigne.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using AchatEnLigne.Data;
using Microsoft.AspNetCore.Identity;

namespace AchatEnLigne.Controllers
{
    public class LoginsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AchatEnLigneContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public LoginsController(ILogger<HomeController> logger, AchatEnLigneContext context, IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
            _logger = logger;
            _context=context;
        }
        public async Task<IActionResult> OnPost(string username, string password)
        {
            List<User> users = await _context.User.ToListAsync();
            foreach (var i in users)
                if (username == i.Username && _passwordHasher.VerifyHashedPassword(i, i.Password, password) == PasswordVerificationResult.Success)
                {
                    
                    List<Claim> claims = new List<Claim> {
                        new Claim(ClaimTypes.Role,i.Role),
                        new Claim(ClaimTypes.Sid,i.Id.ToString()),
                        new Claim(ClaimTypes.Locality,i.Adresse)
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Login");
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new
                  ClaimsPrincipal(claimsIdentity));
                    return Redirect("/Home");
                }
            return Redirect("/Logins");
        }
        public IActionResult Inscription()
        {
            return View();
        }
        public async Task<IActionResult> InscriptionUser(string Adresse,string username,string password )
        {
            bool exists = false;
            List<User> users = await _context.User.ToListAsync();
            foreach (var i in users)
            {
                if (username == i.Username)
                {
                    exists = true;
                }
            }

            if (exists == false)
            {
                User user = new() {
                    Username = username,
                    Adresse = Adresse,
                    
                    Role="Normal" 
            };
                var hashedPassword = _passwordHasher.HashPassword(user, password);
                user.Password = hashedPassword;
                _context.User.Add(user);
                _context.SaveChanges();
            }
            return Redirect("/Logins/Inscription");
        }
        public IActionResult OnGet()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("/Produits");
            }
            return Redirect("/Home");
        }
        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Home");
        }
        public IActionResult Index()
        {
            return View();
        }

      
    }
    
    }

