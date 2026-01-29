
using Microsoft.EntityFrameworkCore;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Data
{
    public class AppDbContext : DbContext
    {

        // monstre asp account 
        // 9583  alfathstore.runasp.net
        // 9581  alsakaphone.runasp.net

        // publish profile
        // publishUrl="site4671.siteasp.net" alfath

        //publishUrl="site41834.siteasp.net" alsakka

        //http://alfathstore.runasp.net/api/Seller/Account/
        //http://alsakaphone.runasp.net/api/Seller/Account/
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BankAccount> BankAccounts { get; set; } = null!;
        public virtual DbSet<Branche> Branches { get; set; } = null!;
        public virtual DbSet<BrancheMoneySafe> BrancheMoneySaves { get; set; } = null!;
        public virtual DbSet<CashDayClose> CashDayCloses { get; set; } = null!;
        public virtual DbSet<CashEditHistory> CashEditHistories { get; set; } = null!;
        public virtual DbSet<CashInFromBankAccount> CashInFromBankAccounts { get; set; } = null!;
        public virtual DbSet<CashInFromBrancheMoneySafe> CashInFromBrancheMoneySaves { get; set; } = null!;
        public virtual DbSet<CashInFromCustomer> CashInFromCustomers { get; set; } = null!;
        public virtual DbSet<CashInFromIncome> CashInFromIncomes { get; set; } = null!;
        public virtual DbSet<CashInFromMasterMoneySafe> CashInFromMasterMoneySaves { get; set; } = null!;
        public virtual DbSet<CashOutToAdvancepaymentOfSalary> CashOutToAdvancepaymentOfSalaries { get; set; } = null!;
        public virtual DbSet<CashOutToBankAccount> CashOutToBankAccounts { get; set; } = null!;
        public virtual DbSet<CashOutToBrancheMoneySafe> CashOutToBrancheMoneySaves { get; set; } = null!;
        public virtual DbSet<CashOutToMasterMoneySafe> CashOutToMasterMoneySaves { get; set; } = null!;
        public virtual DbSet<CashOutToOutGoing> CashOutToOutGoings { get; set; } = null!;
        public virtual DbSet<CashOutToSalary> CashOutToSalaries { get; set; } = null!;
        public virtual DbSet<CashOutToSeller> CashOutToSellers { get; set; } = null!;
        public virtual DbSet<Catogry> Catogries { get; set; } = null!;
        public virtual DbSet<Clime> Climes { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<CustomerAddingSettlement> CustomerAddingSettlements { get; set; } = null!;
        public virtual DbSet<CustomerDiscountSettlement> CustomerDiscountSettlements { get; set; } = null!;
        public virtual DbSet<CustomerPhone> CustomerPhones { get; set; } = null!;
        public virtual DbSet<CustomerType> CustomerTypes { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<EmployeeIncrease> EmployeeIncreases { get; set; } = null!;
        public virtual DbSet<EmployeeLess> EmployeeLesses { get; set; } = null!;
        public virtual DbSet<EmployeePenalty> EmployeePenalties { get; set; } = null!;
        public virtual DbSet<EmployeeReward> EmployeeRewards { get; set; } = null!;
        public virtual DbSet<InCome> InComes { get; set; } = null!;
        public virtual DbSet<MasterMoneySafe> MasterMoneySaves { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderBack> OrderBacks { get; set; } = null!;
        public virtual DbSet<OrderBackDetail> OrderBackDetails { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<OrderDetailsEditHistory> OrderDetailsEditHistories { get; set; } = null!;
        public virtual DbSet<OrderEditHistory> OrderEditHistories { get; set; } = null!;
        public virtual DbSet<OrderToReview> OrderToReviews { get; set; } = null!;
        public virtual DbSet<OutGoing> OutGoings { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductImage> ProductImages { get; set; } = null!;
        public virtual DbSet<ProductTransfer> ProductTransfers { get; set; } = null!;
        public virtual DbSet<ProductTransferDetail> ProductTransferDetails { get; set; } = null!;
        public virtual DbSet<Purchase> Purchases { get; set; } = null!;
        public virtual DbSet<PurchaseBack> PurchaseBacks { get; set; } = null!;
        public virtual DbSet<PurchaseBackDetail> PurchaseBackDetails { get; set; } = null!;
        public virtual DbSet<PurchaseDetail> PurchaseDetails { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RoleClime> RoleClimes { get; set; } = null!;
        public virtual DbSet<Seller> Sellers { get; set; } = null!;
        public virtual DbSet<SellerAddingSettlement> SellerAddingSettlements { get; set; } = null!;
        public virtual DbSet<SellerDiscountSettlement> SellerDiscountSettlements { get; set; } = null!;
        public virtual DbSet<SellerPhone> SellerPhones { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<UserBranches> UserBranches { get; set; } = null!;

      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BrancheMoneySafe>(entity =>
            {
                entity.ToTable("brancheMoneySaves");

                entity.HasIndex(e => e.BrancheId, "IX_brancheMoneySaves_BrancheId");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.BrancheMoneySaves)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CashDayClose>(entity =>
            {
                entity.HasIndex(e => e.BrancheId, "IX_CashDayCloses_BrancheId");

                entity.HasIndex(e => e.ResposibleEmployeeId, "IX_CashDayCloses_ResposibleEmployeeId");

                entity.HasIndex(e => e.UserId, "IX_CashDayCloses_UserId");

                entity.Property(e => e.DayCloseDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.CashDayCloses)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ResposibleEmployee)
                    .WithMany(p => p.CashDayCloses)
                    .HasForeignKey(d => d.ResposibleEmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CashDayCloses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CashEditHistory>(entity =>
            {
                entity.ToTable("cashEditHistories");

                entity.HasIndex(e => e.BrancheId, "IX_cashEditHistories_BrancheId");

                entity.HasIndex(e => e.EditUserId, "IX_cashEditHistories_EditUserId");

                entity.Property(e => e.Discriminator).HasDefaultValueSql("(N'')");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.CashEditHistories)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.EditUser)
                    .WithMany(p => p.CashEditHistories)
                    .HasForeignKey(d => d.EditUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CashInFromBankAccount>(entity =>
            {
                entity.HasIndex(e => e.BanckAccountId, "IX_CashInFromBankAccounts_BanckAccountId");

                entity.HasIndex(e => e.BrancheId, "IX_CashInFromBankAccounts_BrancheId");

                entity.HasIndex(e => e.UserId, "IX_CashInFromBankAccounts_UserId");

                entity.Property(e => e.IsEdit)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.HasOne(d => d.BanckAccount)
                    .WithMany(p => p.CashInFromBankAccounts)
                    .HasForeignKey(d => d.BanckAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.CashInFromBankAccounts)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CashInFromBankAccounts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CashInFromBrancheMoneySafe>(entity =>
            {
                entity.HasIndex(e => e.BrancheId, "IX_CashInFromBrancheMoneySaves_BrancheId");

                entity.HasIndex(e => e.BrancheMoneySafeId, "IX_CashInFromBrancheMoneySaves_BrancheMoneySafeId");

                entity.HasIndex(e => e.UserId, "IX_CashInFromBrancheMoneySaves_UserId");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.IsEdit)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.CashInFromBrancheMoneySaves)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.BrancheMoneySafe)
                    .WithMany(p => p.CashInFromBrancheMoneySaves)
                    .HasForeignKey(d => d.BrancheMoneySafeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CashInFromBrancheMoneySaves)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CashInFromCustomer>(entity =>
            {
                entity.ToTable("cashInFromCustomers");

                entity.HasIndex(e => e.BrancheId, "IX_cashInFromCustomers_BrancheId");

                entity.HasIndex(e => e.CustomerId, "IX_cashInFromCustomers_CustomerId");

                entity.HasIndex(e => e.UserId, "IX_cashInFromCustomers_UserId");

                entity.Property(e => e.IsEdit)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.CashInFromCustomers)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CashInFromCustomers)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CashInFromCustomers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CashInFromIncome>(entity =>
            {
                entity.HasIndex(e => e.BrancheId, "IX_CashInFromIncomes_BrancheId");

                entity.HasIndex(e => e.InComeId, "IX_CashInFromIncomes_InComeId");

                entity.HasIndex(e => e.UserId, "IX_CashInFromIncomes_UserId");

                entity.Property(e => e.IsEdit)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.CashInFromIncomes)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.InCome)
                    .WithMany(p => p.CashInFromIncomes)
                    .HasForeignKey(d => d.InComeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CashInFromIncomes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CashInFromMasterMoneySafe>(entity =>
            {
                entity.HasIndex(e => e.BrancheId, "IX_CashInFromMasterMoneySaves_BrancheId");

                entity.HasIndex(e => e.MasterMoneySafeId, "IX_CashInFromMasterMoneySaves_MasterMoneySafeId");

                entity.HasIndex(e => e.UserId, "IX_CashInFromMasterMoneySaves_UserId");

                entity.Property(e => e.IsEdit)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.CashInFromMasterMoneySaves)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.MasterMoneySafe)
                    .WithMany(p => p.CashInFromMasterMoneySaves)
                    .HasForeignKey(d => d.MasterMoneySafeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CashInFromMasterMoneySaves)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CashOutToAdvancepaymentOfSalary>(entity =>
            {
                entity.HasIndex(e => e.BrancheId, "IX_CashOutToAdvancepaymentOfSalaries_BrancheId");

                entity.HasIndex(e => e.EmployeeId, "IX_CashOutToAdvancepaymentOfSalaries_EmployeeId");

                entity.HasIndex(e => e.UserId, "IX_CashOutToAdvancepaymentOfSalaries_UserId");

                entity.Property(e => e.IsEdit)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.CashOutToAdvancepaymentOfSalaries)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.CashOutToAdvancepaymentOfSalaries)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CashOutToAdvancepaymentOfSalaries)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CashOutToBankAccount>(entity =>
            {
                entity.HasIndex(e => e.BanckAccountId, "IX_CashOutToBankAccounts_BanckAccountId");

                entity.HasIndex(e => e.BrancheId, "IX_CashOutToBankAccounts_BrancheId");

                entity.HasIndex(e => e.UserId, "IX_CashOutToBankAccounts_UserId");

                entity.Property(e => e.IsEdit)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.HasOne(d => d.BanckAccount)
                    .WithMany(p => p.CashOutToBankAccounts)
                    .HasForeignKey(d => d.BanckAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.CashOutToBankAccounts)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CashOutToBankAccounts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CashOutToBrancheMoneySafe>(entity =>
            {
                entity.HasIndex(e => e.BrancheId, "IX_CashOutToBrancheMoneySaves_BrancheId");

                entity.HasIndex(e => e.BrancheMoneySafeId, "IX_CashOutToBrancheMoneySaves_BrancheMoneySafeId");

                entity.HasIndex(e => e.UserId, "IX_CashOutToBrancheMoneySaves_UserId");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.IsEdit)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.CashOutToBrancheMoneySaves)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.BrancheMoneySafe)
                    .WithMany(p => p.CashOutToBrancheMoneySaves)
                    .HasForeignKey(d => d.BrancheMoneySafeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CashOutToBrancheMoneySaves)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CashOutToMasterMoneySafe>(entity =>
            {
                entity.HasIndex(e => e.BrancheId, "IX_CashOutToMasterMoneySaves_BrancheId");

                entity.HasIndex(e => e.MasterMoneySafeId, "IX_CashOutToMasterMoneySaves_MasterMoneySafeId");

                entity.HasIndex(e => e.UserId, "IX_CashOutToMasterMoneySaves_UserId");

                entity.Property(e => e.IsEdit)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.CashOutToMasterMoneySaves)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.MasterMoneySafe)
                    .WithMany(p => p.CashOutToMasterMoneySaves)
                    .HasForeignKey(d => d.MasterMoneySafeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CashOutToMasterMoneySaves)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CashOutToOutGoing>(entity =>
            {
                entity.HasIndex(e => e.BrancheId, "IX_CashOutToOutGoings_BrancheId");

                entity.HasIndex(e => e.OutGoingId, "IX_CashOutToOutGoings_OutGoingId");

                entity.HasIndex(e => e.UserId, "IX_CashOutToOutGoings_UserId");

                entity.Property(e => e.IsEdit)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.CashOutToOutGoings)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.OutGoing)
                    .WithMany(p => p.CashOutToOutGoings)
                    .HasForeignKey(d => d.OutGoingId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CashOutToOutGoings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CashOutToSalary>(entity =>
            {
                entity.HasIndex(e => e.BrancheId, "IX_CashOutToSalaries_BrancheId");

                entity.HasIndex(e => e.EmployeeId, "IX_CashOutToSalaries_EmployeeId");

                entity.HasIndex(e => e.UserId, "IX_CashOutToSalaries_UserId");

                entity.Property(e => e.Date).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.IsEdit)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.CashOutToSalaries)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.CashOutToSalaries)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CashOutToSalaries)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CashOutToSeller>(entity =>
            {
                entity.HasIndex(e => e.BrancheId, "IX_CashOutToSellers_BrancheId");

                entity.HasIndex(e => e.SellerId, "IX_CashOutToSellers_SellerId");

                entity.HasIndex(e => e.UserId, "IX_CashOutToSellers_UserId");

                entity.Property(e => e.IsEdit)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.CashOutToSellers)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.CashOutToSellers)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CashOutToSellers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.BrancheId, "IX_Customers_BrancheId");

                entity.HasIndex(e => e.CustomertypeId, "IX_Customers_CustomertypeId");

                entity.Property(e => e.CustomertypeId).HasDefaultValueSql("((1))");

                entity.Property(e => e.StopDealing)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Customertype)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.CustomertypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CustomerAddingSettlement>(entity =>
            {
                entity.HasIndex(e => e.BrancheId, "IX_CustomerAddingSettlements_BrancheId");

                entity.HasIndex(e => e.CustomerId, "IX_CustomerAddingSettlements_CustomerId");

                entity.HasIndex(e => e.UserId, "IX_CustomerAddingSettlements_UserId");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.CustomerAddingSettlements)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerAddingSettlements)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CustomerAddingSettlements)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CustomerDiscountSettlement>(entity =>
            {
                entity.HasIndex(e => e.BrancheId, "IX_CustomerDiscountSettlements_BrancheId");

                entity.HasIndex(e => e.CustomerId, "IX_CustomerDiscountSettlements_CustomerId");

                entity.HasIndex(e => e.UserId, "IX_CustomerDiscountSettlements_UserId");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.CustomerDiscountSettlements)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerDiscountSettlements)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CustomerDiscountSettlements)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CustomerPhone>(entity =>
            {
                entity.HasIndex(e => e.CustomerId, "IX_CustomerPhones_CustomerId");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerPhones)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.BrancheId, "IX_Employees_BrancheId");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<EmployeeIncrease>(entity =>
            {
                entity.HasIndex(e => e.EmployeeId, "IX_EmployeeIncreases_EmployeeId");

                entity.HasIndex(e => e.UserId, "IX_EmployeeIncreases_UserId");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeIncreases)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EmployeeIncreases)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<EmployeeLess>(entity =>
            {
                entity.HasIndex(e => e.EmployeeId, "IX_EmployeeLesses_EmployeeId");

                entity.HasIndex(e => e.UserId, "IX_EmployeeLesses_UserId");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeLesses)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EmployeeLesses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<EmployeePenalty>(entity =>
            {
                entity.HasIndex(e => e.EmployeeId, "IX_EmployeePenalties_EmployeeId");

                entity.HasIndex(e => e.UserId, "IX_EmployeePenalties_UserId");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeePenalties)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EmployeePenalties)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<EmployeeReward>(entity =>
            {
                entity.HasIndex(e => e.EmployeeId, "IX_EmployeeRewards_EmployeeId");

                entity.HasIndex(e => e.UserId, "IX_EmployeeRewards_UserId");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeRewards)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EmployeeRewards)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<InCome>(entity =>
            {
                entity.HasIndex(e => e.BrancheId, "IX_InComes_BrancheId");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.InComes)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.BrancheId, "IX_Orders_BrancheId");

                entity.HasIndex(e => e.CustomerId, "IX_Orders_CustomerId");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.IsEdit)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.OrderProfit).HasDefaultValueSql("((0.000000000000000e+000))");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<OrderBack>(entity =>
            {
                entity.HasIndex(e => e.BrancheId, "IX_OrderBacks_BrancheId");

                entity.HasIndex(e => e.CustomerId, "IX_OrderBacks_CustomerId");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.IsEdit)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.OrderProfit).HasDefaultValueSql("((0.000000000000000e+000))");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.OrderBacks)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.OrderBacks)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<OrderBackDetail>(entity =>
            {
                entity.HasIndex(e => e.OrderBackId, "IX_OrderBackDetails_OrderBackId");

                entity.HasIndex(e => e.ProductId, "IX_OrderBackDetails_ProductID");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.IsEdit)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.OrderBack)
                    .WithMany(p => p.OrderBackDetails)
                    .HasForeignKey(d => d.OrderBackId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderBackDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasIndex(e => e.OrderId, "IX_OrderDetails_OrderId");

                entity.HasIndex(e => e.ProductId, "IX_OrderDetails_ProductID");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.IsEdit)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<OrderDetailsEditHistory>(entity =>
            {
                entity.ToTable("orderDetailsEditHistories");

                entity.HasIndex(e => e.BrancheId, "IX_orderDetailsEditHistories_BrancheId");

                entity.HasIndex(e => e.EditUserId, "IX_orderDetailsEditHistories_EditUserId");

                entity.Property(e => e.NewProductId).HasColumnName("NewProductID");

                entity.Property(e => e.OldProductId).HasColumnName("OldProductID");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.OrderDetailsEditHistories)
                    .HasForeignKey(d => d.BrancheId);

                entity.HasOne(d => d.EditUser)
                    .WithMany(p => p.OrderDetailsEditHistories)
                    .HasForeignKey(d => d.EditUserId);
            });

            modelBuilder.Entity<OrderEditHistory>(entity =>
            {
                entity.ToTable("orderEditHistories");

                entity.HasIndex(e => e.BrancheId, "IX_orderEditHistories_BrancheId");

                entity.HasIndex(e => e.EditUserId, "IX_orderEditHistories_EditUserId");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.OrderEditHistories)
                    .HasForeignKey(d => d.BrancheId);

                entity.HasOne(d => d.EditUser)
                    .WithMany(p => p.OrderEditHistories)
                    .HasForeignKey(d => d.EditUserId);
            });

            modelBuilder.Entity<OutGoing>(entity =>
            {
                entity.HasIndex(e => e.BrancheId, "IX_OutGoings_BrancheId");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.OutGoings)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.BrancheId, "IX_Products_BrancheId");

                entity.HasIndex(e => e.CatogryId, "IX_Products_CatogryId");

                entity.Property(e => e.Price1).HasColumnName("price1");

                entity.Property(e => e.Price2).HasColumnName("price2");

                entity.Property(e => e.Stock).HasDefaultValueSql("((0.000000000000000e+000))");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.catogry)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CatogryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ProductTransferDetail>(entity =>
            {
                entity.HasIndex(e => e.ProductTransferId, "IX_ProductTransferDetails_ProductTransferId");

                entity.Property(e => e.ProductFromId).HasColumnName("ProductFromID");

                entity.Property(e => e.ProductToId).HasColumnName("ProductToID");

                entity.HasOne(d => d.ProductTransfer)
                    .WithMany(p => p.ProductTransferDetails)
                    .HasForeignKey(d => d.ProductTransferId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasIndex(e => e.BrancheId, "IX_Purchases_BrancheId");

                entity.HasIndex(e => e.SellerId, "IX_Purchases_SellerId");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.IsEdit)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.OrderProfit).HasDefaultValueSql("((0.000000000000000e+000))");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<PurchaseBack>(entity =>
            {
                entity.HasIndex(e => e.BrancheId, "IX_PurchaseBacks_BrancheId");

                entity.HasIndex(e => e.SellerId, "IX_PurchaseBacks_SellerId");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.IsEdit)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.OrderProfit).HasDefaultValueSql("((0.000000000000000e+000))");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.PurchaseBacks)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.PurchaseBacks)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<PurchaseBackDetail>(entity =>
            {
                entity.HasIndex(e => e.ProductId, "IX_PurchaseBackDetails_ProductID");

                entity.HasIndex(e => e.PurchaseBackId, "IX_PurchaseBackDetails_PurchaseBackId");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.IsEdit)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PurchaseBackDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.PurchaseBack)
                    .WithMany(p => p.PurchaseBackDetails)
                    .HasForeignKey(d => d.PurchaseBackId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<PurchaseDetail>(entity =>
            {
                entity.HasIndex(e => e.ProductId, "IX_PurchaseDetails_ProductID");

                entity.HasIndex(e => e.PurchaseId, "IX_PurchaseDetails_PurchaseId");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.IsEdit)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PurchaseDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Purchase)
                    .WithMany(p => p.PurchaseDetails)
                    .HasForeignKey(d => d.PurchaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<RoleClime>(entity =>
            {
                entity.HasIndex(e => e.ClimeId, "IX_RoleClimes_ClimeId");

                entity.HasIndex(e => e.RoleId, "IX_RoleClimes_RoleId");

                entity.HasOne(d => d.Clime)
                    .WithMany(p => p.RoleClimes)
                    .HasForeignKey(d => d.ClimeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleClimes)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Seller>(entity =>
            {
                entity.HasIndex(e => e.BrancheId, "IX_Sellers_BrancheId");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.Sellers)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<SellerAddingSettlement>(entity =>
            {
                entity.HasIndex(e => e.BrancheId, "IX_SellerAddingSettlements_BrancheId");

                entity.HasIndex(e => e.SellerId, "IX_SellerAddingSettlements_SellerId");

                entity.HasIndex(e => e.UserId, "IX_SellerAddingSettlements_UserId");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.SellerAddingSettlements)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.SellerAddingSettlements)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SellerAddingSettlements)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<SellerDiscountSettlement>(entity =>
            {
                entity.HasIndex(e => e.BrancheId, "IX_SellerDiscountSettlements_BrancheId");

                entity.HasIndex(e => e.SellerId, "IX_SellerDiscountSettlements_SellerId");

                entity.HasIndex(e => e.UserId, "IX_SellerDiscountSettlements_UserId");

                entity.HasOne(d => d.Branche)
                    .WithMany(p => p.SellerDiscountSettlements)
                    .HasForeignKey(d => d.BrancheId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.SellerDiscountSettlements)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SellerDiscountSettlements)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<SellerPhone>(entity =>
            {
                entity.HasIndex(e => e.SellerId, "IX_SellerPhones_SellerId");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.SellerPhones)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.IsEdit)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_UserRoles_RoleId");

                entity.HasIndex(e => e.UserId, "IX_UserRoles_UserId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            //OnModelCreatingPartial(modelBuilder);
        }

        //public void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
