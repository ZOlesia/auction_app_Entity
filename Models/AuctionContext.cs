using Microsoft.EntityFrameworkCore;

namespace auction.Models
{
    public class AuctionContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public AuctionContext(DbContextOptions<AuctionContext> options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Auction> auctions { get; set; }
    }
}