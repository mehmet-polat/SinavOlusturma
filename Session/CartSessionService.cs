﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using DreamsBytes.SuperMarket.Enities.Concrete;
//using DreamsBytes.SuperMarket.WebUI.ExtensionMethods;
//using Microsoft.AspNetCore.Http;

//namespace DreamsBytes.SuperMarket.WebUI.Services
//{
//    public class CartSessionService : ICartSessionService
//    {
//        private IHttpContextAccessor _httpContextAccessor;

//        public CartSessionService(IHttpContextAccessor httpContextAccessor)
//        {
//            _httpContextAccessor = httpContextAccessor;
//        }

//        public Cart GetCart()
//        {
//            Cart cartToCheck = _httpContextAccessor.HttpContext.Session.GetObject<Cart>("cart");
//            if (cartToCheck == null)
//            {
//                _httpContextAccessor.HttpContext.Session.SetObjects("cart",new Cart());
//                cartToCheck = _httpContextAccessor.HttpContext.Session.GetObject<Cart>("cart");
//            }

//            return cartToCheck;
//        }

//        public void SetCart(Cart cart)
//        {
//            _httpContextAccessor.HttpContext.Session.SetObjects("cart", cart);
//        }
//    }
//}
