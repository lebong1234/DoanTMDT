using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Webbanson.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Webbanson.Models; // Ensure this namespace includes the Product model

namespace Webbanson.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ComesticsContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(ComesticsContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Supplier);
            return View(await products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            PopulateDropdowns();
            if (TempData["ProductData"] != null)
            {
                var productData = TempData["ProductData"].ToString();
                var product = System.Text.Json.JsonSerializer.Deserialize<Product>(productData);
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
                return View(product);
            }

            return View(new Product());
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Description,StockQuantity,OriginalPrice,DiscountedPrice,Status,CategoryId,SupplierId,BrandId")] Product product, IFormFile imageFile)
        {
            if (string.IsNullOrEmpty(product.ProductName))
            {
                TempData["ErrorMessage"] = "Product Name is required.";
            }
            else if (imageFile == null || imageFile.Length == 0)
            {
                TempData["ErrorMessage"] = "Please select an image file.";
            }
            else if (!ValidateImageFile(imageFile))
            {
                // Error message is set inside ValidateImageFile()
            }
            else
            {
                try
                {
                    product.ImageUrl = await UploadImage(imageFile);
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Product created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Error creating product: {ex.Message}";
                }
            }

            TempData["ProductData"] = System.Text.Json.JsonSerializer.Serialize(product);
            return RedirectToAction(nameof(Create));
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            PopulateDropdowns(product);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProductId,ProductName,Description,StockQuantity,OriginalPrice,DiscountedPrice,Status,ImageUrl,CategoryId,SupplierId,BrandId")] Product product, IFormFile imageFile)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (imageFile != null && imageFile.Length > 0 && ValidateImageFile(imageFile))
            {
                try
                {
                    if (!string.IsNullOrEmpty(product.ImageUrl))
                    {
                        DeleteImage(product.ImageUrl);
                    }
                    product.ImageUrl = await UploadImage(imageFile);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error updating image: {ex.Message}");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Product updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            PopulateDropdowns(product);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    DeleteImage(product.ImageUrl);
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Product deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(string id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }

        private void PopulateDropdowns(Product product = null)
        {
            ViewBag.BrandId = new SelectList(_context.Brands, "BrandId", "BrandName", product?.BrandId);
            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName", product?.CategoryId);
            ViewBag.SupplierId = new SelectList(_context.Suppliers, "SupplierId", "SupplierName", product?.SupplierId);
        }

        private bool ValidateImageFile(IFormFile imageFile)
        {
            long maxFileSize = 5 * 1024 * 1024; // 5MB
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

            if (imageFile.Length > maxFileSize)
            {
                TempData["ErrorMessage"] = "File size must be less than 5MB.";
                return false;
            }

            if (!allowedExtensions.Contains(fileExtension))
            {
                TempData["ErrorMessage"] = "Only .jpg, .jpeg, and .png files are allowed.";
                return false;
            }

            return true;
        }

        private async Task<string> UploadImage(IFormFile imageFile)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "products");
            Directory.CreateDirectory(uploadsFolder);
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return uniqueFileName;
        }

        private void DeleteImage(string imageUrl)
        {
            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "products", imageUrl);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }
    }
}