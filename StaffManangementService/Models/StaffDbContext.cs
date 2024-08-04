using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StaffManagement.Service.Models;

public partial class StaffDbContext : DbContext
{
    public StaffDbContext()
    {
    }

    public StaffDbContext(DbContextOptions<StaffDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Staff> Staff { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=CMTR-62001765;initial catalog=StaffManagement;persist security info=True;user id=sa;password=Adm1n@12345;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK_Staff_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
