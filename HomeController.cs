using AchatEnLigne.Data;
using AchatEnLigne.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;

namespace AchatEnLigne.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AchatEnLigneContext _context;
        public HomeController(ILogger<HomeController> logger, AchatEnLigneContext context)
        {   
            _logger = logger;
            _context = context;
        }
        
        public async Task<IActionResult> AddPanier(int Id,int Quantité)
        {
            if (Quantité >= 1) { 
            var req = HttpContext.User.Claims;
            foreach (var i in req)
            {
                if (i.Type == ClaimTypes.Sid)
                {
                    var produit = await _context.Produit.FindAsync(Id);
                        Panier panier = new Panier
                        {
                            NombreArticles = Quantité,
                            Total = Quantité * (float)produit.Price,
                            User = int.Parse(i.Value),
                            Produit = Id
                        };
                        _context.Panier.Update(panier);
                     _context.SaveChanges();
                    

                    foreach (var tmp in _context.Panier)
                      {
                      
                        if ((tmp.User == int.Parse(i.Value) )&& tmp.Produit==Id)
                          {
                            if (tmp != panier) { 
                              panier.NombreArticles =panier.NombreArticles+ tmp.NombreArticles;
                              panier.Total =panier.Total+ tmp.Total;
                              _context.Panier.Remove(tmp);
                              _context.Panier.Update(panier);
                            }
                        }

                      }
                    _context.SaveChanges();     
                }
            }
            }
            return Redirect("/Paniers");
        }
        public async Task<ActionResult> AddCommande()
        {
            bool verification= true;
            var UtilisateurId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
            var user = await _context.User.FindAsync(int.Parse(UtilisateurId));
            var random = new Random();
            var value = random.Next();
            var verif = _context.Commande.FirstOrDefault(x => x.NumCommande == value);
            int calc= 0;
            float calc2 = 0;
            while (verif != null)
            {
                random = new Random();
                value = random.Next();
            }
            
            foreach (var y in _context.Panier.Where(x => x.User == int.Parse(UtilisateurId)))
            {
                var produit = await _context.Produit.FindAsync(y.Produit);
                if (y.NombreArticles > produit.Stock)
                {
                    verification= false;
                }
            }
            if (verification) {
                
                foreach (var y in _context.Panier.Where(x=>x.User==int.Parse( UtilisateurId))) {
                var produit = await _context.Produit.FindAsync(y.Produit);
               
               
                        calc += y.NombreArticles;
                        calc2 += y.Total;
                        Commande commande = new Commande()
                        {
                            User = y.User,
                        NombreArticles = y.NombreArticles,
                        produit = y.Produit,
                        
                        Adresse = user.Adresse,
                        NumCommande = value,
                        DateTime = DateTime.Now,
                    };
                produit.Stock -= y.NombreArticles;
                    _context.Panier.Remove(y);
                _context.Produit.Update(produit);
                _context.Commande.Add(commande);
                }
            }

            LignePanierCommande lpc = new LignePanierCommande()
            {
                UserX = int.Parse(UtilisateurId),
                CommandeX = value,
                Qte = calc,
                Total = calc2,
            };

            _context.LignePanierCommande.Add(lpc);
            _context.SaveChanges();
            
            return Redirect("/Commandes");
        }
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.Produit.Where(x=>x.Stock>=1).ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}