﻿using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utlity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyBookWeb.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productView = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            return View(productView);
        }
        public IActionResult AllProduct()
        {
            IEnumerable<Product> productView = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            return View(productView);
        }
        public IActionResult Details(int productId)
        {
            ShoppingCart objCart = new()
            {
                Count = 1,
                ProductId = productId,
                Product = _unitOfWork.Product.GetFirstOrDeafult(u => u.Id == productId, includeProperties: "Category,CoverType")
            };
            return View(objCart);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claims.Value;

            ShoppingCart shoppinCartDb = _unitOfWork.shoppingCart.GetFirstOrDeafult(
                u => u.ApplicationUserId == claims.Value && u.ProductId == shoppingCart.ProductId);

            if(shoppinCartDb == null)
            {
                _unitOfWork.shoppingCart.Add(shoppingCart);
                _unitOfWork.Save();
                TempData["success"] = "Add Product  to Cart Successfully !";
                HttpContext.Session.SetInt32(SD.SessionCart,
                    _unitOfWork.shoppingCart.GetAll(u => u.ApplicationUserId == claims.Value).ToList().Count);
            }
            else
            {
                _unitOfWork.shoppingCart.Incremment(shoppinCartDb, shoppingCart.Count);
                _unitOfWork.Save();

            }
            

            return RedirectToAction(nameof(Index));
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