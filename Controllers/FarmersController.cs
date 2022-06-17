using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using brick_and_mortar_store.Data;
using brick_and_mortar_store.Models;
using Microsoft.AspNetCore.Identity;

namespace brick_and_mortar_store.Controllers
{
    public class FarmersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public FarmersController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }


        // GET: Farmers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Farmers.ToListAsync());
        }

        // GET: Farmers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmers = await _context.Farmers
                .FirstOrDefaultAsync(m => m.FarmersId == id);
            if (farmers == null)
            {
                return NotFound();
            }

            return View(farmers);
        }

        // GET: Farmers/Create
        public IActionResult Create()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var role in roleManager.Roles)
                list.Add(new SelectListItem() { Value = role.Name, Text = role.Name });
            ViewBag.Roles = list;
            return View();
        }

        // POST: Farmers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FarmersId,FarmerName,FarmerSurname,FarmerAge,FarmerPhoneNo,FarmerEmail,FarmerPassword,FarmerPicture,FarmName,FarmAadress,role")] Farmers farmers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(farmers);
                await _context.SaveChangesAsync();
                var user = new IdentityUser { UserName = farmers.FarmerEmail, Email = farmers.FarmerEmail };
                var result = await userManager.CreateAsync(user, farmers.FarmerPassword);
                if (result.Succeeded)
                {
                    
                    result = await userManager.AddToRoleAsync(user, farmers.role);
                    await signInManager.SignInAsync(user, isPersistent:false);
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(farmers);
        }

        // GET: Farmers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmers = await _context.Farmers.FindAsync(id);
            if (farmers == null)
            {
                return NotFound();
            }
            return View(farmers);
        }

        // POST: Farmers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FarmersId,FarmerName,FarmerSurname,FarmerAge,FarmerPhoneNo,FarmerEmail,FarmerPassword,FarmerPicture,FarmName,FarmAadress,role")] Farmers farmers)
        {
            if (id != farmers.FarmersId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(farmers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FarmersExists(farmers.FarmersId))
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
            return View(farmers);
        }

        // GET: Farmers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmers = await _context.Farmers
                .FirstOrDefaultAsync(m => m.FarmersId == id);
            if (farmers == null)
            {
                return NotFound();
            }

            return View(farmers);
        }

        // POST: Farmers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var farmers = await _context.Farmers.FindAsync(id);
            _context.Farmers.Remove(farmers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FarmersExists(int id)
        {
            return _context.Farmers.Any(e => e.FarmersId == id);
        }
    }
}
