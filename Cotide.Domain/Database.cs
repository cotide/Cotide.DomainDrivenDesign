

// This file was automatically generated.
// Do not make changes directly to this file - edit the template instead.
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: "MyDbContext"
//     Connection String:      "Data Source=.;Initial Catalog=Peacock.Database;Integrated Security=True"

// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

//using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.DatabaseGeneratedOption;

namespace Cotide.Domain
{
    // ************************************************************************
    // Database context
    public class MyDbContext : DbContext
    {
        public IDbSet<AbstratDataAppraiseDataBase> AbstratDataAppraiseDataBase { get; set; } // AbstratDataAppraiseDataBase
        public IDbSet<DbSerial> DbSerial { get; set; } // DBSerial
        public IDbSet<DbSerialItem> DbSerialItem { get; set; } // DBSerialItem
        public IDbSet<DbSerialValue> DbSerialValue { get; set; } // DBSerialValue
        public IDbSet<District> District { get; set; } // District
        public IDbSet<DistrictPast> DistrictPast { get; set; } // District_Past
        public IDbSet<Picture> Picture { get; set; } // Picture
        public IDbSet<SystemParameter> SystemParameter { get; set; } // System_Parameter

        static MyDbContext()
        {
            Database.SetInitializer<MyDbContext>(null);
        }

        public MyDbContext()
            : base("Name=MyDbContext")
        {
        }

        public MyDbContext(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AbstratDataAppraiseDataBaseConfiguration());
            modelBuilder.Configurations.Add(new DbSerialConfiguration());
            modelBuilder.Configurations.Add(new DbSerialItemConfiguration());
            modelBuilder.Configurations.Add(new DbSerialValueConfiguration());
            modelBuilder.Configurations.Add(new DistrictConfiguration());
            modelBuilder.Configurations.Add(new DistrictPastConfiguration());
            modelBuilder.Configurations.Add(new PictureConfiguration());
            modelBuilder.Configurations.Add(new SystemParameterConfiguration());
        }
    }

    // ************************************************************************
    // POCO classes

    // AbstratDataAppraiseDataBase
    public class AbstratDataAppraiseDataBase
    {
        public long Tid { get; set; } // TID (Primary key)
        public Guid? TGuid { get; set; } // TGuid
        public string Pid { get; set; } // PID
        public string Expansion { get; set; } // Expansion
        public DateTime? CreatedDate { get; set; } // CreatedDate
        public int? Status { get; set; } // Status
        public DateTime? ModifyDate { get; set; } // ModifyDate
    }

    // DBSerial
    public class DbSerial
    {
        public long Id { get; set; } // ID (Primary key)
        public string Name { get; set; } // Name
        public string Format { get; set; } // Format

        // Reverse navigation
        public virtual ICollection<DbSerialItem> DbSerialItem { get; set; } // DBSerialItem.FKAC0D2F61D15401CB;

        public DbSerial()
        {
            DbSerialItem = new List<DbSerialItem>();
        }
    }

    // DBSerialItem
    public class DbSerialItem
    {
        public long Id { get; set; } // ID (Primary key)
        public string Name { get; set; } // Name
        public string GhType { get; set; } // GHType
        public long? MaxValue { get; set; } // MaxValue
        public long? MinValue { get; set; } // MinValue
        public int? GhLength { get; set; } // GHLength
        public bool? ZeroPad { get; set; } // ZeroPad
        public string DefValue { get; set; } // DefValue
        public long? DefStart { get; set; } // DefStart
        public long? DbSerialId { get; set; } // DBSerialID

        // Reverse navigation
        public virtual ICollection<DbSerialValue> DbSerialValue { get; set; } // DBSerialValue.FKF75AC690178DF63D;

        // Foreign keys
        public virtual DbSerial DbSerial { get; set; } //  DbSerialId - FKAC0D2F61D15401CB

        public DbSerialItem()
        {
            DbSerialValue = new List<DbSerialValue>();
        }
    }

    // DBSerialValue
    public class DbSerialValue
    {
        public long Id { get; set; } // ID (Primary key)
        public string GhKey { get; set; } // GHKey
        public string GhValue { get; set; } // GHValue
        public long? GhStart { get; set; } // GHStart
        public long? DbSerialItemId { get; set; } // DBSerialItemID

        // Foreign keys
        public virtual DbSerialItem DbSerialItem { get; set; } //  DbSerialItemId - FKF75AC690178DF63D
    }

    // District
    public class District
    {
        public long Tid { get; set; } // TID (Primary key)
        public Guid? TGuid { get; set; } // TGuid
        public string Pid { get; set; } // PID
        public string Expansion { get; set; } // Expansion
        public DateTime? CreatedDate { get; set; } // CreatedDate
        public int? Status { get; set; } // Status
        public DateTime? ModifyDate { get; set; } // ModifyDate
        public decimal? Area { get; set; } // Area
        public long? Population { get; set; } // Population
        public long? FloatingPopulation { get; set; } // FloatingPopulation
        public string Summary { get; set; } // Summary
        public string EastTo { get; set; } // EastTo
        public string SouthTo { get; set; } // SouthTo
        public string WestTo { get; set; } // WestTo
        public string NorthTo { get; set; } // NorthTo
        public decimal? XCoordinate { get; set; } // XCoordinate
        public decimal? YCoordinate { get; set; } // YCoordinate
        public string XyCoordinateArray { get; set; } // XYCoordinateArray
        public decimal? BaiduMapLongitude { get; set; } // BaiduMapLongitude
        public decimal? BaiduMapLatitude { get; set; } // BaiduMapLatitude
        public string BaiduLatLngArray { get; set; } // BaiduLatLngArray
        public decimal? GoogleMapLongitude { get; set; } // GoogleMapLongitude
        public decimal? GoogleMapLatitude { get; set; } // GoogleMapLatitude
        public string GoogleLatLngArray { get; set; } // GoogleLatLngArray
        public string DistrictName { get; set; } // DistrictName
        public string DistrictParentFullName { get; set; } // DistrictParentFullName
        public string DistrictFullName { get; set; } // DistrictFullName
        public string DistrictType { get; set; } // DistrictType
        public DateTime DescriptionDate { get; set; } // DescriptionDate
        public string Alias { get; set; } // Alias
        public string Code { get; set; } // Code
        public string PostCode { get; set; } // PostCode
        public string Pictures { get; set; } // Pictures
        public string Comment { get; set; } // Comment
    }

    // District_Past
    public class DistrictPast
    {
        public long Tid { get; set; } // TID (Primary key)
        public Guid? TGuid { get; set; } // TGuid
        public string Pid { get; set; } // PID
        public string Expansion { get; set; } // Expansion
        public DateTime? CreatedDate { get; set; } // CreatedDate
        public int? Status { get; set; } // Status
        public DateTime? ModifyDate { get; set; } // ModifyDate
        public decimal? Area { get; set; } // Area
        public long? Population { get; set; } // Population
        public long? FloatingPopulation { get; set; } // FloatingPopulation
        public string Summary { get; set; } // Summary
        public string EastTo { get; set; } // EastTo
        public string SouthTo { get; set; } // SouthTo
        public string WestTo { get; set; } // WestTo
        public string NorthTo { get; set; } // NorthTo
        public decimal? XCoordinate { get; set; } // XCoordinate
        public decimal? YCoordinate { get; set; } // YCoordinate
        public string XyCoordinateArray { get; set; } // XYCoordinateArray
        public decimal? BaiduMapLongitude { get; set; } // BaiduMapLongitude
        public decimal? BaiduMapLatitude { get; set; } // BaiduMapLatitude
        public string BaiduLatLngArray { get; set; } // BaiduLatLngArray
        public decimal? GoogleMapLongitude { get; set; } // GoogleMapLongitude
        public decimal? GoogleMapLatitude { get; set; } // GoogleMapLatitude
        public string GoogleLatLngArray { get; set; } // GoogleLatLngArray
        public string DistrictName { get; set; } // DistrictName
        public string DistrictParentFullName { get; set; } // DistrictParentFullName
        public string DistrictFullName { get; set; } // DistrictFullName
        public string DistrictType { get; set; } // DistrictType
        public DateTime? DescriptionDate { get; set; } // DescriptionDate
        public string Alias { get; set; } // Alias
        public string Code { get; set; } // Code
        public string PostCode { get; set; } // PostCode
        public string Pictures { get; set; } // Pictures
        public string Comment { get; set; } // Comment
        public DateTime? DespEndDate { get; set; } // DespEndDate
        public DateTime? DisappearDate { get; set; } // DisappearDate
    }

    // Picture
    public class Picture
    {
        public long Tid { get; set; } // TID (Primary key)
        public Guid? TGuid { get; set; } // TGuid
        public string Pid { get; set; } // PID
        public string Expansion { get; set; } // Expansion
        public DateTime? CreatedDate { get; set; } // CreatedDate
        public int? Status { get; set; } // Status
        public DateTime? ModifyDate { get; set; } // ModifyDate
        public string Title { get; set; } // Title
        public string Format { get; set; } // Format
        public string Width { get; set; } // Width
        public string Height { get; set; } // Height
        public string Path { get; set; } // Path
        public string FileMd5 { get; set; } // FileMD5
    }

    // System_Parameter
    public class SystemParameter
    {
        public long Tid { get; set; } // TID (Primary key)
        public Guid? TGuid { get; set; } // TGuid
        public string Pid { get; set; } // PID
        public string Expansion { get; set; } // Expansion
        public DateTime? CreatedDate { get; set; } // CreatedDate
        public int? Status { get; set; } // Status
        public string Name { get; set; } // Name
        public string Title { get; set; } // Title
        public string Value { get; set; } // Value
        public long? ParentId { get; set; } // ParentID

        // Reverse navigation
        public virtual ICollection<SystemParameter> SystemParameter2 { get; set; } // System_Parameter.FK65C5E93D45CE5E4F;

        // Foreign keys
        public virtual SystemParameter SystemParameter1 { get; set; } //  ParentId - FK65C5E93D45CE5E4F

        public SystemParameter()
        {
            SystemParameter2 = new List<SystemParameter>();
        }
    }


    // ************************************************************************
    // POCO Configuration

    // AbstratDataAppraiseDataBase
    internal class AbstratDataAppraiseDataBaseConfiguration : EntityTypeConfiguration<AbstratDataAppraiseDataBase>
    {
        public AbstratDataAppraiseDataBaseConfiguration()
        {
            ToTable("dbo.AbstratDataAppraiseDataBase");
            HasKey(x => x.Tid);

            Property(x => x.Tid).HasColumnName("TID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.TGuid).HasColumnName("TGuid").IsOptional();
            Property(x => x.Pid).HasColumnName("PID").IsOptional().HasMaxLength(255);
            Property(x => x.Expansion).HasColumnName("Expansion").IsOptional().HasMaxLength(2000);
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.Status).HasColumnName("Status").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("ModifyDate").IsOptional();
        }
    }

    // DBSerial
    internal class DbSerialConfiguration : EntityTypeConfiguration<DbSerial>
    {
        public DbSerialConfiguration()
        {
            ToTable("dbo.DBSerial");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("ID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("Name").IsOptional().HasMaxLength(255);
            Property(x => x.Format).HasColumnName("Format").IsOptional().HasMaxLength(255);
        }
    }

    // DBSerialItem
    internal class DbSerialItemConfiguration : EntityTypeConfiguration<DbSerialItem>
    {
        public DbSerialItemConfiguration()
        {
            ToTable("dbo.DBSerialItem");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("ID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("Name").IsOptional().HasMaxLength(255);
            Property(x => x.GhType).HasColumnName("GHType").IsOptional().HasMaxLength(255);
            Property(x => x.MaxValue).HasColumnName("MaxValue").IsOptional();
            Property(x => x.MinValue).HasColumnName("MinValue").IsOptional();
            Property(x => x.GhLength).HasColumnName("GHLength").IsOptional();
            Property(x => x.ZeroPad).HasColumnName("ZeroPad").IsOptional();
            Property(x => x.DefValue).HasColumnName("DefValue").IsOptional().HasMaxLength(255);
            Property(x => x.DefStart).HasColumnName("DefStart").IsOptional();
            Property(x => x.DbSerialId).HasColumnName("DBSerialID").IsOptional();

            // Foreign keys
            HasOptional(a => a.DbSerial).WithMany(b => b.DbSerialItem).HasForeignKey(c => c.DbSerialId); // FKAC0D2F61D15401CB
        }
    }

    // DBSerialValue
    internal class DbSerialValueConfiguration : EntityTypeConfiguration<DbSerialValue>
    {
        public DbSerialValueConfiguration()
        {
            ToTable("dbo.DBSerialValue");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("ID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.GhKey).HasColumnName("GHKey").IsOptional().HasMaxLength(255);
            Property(x => x.GhValue).HasColumnName("GHValue").IsOptional().HasMaxLength(255);
            Property(x => x.GhStart).HasColumnName("GHStart").IsOptional();
            Property(x => x.DbSerialItemId).HasColumnName("DBSerialItemID").IsOptional();

            // Foreign keys
            HasOptional(a => a.DbSerialItem).WithMany(b => b.DbSerialValue).HasForeignKey(c => c.DbSerialItemId); // FKF75AC690178DF63D
        }
    }

    // District
    internal class DistrictConfiguration : EntityTypeConfiguration<District>
    {
        public DistrictConfiguration()
        {
            ToTable("dbo.District");
            HasKey(x => x.Tid);

            Property(x => x.Tid).HasColumnName("TID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.TGuid).HasColumnName("TGuid").IsOptional();
            Property(x => x.Pid).HasColumnName("PID").IsOptional().HasMaxLength(255);
            Property(x => x.Expansion).HasColumnName("Expansion").IsOptional().HasMaxLength(2000);
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.Status).HasColumnName("Status").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("ModifyDate").IsOptional();
            Property(x => x.Area).HasColumnName("Area").IsOptional().HasPrecision(19,5);
            Property(x => x.Population).HasColumnName("Population").IsOptional();
            Property(x => x.FloatingPopulation).HasColumnName("FloatingPopulation").IsOptional();
            Property(x => x.Summary).HasColumnName("Summary").IsOptional().HasMaxLength(1073741823);
            Property(x => x.EastTo).HasColumnName("EastTo").IsOptional().HasMaxLength(255);
            Property(x => x.SouthTo).HasColumnName("SouthTo").IsOptional().HasMaxLength(255);
            Property(x => x.WestTo).HasColumnName("WestTo").IsOptional().HasMaxLength(255);
            Property(x => x.NorthTo).HasColumnName("NorthTo").IsOptional().HasMaxLength(255);
            Property(x => x.XCoordinate).HasColumnName("XCoordinate").IsOptional().HasPrecision(19,5);
            Property(x => x.YCoordinate).HasColumnName("YCoordinate").IsOptional().HasPrecision(19,5);
            Property(x => x.XyCoordinateArray).HasColumnName("XYCoordinateArray").IsOptional().HasMaxLength(1073741823);
            Property(x => x.BaiduMapLongitude).HasColumnName("BaiduMapLongitude").IsOptional().HasPrecision(19,5);
            Property(x => x.BaiduMapLatitude).HasColumnName("BaiduMapLatitude").IsOptional().HasPrecision(19,5);
            Property(x => x.BaiduLatLngArray).HasColumnName("BaiduLatLngArray").IsOptional().HasMaxLength(1073741823);
            Property(x => x.GoogleMapLongitude).HasColumnName("GoogleMapLongitude").IsOptional().HasPrecision(19,5);
            Property(x => x.GoogleMapLatitude).HasColumnName("GoogleMapLatitude").IsOptional().HasPrecision(19,5);
            Property(x => x.GoogleLatLngArray).HasColumnName("GoogleLatLngArray").IsOptional().HasMaxLength(1073741823);
            Property(x => x.DistrictName).HasColumnName("DistrictName").IsOptional().HasMaxLength(255);
            Property(x => x.DistrictParentFullName).HasColumnName("DistrictParentFullName").IsOptional().HasMaxLength(255);
            Property(x => x.DistrictFullName).HasColumnName("DistrictFullName").IsOptional().HasMaxLength(255);
            Property(x => x.DistrictType).HasColumnName("DistrictType").IsOptional().HasMaxLength(255);
            Property(x => x.DescriptionDate).HasColumnName("DescriptionDate").IsRequired();
            Property(x => x.Alias).HasColumnName("Alias").IsOptional().HasMaxLength(255);
            Property(x => x.Code).HasColumnName("Code").IsOptional().HasMaxLength(255);
            Property(x => x.PostCode).HasColumnName("PostCode").IsOptional().HasMaxLength(255);
            Property(x => x.Pictures).HasColumnName("Pictures").IsOptional().HasMaxLength(4000);
            Property(x => x.Comment).HasColumnName("Comment").IsOptional().HasMaxLength(255);
        }
    }

    // District_Past
    internal class DistrictPastConfiguration : EntityTypeConfiguration<DistrictPast>
    {
        public DistrictPastConfiguration()
        {
            ToTable("dbo.District_Past");
            HasKey(x => x.Tid);

            Property(x => x.Tid).HasColumnName("TID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.TGuid).HasColumnName("TGuid").IsOptional();
            Property(x => x.Pid).HasColumnName("PID").IsOptional().HasMaxLength(255);
            Property(x => x.Expansion).HasColumnName("Expansion").IsOptional().HasMaxLength(2000);
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.Status).HasColumnName("Status").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("ModifyDate").IsOptional();
            Property(x => x.Area).HasColumnName("Area").IsOptional().HasPrecision(19,5);
            Property(x => x.Population).HasColumnName("Population").IsOptional();
            Property(x => x.FloatingPopulation).HasColumnName("FloatingPopulation").IsOptional();
            Property(x => x.Summary).HasColumnName("Summary").IsOptional().HasMaxLength(1073741823);
            Property(x => x.EastTo).HasColumnName("EastTo").IsOptional().HasMaxLength(255);
            Property(x => x.SouthTo).HasColumnName("SouthTo").IsOptional().HasMaxLength(255);
            Property(x => x.WestTo).HasColumnName("WestTo").IsOptional().HasMaxLength(255);
            Property(x => x.NorthTo).HasColumnName("NorthTo").IsOptional().HasMaxLength(255);
            Property(x => x.XCoordinate).HasColumnName("XCoordinate").IsOptional().HasPrecision(19,5);
            Property(x => x.YCoordinate).HasColumnName("YCoordinate").IsOptional().HasPrecision(19,5);
            Property(x => x.XyCoordinateArray).HasColumnName("XYCoordinateArray").IsOptional().HasMaxLength(1073741823);
            Property(x => x.BaiduMapLongitude).HasColumnName("BaiduMapLongitude").IsOptional().HasPrecision(19,5);
            Property(x => x.BaiduMapLatitude).HasColumnName("BaiduMapLatitude").IsOptional().HasPrecision(19,5);
            Property(x => x.BaiduLatLngArray).HasColumnName("BaiduLatLngArray").IsOptional().HasMaxLength(1073741823);
            Property(x => x.GoogleMapLongitude).HasColumnName("GoogleMapLongitude").IsOptional().HasPrecision(19,5);
            Property(x => x.GoogleMapLatitude).HasColumnName("GoogleMapLatitude").IsOptional().HasPrecision(19,5);
            Property(x => x.GoogleLatLngArray).HasColumnName("GoogleLatLngArray").IsOptional().HasMaxLength(1073741823);
            Property(x => x.DistrictName).HasColumnName("DistrictName").IsOptional().HasMaxLength(255);
            Property(x => x.DistrictParentFullName).HasColumnName("DistrictParentFullName").IsOptional().HasMaxLength(255);
            Property(x => x.DistrictFullName).HasColumnName("DistrictFullName").IsOptional().HasMaxLength(255);
            Property(x => x.DistrictType).HasColumnName("DistrictType").IsOptional().HasMaxLength(255);
            Property(x => x.DescriptionDate).HasColumnName("DescriptionDate").IsOptional();
            Property(x => x.Alias).HasColumnName("Alias").IsOptional().HasMaxLength(255);
            Property(x => x.Code).HasColumnName("Code").IsOptional().HasMaxLength(255);
            Property(x => x.PostCode).HasColumnName("PostCode").IsOptional().HasMaxLength(255);
            Property(x => x.Pictures).HasColumnName("Pictures").IsOptional().HasMaxLength(255);
            Property(x => x.Comment).HasColumnName("Comment").IsOptional().HasMaxLength(255);
            Property(x => x.DespEndDate).HasColumnName("DespEndDate").IsOptional();
            Property(x => x.DisappearDate).HasColumnName("DisappearDate").IsOptional();
        }
    }

    // Picture
    internal class PictureConfiguration : EntityTypeConfiguration<Picture>
    {
        public PictureConfiguration()
        {
            ToTable("dbo.Picture");
            HasKey(x => x.Tid);

            Property(x => x.Tid).HasColumnName("TID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.TGuid).HasColumnName("TGuid").IsOptional();
            Property(x => x.Pid).HasColumnName("PID").IsOptional().HasMaxLength(255);
            Property(x => x.Expansion).HasColumnName("Expansion").IsOptional().HasMaxLength(2000);
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.Status).HasColumnName("Status").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("ModifyDate").IsOptional();
            Property(x => x.Title).HasColumnName("Title").IsOptional().HasMaxLength(255);
            Property(x => x.Format).HasColumnName("Format").IsOptional().HasMaxLength(255);
            Property(x => x.Width).HasColumnName("Width").IsOptional().HasMaxLength(255);
            Property(x => x.Height).HasColumnName("Height").IsOptional().HasMaxLength(255);
            Property(x => x.Path).HasColumnName("Path").IsOptional().HasMaxLength(255);
            Property(x => x.FileMd5).HasColumnName("FileMD5").IsOptional().HasMaxLength(255);
        }
    }

    // System_Parameter
    internal class SystemParameterConfiguration : EntityTypeConfiguration<SystemParameter>
    {
        public SystemParameterConfiguration()
        {
            ToTable("dbo.System_Parameter");
            HasKey(x => x.Tid);

            Property(x => x.Tid).HasColumnName("TID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.TGuid).HasColumnName("TGuid").IsOptional();
            Property(x => x.Pid).HasColumnName("PID").IsOptional().HasMaxLength(255);
            Property(x => x.Expansion).HasColumnName("Expansion").IsOptional().HasMaxLength(2000);
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.Status).HasColumnName("Status").IsOptional();
            Property(x => x.Name).HasColumnName("Name").IsOptional().HasMaxLength(255);
            Property(x => x.Title).HasColumnName("Title").IsOptional().HasMaxLength(255);
            Property(x => x.Value).HasColumnName("Value").IsOptional().HasMaxLength(255);
            Property(x => x.ParentId).HasColumnName("ParentID").IsOptional();

            // Foreign keys
            HasOptional(a => a.SystemParameter1).WithMany(b => b.SystemParameter2).HasForeignKey(c => c.ParentId); // FK65C5E93D45CE5E4F
        }
    }

}

