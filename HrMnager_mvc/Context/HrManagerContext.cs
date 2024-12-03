using System;
using HrMnager_mvc.Entities;
using HrMnager_mvc.Enums;
using Microsoft.EntityFrameworkCore;

namespace HrMnager_mvc.Context
{
    public class HrManagerContext : DbContext
    {
      public HrManagerContext(DbContextOptions<HrManagerContext> options) : base(options) { }

      public DbSet<User> Users { get; set; }
      public DbSet<Role> Roles { get; set; }
      public DbSet<HrManager> HrManagers { get; set; }
      public DbSet<Employee> Employees { get; set; }
      public DbSet<Department> Departments { get; set; }
      public DbSet<Request> Requests { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
        base.OnModelCreating(modelBuilder);

        // Seed roles
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "HrManager" },
            new Role { Id = 3, Name = "Employee" }
        );

        modelBuilder.Entity<User>().HasData(
          new User
          {
            Id = 1,
            FullName = "Admin",
            Email = "admin@gmail.com",
            PhoneNumber = "09154682311",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("pass"),
            Salt = "",
            Gender = Gender.Male,
            RoleId = 1,
          }
        );


        modelBuilder.Entity<Department>()
                .HasOne(d => d.HrManager)
                .WithOne(h => h.Department)
                .HasForeignKey<HrManager>(h => h.DepartmentId);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.User)
            .WithOne(u => u.Employee)
            .HasForeignKey<Employee>(e => e.UserId);

        modelBuilder.Entity<HrManager>()
            .HasOne(h => h.User)
            .WithOne(u => u.HrManager)
            .HasForeignKey<User>(u => u.HrManagerId);

        modelBuilder.Entity<User>()
        .HasOne(u => u.HrManager)
        .WithOne(h => h.User)
        .HasForeignKey<HrManager>(h => h.UserId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
                .HasOne(u => u.Employee)
                .WithOne(e => e.User)
                .HasForeignKey<Employee>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Department>()
                .HasOne(d => d.HrManager)
                .WithOne(h => h.Department);

        modelBuilder.Entity<Request>()
          .HasOne(r => r.Employee)
          .WithMany()
          .HasForeignKey(r => r.EmployeeId)
          .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Request>()
            .HasOne(r => r.ApprovedBy)
            .WithMany()
            .HasForeignKey(r => r.ApprovedById)
            .OnDelete(DeleteBehavior.Restrict)  
            .IsRequired(false);

        modelBuilder.Entity<User>()
          .HasOne(u => u.Department)
          .WithMany(d => d.Users)
          .HasForeignKey(u => u.DepartmentId)
          .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HrManager>()
          .HasMany(h => h.Employees)
          .WithOne(e => e.HrManager)
          .HasForeignKey(e => e.HrManagerId)
          .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.HrManager)
            .WithMany(h => h.Employees)
            .HasForeignKey(e => e.HrManagerId);

     }

   }
}