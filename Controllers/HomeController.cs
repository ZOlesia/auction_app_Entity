﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using auction.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace auction.Controllers
{
    public class HomeController : Controller
    {
        private AuctionContext _context;
 
        public HomeController(AuctionContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        [Route("/newUser")]
        public IActionResult CreateUser(RegisterViewModel model)
        {
            var checkEmail = _context.users.SingleOrDefault(e => e.username == model.username);
            
            if(ModelState.IsValid)
            {
                if(checkEmail != null)
                {
                    TempData["error"] = "Email already in use";
                    return View("Index");
                }
                User newUser = new User{
                    first_name = model.first_name,
                    last_name = model.last_name,
                    username = model.username,
                    password = model.password
                };
                PasswordHasher<User> hasher = new PasswordHasher<User>();
                newUser.password = hasher.HashPassword(newUser, newUser.password);
                _context.users.Add(newUser);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("current_userid", newUser.userid);
                // var id = HttpContext.Session.GetInt32("user_id");
                return RedirectToAction("Dashboard", "Auction");
            }
            return View("Index");
        }


        [HttpPost]
        [Route("logIn")]
        public IActionResult SignIn(string loginEmail, string loginPassword)
        {

            var checkEmail = _context.users.SingleOrDefault(e => e.username == loginEmail);
            if(checkEmail==null)
            {
                TempData["email_error"] = "Please check your email otherwie go to register";
                return View("Index");
            }

            if(checkEmail!=null && loginPassword!= null)
            {
                var hasher = new PasswordHasher<User>();
                if(0 != hasher.VerifyHashedPassword(checkEmail, checkEmail.password, loginPassword))
                {
                    HttpContext.Session.SetInt32("current_userid", checkEmail.userid);
                    // var id = HttpContext.Session.GetInt32("userid");
                    return RedirectToAction("Dashboard", "Auction");
                }
            }
            TempData["psw_error"] = "Password is incorrect";
            return RedirectToAction("Index");
        }


        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }

    }
}
