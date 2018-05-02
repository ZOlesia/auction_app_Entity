using System;
using System.Collections.Generic;


namespace auction.Models
{
    public class Auction
    {

        public int auctionid { get; set; }
        public int userid { get; set; }
        public User user { get; set; }
        public int productid { get; set; }
        public Product product { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public Auction()
        {
            this.created_at = DateTime.Now;
            this.updated_at = DateTime.Now;
        }
    }
}
