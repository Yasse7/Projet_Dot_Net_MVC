using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AchatEnLigne.Data;
using AchatEnLigne.Models;
using Microsoft.AspNetCore.Authorization;

namespace AchatEnLigne.Controllers
{
    [Authorize(Policy ="AdminOnly")]
    public class LignePanierCommandesController : Controller
    {
        private readonly AchatEnLigneContext _context;

        public LignePanierCommandesController(AchatEnLigneContext context)
        {
            _context = context;
        }

        // GET: LignePanierCommandes
        public async Task<IActionResult> Index()
        {
            var commandes = _context.Commande;
            ViewBag.commandes = commandes;
            var produit = _context.Produit;
            ViewBag.Produit = produit;
            var panier = _context.Panier;
            ViewBag.panier = panier;
            var achatEnLigneContext = _context.LignePanierCommande.Include(l => l.Commande).Include(l => l.User);
            return View(await achatEnLigneContext.ToListAsync());
        }

        // GET: LignePanierCommandes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LignePanierCommande == null)
            {
                return NotFound();
            }

            var lignePanierCommande = await _context.LignePanierCommande
                .Include(l => l.Commande)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.LignepanierCommandeId == id);
            if (lignePanierCommande == null)
            {
                return NotFound();
            }

            return View(lignePanierCommande);
        }

        // GET: LignePanierCommandes/Create
        public IActionResult Create()
        {
            ViewData["CommandeId"] = new SelectList(_context.Commande, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: LignePanierCommandes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LignepanierCommandeId,Qte,UserId,CommandeId")] LignePanierCommande lignePanierCommande)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lignePanierCommande);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CommandeId"] = new SelectList(_context.Commande, "Id", "Id", lignePanierCommande.CommandeId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", lignePanierCommande.UserId);
            return View(lignePanierCommande);
        }

        // GET: LignePanierCommandes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LignePanierCommande == null)
            {
                return NotFound();
            }

            var lignePanierCommande = await _context.LignePanierCommande.FindAsync(id);
            if (lignePanierCommande == null)
            {
                return NotFound();
            }
            ViewData["CommandeId"] = new SelectList(_context.Commande, "Id", "Id", lignePanierCommande.CommandeId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", lignePanierCommande.UserId);
            return View(lignePanierCommande);
        }

        // POST: LignePanierCommandes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LignepanierCommandeId,Qte,UserId,CommandeId")] LignePanierCommande lignePanierCommande)
        {
            if (id != lignePanierCommande.LignepanierCommandeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lignePanierCommande);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LignePanierCommandeExists(lignePanierCommande.LignepanierCommandeId))
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
            ViewData["CommandeId"] = new SelectList(_context.Commande, "Id", "Id", lignePanierCommande.CommandeId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", lignePanierCommande.UserId);
            return View(lignePanierCommande);
        }

        // GET: LignePanierCommandes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LignePanierCommande == null)
            {
                return NotFound();
            }

            var lignePanierCommande = await _context.LignePanierCommande
                .Include(l => l.Commande)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.LignepanierCommandeId == id);
            if (lignePanierCommande == null)
            {
                return NotFound();
            }

            return View(lignePanierCommande);
        }

        // POST: LignePanierCommandes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LignePanierCommande == null)
            {
                return Problem("Entity set 'AchatEnLigneContext.LignePanierCommande'  is null.");
            }
            var lignePanierCommande = await _context.LignePanierCommande.FindAsync(id);
            if (lignePanierCommande != null)
            {
                _context.LignePanierCommande.Remove(lignePanierCommande);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LignePanierCommandeExists(int id)
        {
          return _context.LignePanierCommande.Any(e => e.LignepanierCommandeId == id);
        }
    }
}
