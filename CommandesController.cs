using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AchatEnLigne.Data;
using AchatEnLigne.Models;
using System.Security.Claims;

namespace AchatEnLigne.Controllers
{
    public class CommandesController : Controller
    {
        private readonly AchatEnLigneContext _context;

        public CommandesController(AchatEnLigneContext context)
        {
            _context = context;
        }

        // GET: Commandes
        public async Task<IActionResult> Index()
        {

            var produit = _context.Produit;
            ViewBag.Produit = produit;
            var panier = _context.Panier;
            ViewBag.panier = panier;
            var UtilisateurId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
            var Utilisateur = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            if (Utilisateur == "Admin") { return View(await _context.Commande.ToListAsync()); }
            if(Utilisateur != "Normal") { return Redirect("Logins"); }
            var paniers = _context.Panier.Where(x => x.User == Int32.Parse(UtilisateurId));

            
             return View(await _context.Commande.Where(x=>x.User== Int32.Parse(UtilisateurId)).ToListAsync());
        }

        // GET: Commandes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Commande == null)
            {
                return NotFound();
            }

            var commande = await _context.Commande
                .Include(c => c.Panier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commande == null)
            {
                return NotFound();
            }

            return View(commande);
        }

        // GET: Commandes/Create
        public IActionResult Create()
        {
            ViewData["PanierId"] = new SelectList(_context.Panier, "Id", "Id");
            return View();
        }

        // POST: Commandes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Adresse,PanierId")] Commande commande)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commande);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PanierId"] = new SelectList(_context.Panier, "Id", "Id", commande.PanierId);
            return View(commande);
        }

        // GET: Commandes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Commande == null)
            {
                return NotFound();
            }

            var commande = await _context.Commande.FindAsync(id);
            if (commande == null)
            {
                return NotFound();
            }
            ViewData["PanierId"] = new SelectList(_context.Panier, "Id", "Id", commande.PanierId);
            return View(commande);
        }

        // POST: Commandes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Adresse,PanierId")] Commande commande)
        {
            if (id != commande.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commande);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommandeExists(commande.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PanierId"] = new SelectList(_context.Panier, "Id", "Id", commande.PanierId);
            return View(commande);
        }

        // GET: Commandes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Commande == null)
            {
                return NotFound();
            }

            var commande = await _context.Commande
                .Include(c => c.Panier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commande == null)
            {
                return NotFound();
            }

            return View(commande);
        }

        // POST: Commandes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Commande == null)
            {
                return Problem("Entity set 'AchatEnLigneContext.Commande'  is null.");
            }
            var commande = await _context.Commande.FindAsync(id);
            if (commande != null)
            {
                _context.Commande.Remove(commande);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommandeExists(int id)
        {
          return _context.Commande.Any(e => e.Id == id);
        }
    }
}
