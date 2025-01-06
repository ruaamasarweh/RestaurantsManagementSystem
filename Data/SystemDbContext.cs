using Gp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Gp.Data
{
    public class SystemDbContext : DbContext
    {
        public SystemDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeType> EmployeeType { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Dish> Dish { get; set; }
        public DbSet<DishCategory> DishCategory { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<ReservationStatus> ReservationStatus { get; set; }
        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserType> UserType { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Favorite> Favorite { get; set; }
        public DbSet<ReservationDish> ReservationDish { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>()
               .HasOne(r => r.Branch)
               .WithMany(b => b.Reservations)
               .HasForeignKey(r => r.BranchID)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserID)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.ReservationStatus)
                .WithMany(rs => rs.Reservations)
                .HasForeignKey(r => r.ReservationStatusID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DishCategory>()
                .HasOne(d => d.Restaurant)
                .WithMany(r => r.DishCategories)
                .HasForeignKey(d => d.RestaurantID)
                .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Review>()
            //    .HasOne(r => r.Branch)
            //    .WithMany()
            //    .HasForeignKey(r => r.BranchID)
            //    .OnDelete(DeleteBehavior.Cascade); 

            //modelBuilder.Entity<Review>()
            //    .HasOne(r => r.User)
            //    .WithMany() 
            //    .HasForeignKey(r => r.UserID)
            //    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Branch)
                .WithMany(b => b.Favorites)
                .HasForeignKey(f => f.BranchID)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReservationDish>()
                .HasOne(rd => rd.Reservation)
                .WithMany(r => r.ReservationDishes)
                .HasForeignKey(rd => rd.ID_Reservation);

            modelBuilder.Entity<ReservationDish>()
                .HasOne(rd => rd.Dish)
                .WithMany(d => d.ReservationDishes)
                .HasForeignKey(rd => rd.ID_Dish);


            modelBuilder.Entity<Order>()
                .HasOne(o => o.ReservationDish)
                .WithMany(rd => rd.Orders)
                .HasForeignKey(o => o.ID_Reservation_Dish);

        }
        

    }

}
