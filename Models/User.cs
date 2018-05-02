using System;
using System.Collections.Generic;


namespace auction.Models
{
    public class User
    {
        public int userid { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public decimal wallet { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public List<Product> products { get; set; }
        public List<Auction> players { get; set; }


        public User()
        {
            this.created_at = DateTime.Now;
            this.updated_at = DateTime.Now;
            products = new List<Product>();
            players = new List<Auction>();
            wallet = 1000;
        }

    }
}


        // public string order_duration {
        //     get{
        //         double dur = DateTime.Now.Subtract(this.created_at).TotalMinutes;
        //         string final_word = "minute(s)";
        //         if((int)dur >= 60)
        //         {
        //             dur = DateTime.Now.Subtract(this.created_at).TotalHours;
        //             final_word = "hour(s)";
        //         }
        //         else if(dur >= 24)
        //         {
        //             dur = DateTime.Now.Subtract(this.created_at).TotalDays;
        //             final_word = "day(s)";
        //         }
        //         else if(dur >= 7)
        //         {
        //             dur = DateTime.Now.Subtract(this.created_at).TotalDays/7;
        //             final_word = "week(s)";
        //         }
        //         return $"{(int)dur} {final_word} ago";
        //     }