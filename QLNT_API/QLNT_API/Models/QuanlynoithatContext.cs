using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QLNT_API.Models;

public partial class QuanlynoithatContext : DbContext
{
    public QuanlynoithatContext()
    {
    }

    public QuanlynoithatContext(DbContextOptions<QuanlynoithatContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Banner> Banners { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CategoryChild> CategoryChildren { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<EmailConfirmation> EmailConfirmations { get; set; }

    public virtual DbSet<Extension> Extensions { get; set; }

    public virtual DbSet<InforCompany> InforCompanies { get; set; }

    public virtual DbSet<Introduction> Introductions { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderdetail> Orderdetails { get; set; }

    public virtual DbSet<Partner> Partners { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductExtension> ProductExtensions { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductMaterial> ProductMaterials { get; set; }

    public virtual DbSet<Slide> Slides { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=LAPTOP-SSHUBNQA\\SQLEXPRESS;Database=QUANLYNOITHAT;uid=sa;pwd=123456;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Banner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BANNER__3214EC274D7E8BD5");

            entity.ToTable("BANNER");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("CREATED_DATE");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("IMAGE");
            entity.Property(e => e.Isdelete).HasColumnName("ISDELETE");
            entity.Property(e => e.Orders).HasColumnName("ORDERS");
            entity.Property(e => e.Status).HasColumnName("STATUS");
            entity.Property(e => e.SubTitle)
                .HasMaxLength(255)
                .HasColumnName("SUB_TITLE");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("TITLE");
            entity.Property(e => e.Type)
                .HasMaxLength(500)
                .HasColumnName("TYPE");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("UPDATED_DATE");
            entity.Property(e => e.Urls)
                .HasMaxLength(255)
                .HasColumnName("URLS");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PRODUCT___A88186B13985BFD1");

            entity.ToTable("CATEGORY");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("CREATED_DATE");
            entity.Property(e => e.Icon)
                .HasMaxLength(200)
                .HasColumnName("ICON");
            entity.Property(e => e.Isdelete).HasColumnName("ISDELETE");
            entity.Property(e => e.MetaDescription)
                .HasMaxLength(500)
                .HasColumnName("META_DESCRIPTION");
            entity.Property(e => e.MetaKeyword)
                .HasMaxLength(500)
                .HasColumnName("META_KEYWORD");
            entity.Property(e => e.MetaTitle)
                .HasMaxLength(500)
                .HasColumnName("META_TITLE");
            entity.Property(e => e.Orders).HasColumnName("ORDERS");
            entity.Property(e => e.Parentid).HasColumnName("PARENTID");
            entity.Property(e => e.Slug)
                .HasMaxLength(500)
                .HasColumnName("SLUG");
            entity.Property(e => e.Status).HasColumnName("STATUS");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("TITLE");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("UPDATED_DATE");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.Parentid)
                .HasConstraintName("FK_CATEGORY_CATEGORY");
        });

        modelBuilder.Entity<CategoryChild>(entity =>
        {
            entity.ToTable("CATEGORY_CHILD");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");
            entity.Property(e => e.Title)
                .HasMaxLength(250)
                .HasColumnName("TITLE");

            entity.HasOne(d => d.Category).WithMany(p => p.CategoryChildren)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CATEGORY_CHILD_CATEGORY");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.ToTable("CONTACT");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Content)
                .HasColumnType("ntext")
                .HasColumnName("CONTENT");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("CREATED_DATE");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Isdelete).HasColumnName("ISDELETE");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("PHONE");
            entity.Property(e => e.Status).HasColumnName("STATUS");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("TITLE");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("UPDATED_DATE");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("CUSTOMER");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(250)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Avatar)
                .HasMaxLength(250)
                .HasColumnName("AVATAR");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("CREATED_DATE");
            entity.Property(e => e.Fullname)
                .HasMaxLength(250)
                .HasColumnName("FULLNAME");
            entity.Property(e => e.Isactive)
                .HasDefaultValue((byte)1)
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.Isdelete)
                .HasDefaultValue((byte)0)
                .HasColumnName("ISDELETE");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Customers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CUSTOMER_AspNetUsers");
        });

        modelBuilder.Entity<EmailConfirmation>(entity =>
        {
            entity.ToTable("EmailConfirmation");

            entity.Property(e => e.Id).HasMaxLength(50);
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.CreateAt).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.EmailConfirmations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_EmailConfirmation_AspNetUsers");
        });

        modelBuilder.Entity<Extension>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PRODUCT___EXTENSION");

            entity.ToTable("EXTENSION");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AdminCreated)
                .HasMaxLength(255)
                .HasColumnName("ADMIN_CREATED");
            entity.Property(e => e.AdminUpdated)
                .HasMaxLength(255)
                .HasColumnName("ADMIN_UPDATED");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("CREATED_DATE");
            entity.Property(e => e.Icon)
                .HasMaxLength(200)
                .HasColumnName("ICON");
            entity.Property(e => e.Isdelete).HasColumnName("ISDELETE");
            entity.Property(e => e.MetaDescription)
                .HasMaxLength(500)
                .HasColumnName("META_DESCRIPTION");
            entity.Property(e => e.MetaKeyword)
                .HasMaxLength(500)
                .HasColumnName("META_KEYWORD");
            entity.Property(e => e.MetaTitle)
                .HasMaxLength(500)
                .HasColumnName("META_TITLE");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("NOTES");
            entity.Property(e => e.Orders).HasColumnName("ORDERS");
            entity.Property(e => e.Parentid).HasColumnName("PARENTID");
            entity.Property(e => e.Slug)
                .HasMaxLength(500)
                .HasColumnName("SLUG");
            entity.Property(e => e.Status).HasColumnName("STATUS");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("TITLE");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("UPDATED_DATE");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.Parentid)
                .HasConstraintName("FK_EXTENSION_EXTENSION");
        });

        modelBuilder.Entity<InforCompany>(entity =>
        {
            entity.ToTable("INFOR_COMPANY");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.CreatedDate).HasColumnName("CREATED_DATE");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Facebook)
                .HasMaxLength(500)
                .HasColumnName("FACEBOOK");
            entity.Property(e => e.Instagram)
                .HasMaxLength(500)
                .HasColumnName("INSTAGRAM");
            entity.Property(e => e.Logo)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("LOGO");
            entity.Property(e => e.MetaDescription)
                .HasMaxLength(500)
                .HasColumnName("META_DESCRIPTION");
            entity.Property(e => e.MetaKeyword)
                .HasMaxLength(500)
                .HasColumnName("META_KEYWORD");
            entity.Property(e => e.MetaTitle)
                .HasMaxLength(500)
                .HasColumnName("META_TITLE");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("NAME");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("PHONE");
            entity.Property(e => e.UpdateDate).HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Youtube)
                .HasMaxLength(500)
                .HasColumnName("YOUTUBE");
        });

        modelBuilder.Entity<Introduction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__INTRODUC__3214EC2715DFAD94");

            entity.ToTable("INTRODUCTIONS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Content)
                .HasColumnType("ntext")
                .HasColumnName("CONTENT");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("CREATED_DATE");
            entity.Property(e => e.Description).HasColumnName("DESCRIPTION");
            entity.Property(e => e.Home).HasColumnName("HOME");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("IMAGE");
            entity.Property(e => e.Isdelete).HasColumnName("ISDELETE");
            entity.Property(e => e.MetaDescription)
                .HasMaxLength(4000)
                .HasColumnName("META_DESCRIPTION");
            entity.Property(e => e.MetaKeyword)
                .HasMaxLength(500)
                .HasColumnName("META_KEYWORD");
            entity.Property(e => e.MetaTitle)
                .HasMaxLength(500)
                .HasColumnName("META_TITLE");
            entity.Property(e => e.Orders).HasColumnName("ORDERS");
            entity.Property(e => e.Parentid).HasColumnName("PARENTID");
            entity.Property(e => e.Slug)
                .HasMaxLength(500)
                .HasColumnName("SLUG");
            entity.Property(e => e.Status).HasColumnName("STATUS");
            entity.Property(e => e.Title)
                .HasMaxLength(300)
                .HasColumnName("TITLE");
            entity.Property(e => e.Type).HasColumnName("TYPE");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("UPDATED_DATE");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MATERIAL___EXTENSION");

            entity.ToTable("MATERIAL");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AdminCreated)
                .HasMaxLength(255)
                .HasColumnName("ADMIN_CREATED");
            entity.Property(e => e.AdminUpdated)
                .HasMaxLength(255)
                .HasColumnName("ADMIN_UPDATED");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("CREATED_DATE");
            entity.Property(e => e.Icon)
                .HasMaxLength(200)
                .HasColumnName("ICON");
            entity.Property(e => e.Isdelete).HasColumnName("ISDELETE");
            entity.Property(e => e.MetaDescription)
                .HasMaxLength(500)
                .HasColumnName("META_DESCRIPTION");
            entity.Property(e => e.MetaKeyword)
                .HasMaxLength(500)
                .HasColumnName("META_KEYWORD");
            entity.Property(e => e.MetaTitle)
                .HasMaxLength(500)
                .HasColumnName("META_TITLE");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("NOTES");
            entity.Property(e => e.Orders).HasColumnName("ORDERS");
            entity.Property(e => e.Parentid).HasColumnName("PARENTID");
            entity.Property(e => e.Slug)
                .HasMaxLength(500)
                .HasColumnName("SLUG");
            entity.Property(e => e.Status).HasColumnName("STATUS");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("TITLE");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("UPDATED_DATE");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.Parentid)
                .HasConstraintName("FK_MATERIAL_MATERIAL");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NEWS__3214EC27E639B94D");

            entity.ToTable("NEWS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("CODE");
            entity.Property(e => e.Content)
                .HasColumnType("ntext")
                .HasColumnName("CONTENT");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("CREATED_DATE");
            entity.Property(e => e.Description).HasColumnName("DESCRIPTION");
            entity.Property(e => e.Image).HasColumnName("IMAGE");
            entity.Property(e => e.Isdelete).HasColumnName("ISDELETE");
            entity.Property(e => e.Likes).HasColumnName("LIKES");
            entity.Property(e => e.MainKeyword)
                .HasMaxLength(500)
                .HasColumnName("MAIN_KEYWORD");
            entity.Property(e => e.MetaDescription)
                .HasMaxLength(500)
                .HasColumnName("META_DESCRIPTION");
            entity.Property(e => e.MetaKeyword)
                .HasMaxLength(500)
                .HasColumnName("META_KEYWORD");
            entity.Property(e => e.MetaTitle)
                .HasMaxLength(500)
                .HasColumnName("META_TITLE");
            entity.Property(e => e.Slug)
                .HasMaxLength(500)
                .HasColumnName("SLUG");
            entity.Property(e => e.Star).HasColumnName("STAR");
            entity.Property(e => e.Status).HasColumnName("STATUS");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("TITLE");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("UPDATED_DATE");
            entity.Property(e => e.Views).HasColumnName("VIEWS");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("ORDERS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address).HasColumnName("ADDRESS");
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .HasColumnName("EMAIL");
            entity.Property(e => e.IdCustomer).HasColumnName("ID_CUSTOMER");
            entity.Property(e => e.IdOrders).HasColumnName("ID_ORDERS");
            entity.Property(e => e.IdPayment).HasColumnName("ID_PAYMENT");
            entity.Property(e => e.Isactive).HasColumnName("ISACTIVE");
            entity.Property(e => e.Isdelete).HasColumnName("ISDELETE");
            entity.Property(e => e.NameReciver)
                .HasMaxLength(250)
                .HasColumnName("NAME_RECIVER");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("NOTES");
            entity.Property(e => e.OrdersDate)
                .HasColumnType("datetime")
                .HasColumnName("ORDERS_DATE");
            entity.Property(e => e.Phone)
                .HasMaxLength(250)
                .HasColumnName("PHONE");
            entity.Property(e => e.TotalMoney)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("TOTAL_MONEY");

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdCustomer)
                .HasConstraintName("FK_ORDERS_CUSTOMER");

            entity.HasOne(d => d.IdOrdersNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdOrders)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ORDERS_ORDERDETAILS");
        });

        modelBuilder.Entity<Orderdetail>(entity =>
        {
            entity.ToTable("ORDERDETAILS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdOrder).HasColumnName("ID_ORDER");
            entity.Property(e => e.IdProduct).HasColumnName("ID_PRODUCT");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("PRICE");
            entity.Property(e => e.Qty).HasColumnName("QTY");
            entity.Property(e => e.ReturnQty).HasColumnName("RETURN_QTY");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("TOTAL");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.Orderdetails)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK_ORDERDETAILS_PRODUCT");
        });

        modelBuilder.Entity<Partner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PARTNER__3214EC27EF40CBBF");

            entity.ToTable("PARTNER");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("CREATED_DATE");
            entity.Property(e => e.Description).HasColumnName("DESCRIPTION");
            entity.Property(e => e.Isdelete).HasColumnName("ISDELETE");
            entity.Property(e => e.Logo)
                .HasMaxLength(255)
                .HasColumnName("LOGO");
            entity.Property(e => e.Orders).HasColumnName("ORDERS");
            entity.Property(e => e.Status).HasColumnName("STATUS");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("TITLE");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("UPDATED_DATE");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.ToTable("PAYMENT_METHOD");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("CREATED_DATE");
            entity.Property(e => e.Isactive).HasColumnName("ISACTIVE");
            entity.Property(e => e.Isdelete).HasColumnName("ISDELETE");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .HasColumnName("NAME");
            entity.Property(e => e.Notes)
                .HasMaxLength(250)
                .HasColumnName("NOTES");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("UPDATED_DATE");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PRODUCT__3214EC27E639B94D");

            entity.ToTable("PRODUCT");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cid).HasColumnName("CID");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("CODE");
            entity.Property(e => e.Content)
                .HasColumnType("ntext")
                .HasColumnName("CONTENT");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("CREATED_DATE");
            entity.Property(e => e.Description).HasColumnName("DESCRIPTION");
            entity.Property(e => e.Home).HasColumnName("HOME");
            entity.Property(e => e.Hot).HasColumnName("HOT");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("IMAGE");
            entity.Property(e => e.Isdelete).HasColumnName("ISDELETE");
            entity.Property(e => e.Likes).HasColumnName("LIKES");
            entity.Property(e => e.MetaDescription)
                .HasMaxLength(500)
                .HasColumnName("META_DESCRIPTION");
            entity.Property(e => e.MetaKeyword)
                .HasMaxLength(500)
                .HasColumnName("META_KEYWORD");
            entity.Property(e => e.MetaTitle)
                .HasMaxLength(500)
                .HasColumnName("META_TITLE");
            entity.Property(e => e.PriceNew)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("PRICE_NEW");
            entity.Property(e => e.PriceOld)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("PRICE_OLD");
            entity.Property(e => e.Size)
                .HasMaxLength(500)
                .HasColumnName("SIZE");
            entity.Property(e => e.Slug)
                .HasMaxLength(500)
                .HasColumnName("SLUG");
            entity.Property(e => e.Star).HasColumnName("STAR");
            entity.Property(e => e.Status).HasColumnName("STATUS");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("TITLE");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("UPDATED_DATE");
            entity.Property(e => e.Views).HasColumnName("VIEWS");

            entity.HasOne(d => d.CidNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Cid)
                .HasConstraintName("FK_PRODUCT_CATEGORY");
        });

        modelBuilder.Entity<ProductExtension>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PRODUCTEXTENSIONS");

            entity.ToTable("PRODUCT_EXTENSIONS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Content)
                .HasColumnType("ntext")
                .HasColumnName("CONTENT");
            entity.Property(e => e.Eid).HasColumnName("EID");
            entity.Property(e => e.Pid).HasColumnName("PID");

            entity.HasOne(d => d.EidNavigation).WithMany(p => p.ProductExtensions)
                .HasForeignKey(d => d.Eid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PRODUCT_EXTENSIONS_EXTENSION");

            entity.HasOne(d => d.PidNavigation).WithMany(p => p.ProductExtensions)
                .HasForeignKey(d => d.Pid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PRODUCT_EXTENSIONS_PRODUCT");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PRODUCTIMAGES");

            entity.ToTable("PRODUCT_IMAGES");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Image)
                .HasMaxLength(500)
                .HasColumnName("IMAGE");
            entity.Property(e => e.Pid).HasColumnName("PID");

            entity.HasOne(d => d.PidNavigation).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.Pid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PRODUCT_IMAGES_PRODUCT");
        });

        modelBuilder.Entity<ProductMaterial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PRODUCTMATERIALS");

            entity.ToTable("PRODUCT_MATERIALS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Mid).HasColumnName("MID");
            entity.Property(e => e.Pid).HasColumnName("PID");

            entity.HasOne(d => d.MidNavigation).WithMany(p => p.ProductMaterials)
                .HasForeignKey(d => d.Mid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PRODUCT_MATERIALS_MATERIAL");

            entity.HasOne(d => d.PidNavigation).WithMany(p => p.ProductMaterials)
                .HasForeignKey(d => d.Pid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PRODUCT_MATERIALS_PRODUCT");
        });

        modelBuilder.Entity<Slide>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SLIDES__3214EC274205760A");

            entity.ToTable("SLIDES");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("CREATED_DATE");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("IMAGE");
            entity.Property(e => e.Isdelete).HasColumnName("ISDELETE");
            entity.Property(e => e.Orders).HasColumnName("ORDERS");
            entity.Property(e => e.Status).HasColumnName("STATUS");
            entity.Property(e => e.SubTitle)
                .HasMaxLength(255)
                .HasColumnName("SUB_TITLE");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("TITLE");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("UPDATED_DATE");
            entity.Property(e => e.Urls)
                .HasMaxLength(255)
                .HasColumnName("URLS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
