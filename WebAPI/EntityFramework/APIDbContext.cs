namespace WebAPI.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class APIDbContext : DbContext
    {
        public APIDbContext()
            : base("name=APIDbContext")
        {
            //fix loi The 'ObjectContent`1' type failed to serialize the response body for content type 'application/xml; charset=utf-8'
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<AGENT> AGENT { get; set; }
        public virtual DbSet<CARD> CARD { get; set; }
        public virtual DbSet<CARD_TYPE> CARD_TYPE { get; set; }
        public virtual DbSet<CITY> CITY { get; set; }
        public virtual DbSet<COUNTRY> COUNTRY { get; set; }
        public virtual DbSet<MERCHANT> MERCHANT { get; set; }
        public virtual DbSet<MERCHANT_SUMMARY_DAILY> MERCHANT_SUMMARY_DAILY { get; set; }
        public virtual DbSet<MERCHANT_SUMMARY_MONTHLY> MERCHANT_SUMMARY_MONTHLY { get; set; }
        public virtual DbSet<MERCHANT_SUMMARY_QUARTERLY> MERCHANT_SUMMARY_QUARTERLY { get; set; }
        public virtual DbSet<MERCHANT_SUMMARY_YEARLY> MERCHANT_SUMMARY_YEARLY { get; set; }
        public virtual DbSet<MERCHANT_TYPE> MERCHANT_TYPE { get; set; }
        public virtual DbSet<MESSAGE> MESSAGE { get; set; }
        public virtual DbSet<PROCESSOR> PROCESSOR { get; set; }
        public virtual DbSet<REGION> REGION { get; set; }
        public virtual DbSet<RETRIVAL> RETRIVAL { get; set; }
        public virtual DbSet<TRANSACTION_DETAIL> TRANSACTION_DETAIL { get; set; }
        public virtual DbSet<TRANSACTION_TYPE> TRANSACTION_TYPE { get; set; }
        public virtual DbSet<USER_INFORMATION> USER_INFORMATION { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AGENT>()
                .Property(e => e.AgentCode)
                .IsUnicode(false);

            modelBuilder.Entity<AGENT>()
                .Property(e => e.AgentStatus)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AGENT>()
                .Property(e => e.CityCode)
                .IsUnicode(false);

            modelBuilder.Entity<AGENT>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<AGENT>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<AGENT>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<AGENT>()
                .HasMany(e => e.MERCHANT)
                .WithRequired(e => e.AGENT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CARD>()
                .Property(e => e.AccountNumber)
                .IsUnicode(false);

            modelBuilder.Entity<CARD>()
                .Property(e => e.FirstTwelveAccountNumber)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CARD>()
                .Property(e => e.CardTypeCode)
                .IsUnicode(false);

            modelBuilder.Entity<CARD>()
                .HasMany(e => e.RETRIVAL)
                .WithRequired(e => e.CARD)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CARD>()
                .HasMany(e => e.TRANSACTION_DETAIL)
                .WithRequired(e => e.CARD)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CARD_TYPE>()
                .Property(e => e.CardTypeCode)
                .IsUnicode(false);

            modelBuilder.Entity<CARD_TYPE>()
                .HasMany(e => e.CARD)
                .WithRequired(e => e.CARD_TYPE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CITY>()
                .Property(e => e.CityCode)
                .IsUnicode(false);

            modelBuilder.Entity<CITY>()
                .Property(e => e.RegionCode)
                .IsUnicode(false);

            modelBuilder.Entity<COUNTRY>()
                .Property(e => e.CountryCode)
                .IsUnicode(false);

            modelBuilder.Entity<MERCHANT>()
                .Property(e => e.MerchantCode)
                .IsUnicode(false);

            modelBuilder.Entity<MERCHANT>()
                .Property(e => e.Status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MERCHANT>()
                .Property(e => e.MerchantType)
                .IsUnicode(false);

            modelBuilder.Entity<MERCHANT>()
                .Property(e => e.CityCode)
                .IsUnicode(false);

            modelBuilder.Entity<MERCHANT>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<MERCHANT>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<MERCHANT>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<MERCHANT>()
                .Property(e => e.BankCardDBA)
                .IsUnicode(false);

            modelBuilder.Entity<MERCHANT>()
                .Property(e => e.AgentCode)
                .IsUnicode(false);

            modelBuilder.Entity<MERCHANT>()
                .HasMany(e => e.RETRIVAL)
                .WithRequired(e => e.MERCHANT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MERCHANT>()
                .HasMany(e => e.TRANSACTION_DETAIL)
                .WithRequired(e => e.MERCHANT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.MerchantCode)
                .IsUnicode(false);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.SaleAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.ReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.NetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.KeyedAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.KeyedReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.KeyedNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.ForeignCardAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.ForeignCardReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.ForeignCardNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.DebitCardAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.DebitCardReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.DebitCardNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.VisaCardAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.VisaCardReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.VisaCardNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.DiscoverCardAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.DiscoverCardReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.DiscoverCardNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.MasterCardAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.MasterCardReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.MasterCardNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.AmericanExpressAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.AmericanExpressReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.AmericanExpressNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.OtherCardAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.OtherCardReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.OtherCardNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.RegionCode)
                .IsUnicode(false);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.MerchantType)
                .IsUnicode(false);

            modelBuilder.Entity<MERCHANT_SUMMARY_DAILY>()
                .Property(e => e.AgentCode)
                .IsUnicode(false);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.MerchantCode)
                .IsUnicode(false);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.SaleAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.ReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.NetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.KeyedAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.KeyedReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.KeyedNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.ForeignCardAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.ForeignCardReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.ForeignCardNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.DebitCardAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.DebitCardReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.DebitCardNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.VisaCardAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.VisaCardReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.VisaCardNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.DiscoverCardAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.DiscoverCardReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.DiscoverCardNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.MasterCardAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.MasterCardReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.MasterCardNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.AmericanExpressAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.AmericanExpressReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.AmericanExpressNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.OtherCardAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.OtherCardReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.OtherCardNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.RegionCode)
                .IsUnicode(false);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.MerchantType)
                .IsUnicode(false);

            modelBuilder.Entity<MERCHANT_SUMMARY_MONTHLY>()
                .Property(e => e.AgentCode)
                .IsUnicode(false);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.MerchantCode)
                .IsUnicode(false);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.SaleAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.ReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.NetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.KeyedAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.KeyedReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.KeyedNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.ForeignCardAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.ForeignCardReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.ForeignCardNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.DebitCardAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.DebitCardReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.DebitCardNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.VisaCardAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.VisaCardReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.VisaCardNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.DiscoverCardAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.DiscoverCardReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.DiscoverCardNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.MasterCardAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.MasterCardReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.MasterCardNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.AmericanExpressAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.AmericanExpressReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.AmericanExpressNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.OtherCardAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.OtherCardReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.OtherCardNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.RegionCode)
                .IsUnicode(false);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.MerchantType)
                .IsUnicode(false);

            modelBuilder.Entity<MERCHANT_SUMMARY_QUARTERLY>()
                .Property(e => e.AgentCode)
                .IsUnicode(false);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.MerchantCode)
                .IsUnicode(false);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.SaleAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.ReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.NetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.KeyedAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.KeyedReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.KeyedNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.ForeignCardAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.ForeignCardReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.ForeignCardNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.DebitCardAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.DebitCardReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.DebitCardNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.VisaCardAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.VisaCardReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.VisaCardNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.DiscoverCardAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.DiscoverCardReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.DiscoverCardNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.MasterCardAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.MasterCardReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.MasterCardNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.AmericanExpressAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.AmericanExpressReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.AmericanExpressNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.OtherCardAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.OtherCardReturnAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.OtherCardNetAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.RegionCode)
                .IsUnicode(false);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.MerchantType)
                .IsUnicode(false);

            modelBuilder.Entity<MERCHANT_SUMMARY_YEARLY>()
                .Property(e => e.AgentCode)
                .IsUnicode(false);

            modelBuilder.Entity<MERCHANT_TYPE>()
                .Property(e => e.MerchantType)
                .IsUnicode(false);

            modelBuilder.Entity<MESSAGE>()
                .Property(e => e.Sender)
                .IsUnicode(false);

            modelBuilder.Entity<MESSAGE>()
                .Property(e => e.SenderType)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MESSAGE>()
                .Property(e => e.Receiver)
                .IsUnicode(false);

            modelBuilder.Entity<MESSAGE>()
                .Property(e => e.ReceiverType)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MESSAGE>()
                .Property(e => e.Message1)
                .IsUnicode(false);

            modelBuilder.Entity<MESSAGE>()
                .Property(e => e.MessageType)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PROCESSOR>()
                .Property(e => e.ProcessorCode)
                .IsUnicode(false);

            modelBuilder.Entity<PROCESSOR>()
                .Property(e => e.Status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PROCESSOR>()
                .HasMany(e => e.MERCHANT)
                .WithOptional(e => e.PROCESSOR)
                .HasForeignKey(e => e.BackEndProcessor);

            modelBuilder.Entity<REGION>()
                .Property(e => e.RegionCode)
                .IsUnicode(false);

            modelBuilder.Entity<REGION>()
                .Property(e => e.CountryCode)
                .IsUnicode(false);

            modelBuilder.Entity<RETRIVAL>()
                .Property(e => e.RetrivalCode)
                .IsUnicode(false);

            modelBuilder.Entity<RETRIVAL>()
                .Property(e => e.AccountNumber)
                .IsUnicode(false);

            modelBuilder.Entity<RETRIVAL>()
                .Property(e => e.MerchantCode)
                .IsUnicode(false);

            modelBuilder.Entity<RETRIVAL>()
                .Property(e => e.TransactionCode)
                .IsUnicode(false);

            modelBuilder.Entity<RETRIVAL>()
                .Property(e => e.Amout)
                .HasPrecision(19, 4);

            modelBuilder.Entity<TRANSACTION_DETAIL>()
                .Property(e => e.TransactionCode)
                .IsUnicode(false);

            modelBuilder.Entity<TRANSACTION_DETAIL>()
                .Property(e => e.MerchantCode)
                .IsUnicode(false);

            modelBuilder.Entity<TRANSACTION_DETAIL>()
                .Property(e => e.TerminalNumber)
                .IsUnicode(false);

            modelBuilder.Entity<TRANSACTION_DETAIL>()
                .Property(e => e.CardtypeCode)
                .IsUnicode(false);

            modelBuilder.Entity<TRANSACTION_DETAIL>()
                .Property(e => e.TransactionAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<TRANSACTION_DETAIL>()
                .Property(e => e.AuthorizationNumber)
                .IsUnicode(false);

            modelBuilder.Entity<TRANSACTION_DETAIL>()
                .Property(e => e.AccountNumber)
                .IsUnicode(false);

            modelBuilder.Entity<TRANSACTION_DETAIL>()
                .Property(e => e.FirstTwelveAccountNumber)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TRANSACTION_DETAIL>()
                .Property(e => e.TransactionTypeCode)
                .IsUnicode(false);

            modelBuilder.Entity<TRANSACTION_DETAIL>()
                .Property(e => e.RegionCode)
                .IsUnicode(false);

            modelBuilder.Entity<TRANSACTION_DETAIL>()
                .Property(e => e.MerchantType)
                .IsUnicode(false);

            modelBuilder.Entity<TRANSACTION_DETAIL>()
                .Property(e => e.AgentCode)
                .IsUnicode(false);

            modelBuilder.Entity<TRANSACTION_TYPE>()
                .Property(e => e.TransactionTypeCode)
                .IsUnicode(false);

            modelBuilder.Entity<USER_INFORMATION>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            //modelBuilder.Entity<USER_INFORMATION>()
            //    .Property(e => e.Password)
            //    .IsUnicode(false);

            modelBuilder.Entity<USER_INFORMATION>()
                .Property(e => e.UserType)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<USER_INFORMATION>()
                .Property(e => e.Status)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
