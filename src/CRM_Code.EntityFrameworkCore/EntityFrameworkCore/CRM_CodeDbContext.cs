using CRM_Code.Readers;
using CRM_Code.Books;
using CRM_Code.Authors;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TextTemplateManagement.EntityFrameworkCore;
using Volo.Saas.EntityFrameworkCore;
using Volo.Saas.Editions;
using Volo.Saas.Tenants;
using Volo.Abp.Gdpr;

namespace CRM_Code.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityProDbContext))]
[ReplaceDbContext(typeof(ISaasDbContext))]
[ConnectionStringName("Default")]
public class CRM_CodeDbContext :
    AbpDbContext<CRM_CodeDbContext>,
    IIdentityProDbContext,
    ISaasDbContext
{
    public DbSet<Reader> Readers { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext and ISaasDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext and ISaasDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // SaaS
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Edition> Editions { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public CRM_CodeDbContext(DbContextOptions<CRM_CodeDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentityPro();
        builder.ConfigureIdentityServer();
        builder.ConfigureFeatureManagement();
        builder.ConfigureLanguageManagement();
        builder.ConfigureSaas();
        builder.ConfigureTextTemplateManagement();
        builder.ConfigureBlobStoring();
        builder.ConfigureGdpr();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(CRM_CodeConsts.DbTablePrefix + "YourEntities", CRM_CodeConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
        if (builder.IsHostDatabase())
        {
            builder.Entity<Author>(b =>
{
    b.ToTable(CRM_CodeConsts.DbTablePrefix + "Authors", CRM_CodeConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Name).HasColumnName(nameof(Author.Name)).IsRequired().HasMaxLength(AuthorConsts.NameMaxLength);
    b.Property(x => x.Birthdate).HasColumnName(nameof(Author.Birthdate));
    b.Property(x => x.Active).HasColumnName(nameof(Author.Active));
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Book>(b =>
{
    b.ToTable(CRM_CodeConsts.DbTablePrefix + "Books", CRM_CodeConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Title).HasColumnName(nameof(Book.Title)).IsRequired().HasMaxLength(BookConsts.TitleMaxLength);
    b.Property(x => x.PageCount).HasColumnName(nameof(Book.PageCount)).IsRequired().HasMaxLength(BookConsts.PageCountMaxLength);
    b.Property(x => x.Pirce).HasColumnName(nameof(Book.Pirce));
    b.HasOne<Author>().WithMany().HasForeignKey(x => x.AuthorId).OnDelete(DeleteBehavior.NoAction);
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Reader>(b =>
{
    b.ToTable(CRM_CodeConsts.DbTablePrefix + "Readers", CRM_CodeConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.NameSurname).HasColumnName(nameof(Reader.NameSurname)).IsRequired().HasMaxLength(ReaderConsts.NameSurnameMaxLength);
    b.Property(x => x.EmailAddress).HasColumnName(nameof(Reader.EmailAddress));
    b.Property(x => x.Gender).HasColumnName(nameof(Reader.Gender));
    b.HasMany(x => x.Books).WithOne().HasForeignKey(x => x.ReaderId).IsRequired().OnDelete(DeleteBehavior.NoAction);
});

            builder.Entity<ReaderBook>(b =>
{
b.ToTable(CRM_CodeConsts.DbTablePrefix + "ReaderBook" + CRM_CodeConsts.DbSchema);
b.ConfigureByConvention();

b.HasKey(
    x => new { x.ReaderId, x.BookId }
);

b.HasOne<Reader>().WithMany(x => x.Books).HasForeignKey(x => x.ReaderId).IsRequired().OnDelete(DeleteBehavior.NoAction);
b.HasOne<Book>().WithMany().HasForeignKey(x => x.BookId).IsRequired().OnDelete(DeleteBehavior.NoAction);

b.HasIndex(
        x => new { x.ReaderId, x.BookId }
);
});
        }
    }
}