using System;
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
    public class AuctionController : Controller
    {
        private AuctionContext _context;
 
        public AuctionController(AuctionContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("auctions")]
        public IActionResult Dashboard()
        {
            ViewBag.user = HttpContext.Session.GetInt32("current_userid");
            ViewBag.current_user = _context.users.SingleOrDefault(f=>f.userid == HttpContext.Session.GetInt32("current_userid"));
            var all = _context.products.Include(d => d.seller).Include(v => v.auctions).ThenInclude(c =>c.user);

             ViewBag.all_auctions = all;


            foreach(var i in all)
            {
                if(i.end_date == DateTime.Now)
                {
                    var money = _context.auctions.Include(f => f.user).ThenInclude(d => d.products).SingleOrDefault(a => a.productid == i.productid);
                    // i.seller.wallet -= i.auctions.
                }
            }


            return View("dashboard");
        }


        [HttpGet]
        [Route("new/auction")]
        public IActionResult NewAuction()
        {
            return View("newauction");
        }

        [HttpPost]
        [Route("create/auction")]
        public IActionResult CreateAuction(FormViewModel model)
        {
            if(model.bid <= 0)
            {
                TempData["bid_error"] = "Starting bid must be higher than 0!";
                return View("newauction");
            }
            if(model.end_date <= DateTime.Now)
            {
                TempData["date_error"] = "Only future dates!";
                return View("newauction");
            }
            if(ModelState.IsValid)
            {
                Product newProduct = new Product{
                    name = model.name,
                    description = model.description,
                    end_date = model.end_date,
                    bid = model.bid,
                    sellerid = (int)HttpContext.Session.GetInt32("current_userid")
                };
                _context.products.Add(newProduct);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return View("newauction");
        }


        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _context.Remove(_context.products.SingleOrDefault(c => c.productid == id));
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("auction/{id}")]
        public IActionResult Show(int id)
        {
            ViewBag.product = _context.products.Include(c => c.seller).Include(d => d.auctions).ThenInclude(s => s.user).SingleOrDefault(e => e.productid == id);

            ViewBag.auct = _context.auctions.Include(n => n.user).SingleOrDefault(t => t.productid == id);
            return View("auction");
        }

        [HttpPost]
        [Route("bid/{id}")]
        public IActionResult Bid(int id, decimal bid)
        {

            var auct = _context.auctions.Include(m => m.user).SingleOrDefault(s => s.productid == id);
            var amount = _context.products.Include(u => u.seller).SingleOrDefault(x => x.productid == id);
            var user_wallet = _context.users.SingleOrDefault(g => g.userid == HttpContext.Session.GetInt32("current_userid")).wallet;

            // if(amount.end_date == DateTime.Now)
            // {
            //     auct.user.wallet = auct.user.wallet - amount.bid;
            //     user_wallet = auct.user.wallet + amount.bid;
            //     _context.SaveChanges();
            // }






            if(auct == null)
            {
                if(bid <= amount.bid)
                {
                    TempData["err"] = "Bid must be higher the bid you are trying ti bid!";
                    return RedirectToAction("Show", new{id = id});
                }
                if(bid > user_wallet)
                {
                    TempData["err_wallet"] = "You have not enough money";
                    return RedirectToAction("Show", new{id = id});
                }
                Auction newBid = new Auction{
                    userid = (int)HttpContext.Session.GetInt32("current_userid"),
                    productid = id,
                };
                _context.auctions.Add(newBid);
                var new_bid = _context.products.SingleOrDefault(j => j.productid == id);
                new_bid.bid = bid;
                _context.SaveChanges();
            }
            else
            {
                if(bid <= amount.bid)
                {
                    TempData["err"] = "Bid must be higher the bid you are trying ti bid!";
                    return RedirectToAction("Show", new{id = id});
                }
                if(bid > user_wallet)
                {
                    TempData["err_wallet"] = "You have not enough money";
                    return RedirectToAction("Show", new{id = id});
                }
                var auction = _context.auctions.SingleOrDefault(v => v.productid == id);
                auction.userid = (int)HttpContext.Session.GetInt32("current_userid");
                var new_bid = _context.products.SingleOrDefault(j => j.productid == id);
                new_bid.bid = bid;
                _context.SaveChanges();
            }
            return RedirectToAction("Show", new{id = id});
        }
    }
}
