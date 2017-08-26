using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OSM.Model.Entities;
using System.Linq;

namespace OSM.Data
{
    public class AppsDbContext : IdentityDbContext<ApplicationUser>
    {
        #region Database Object
        public DbSet<ContactDetail> ContactDetails { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Footer> Footers { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuGroup> MenuGroups { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<SupportOnline> SupportOnlines { get; set; }
        public DbSet<SystemConfig> SystemConfigs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<VisitorStatistic> VisitorStatistics { get; set; }
        #endregion

        public AppsDbContext(DbContextOptions options) : base(options)
        {

        }

        //This will select your start-up project migrations assembly to OSM.Data
        //protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        //{
        //    string connection = "Data Source=USER\\SQLEXPRESS;Initial Catalog=OSM;Integrated Security=True;Pooling=False";
        //    optionBuilder.UseSqlServer(connection, x => x.MigrationsAssembly("OSM.Data"));
        //}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Select relationship from multiple collection
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(builder);

            //Composite keys can only be configured using the Fluent API
            builder.Entity<ProductTag>()
               .HasKey(c => new { c.TagID, c.ProductID });

            builder.Entity<PostTag>()
                .HasKey(c => new { c.TagID, c.PostID });

            builder.Entity<OrderDetail>()
                .HasKey(c => new { c.OrderID, c.ProductID });
     
        }
    }
}
