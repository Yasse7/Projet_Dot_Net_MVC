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
using System.Security.Claims;

namespace AchatEnLigne.Controllers
{
    [Authorize]
    public class PaniersController : Controller
    {
        private readonly AchatEnLigneContext _context;

        public PaniersController(AchatEnLigneContext context)
        {
            _context = context;
        }

        // GET: Paniers
        public async Task<IActionResult> Index()
        {
            var produit = _context.Produit;
            ViewBag.donnes = produit;
            var Utilisateur = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            if(Utilisateur == "Admin") { return View(await _context.Panier.ToListAsync()); }
            var UtilisateurId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
            return View(await _context.Panier.Where(x=>x.User== Int32.Parse(UtilisateurId)).ToListAsync());
        }
        
        // GET: Paniers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Panier == null)
            {
                return NotFound();
            }

            var panier = await _context.Panier
                .FirstOrDefaultAsync(m => m.Id == id);
            if (panier == null)
            {
                return NotFound();
            }

            return View(panier);
        }
        // GET: Paniers/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Paniers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreArticles,Total")] Panier panier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(panier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(panier);
        }
        // GET: Paniers/Edit/5
        [Authorize(Policy ="AdminOnly")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Panier == null)
            {
                return NotFound();
            }

            var panier = await _context.Panier.FindAsync(id);
            if (panier == null)
            {
                return NotFound();
            }
            return View(panier);
        }
        // POST: Paniers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreArticles,Total")] Panier panier)
        {
            if (id != panier.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(panier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PanierExists(panier.Id))
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
            return View(panier);
        }
        // GET: Paniers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Panier == null)
            {
                return NotFound();
            }

            var panier = await _context.Panier
                .FirstOrDefaultAsync(m => m.Id == id);
            if (panier == null)
            {
                return NotFound();
            }

            return View(panier);
        }
        // POST: Paniers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Panier == null)
            {
                return Problem("Entity set 'AchatEnLigneContext.Panier'  is null.");
            }
            var panier = await _context.Panier.FindAsync(id);
            if (panier != null)
            {
                _context.Panier.Remove(panier);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PanierExists(int id)
        {
          return _context.Panier.Any(e => e.Id == id);
        }
    }
}
