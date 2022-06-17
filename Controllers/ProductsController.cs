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
using System.Security.Claims;

namespace brick_and_mortar_store.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public ProductsController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        // GET: Products
        public async Task<IActionResult> Index(int? pageNumber, string option = null, string search = null)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var applicationDbContext = _context.Products.Include(p => p.Farmers);
            //var model = from s in _context.Products
            //            join m in _context.Farmers on s.FarmersId equals m.FarmersId
            //            join u in _context.Users on s.UserId equals u.Id
            //            select new ProductsViewModel
            //            {
            //                ProductsId = s.ProductsId,
            //                ProductName = s.ProductName,
            //                ProductDescription = s.ProductDescription,
            //                ProductQuantity = s.ProductQuantity,
            //                ProductPicture = s.ProductPicture,
            //                ProductPrice = s.ProductPrice,
            //                DateAdded = s.DateAdded,
            //                Farmers = (from b in _context.Farmers where b.FarmersId == s.FarmersId select b.FarmName).FirstOrDefault(),

            //            };
            //if (option == "Farmer" && search.Length > 0)
            //{
            //    model = model.Where(u => u.Farmers.Contains(search));
            //}
            //if (option == "Products" && search.Length > 0)
            //{
            //    model = model.Where(u => u.ProductName.Contains(search));
            //}
            //return View(await applicationDbContext.ToListAsync());
            if (User.IsInRole("Farmer"))
            {
                //deviceStatuses = db.DeviceStatuses.Include(d => d.RepairStatus).Where(x => x.UserId == userId);
                return View(_context.Products.Where(x => x.UserId == userId).ToList());
            }
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.Farmers)
                .FirstOrDefaultAsync(m => m.ProductsId == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Products/Create
        public IActionResult Create(int id, string ProductName, string FarmersName)
        {
            ViewData["FarmersId"] = new SelectList(_context.Farmers, "FarmersId", "FarmerName");
            ProductsViewModel model = new ProductsViewModel
            {
                ProductName = ProductName,
                ProductsId = id,
                Farmers = FarmersName
            };
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Products products, ProductsViewModel productsViewModel)
        {
            if (ModelState.IsValid)
            {
                products.UserId= User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(products);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FarmersId"] = new SelectList(_context.Farmers, "FarmersId", "FarmerName", products.FarmersId);
            return View(products);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            ViewData["FarmersId"] = new SelectList(_context.Farmers, "FarmersId", "FarmerName", products.FarmersId);
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductsId,ProductName,ProductDescription,ProductQuantity,ProductPrice,ProductPicture,DateAdded,FarmersId")] Products products)
        {
            if (id != products.ProductsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.ProductsId))
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
            ViewData["FarmersId"] = new SelectList(_context.Farmers, "FarmersId", "FarmerName", products.FarmersId);
            return View(products);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.Farmers)
                .FirstOrDefaultAsync(m => m.ProductsId == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.Products.FindAsync(id);
            _context.Products.Remove(products);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.ProductsId == id);
        }
    }
}
