using System;
using System.Collections.Generic;


namespace auction.Models
{
    public class Product
    {

        public int productid { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime end_date { get; set; }
        public decimal bid { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public int sellerid { get; set; }
        public User seller { get; set; }
        public List<Auction> auctions { get; set; }

        public Product()
        {
            this.created_at = DateTime.Now;
            this.updated_at = DateTime.Now;
            auctions = new List<Auction>();

        }

        public string remaining_time{
            get{
                double time = this.end_date.Subtract(DateTime.Now).TotalDays;
                return $"{(int)time} day(s)";
            }
        }

    }
}
