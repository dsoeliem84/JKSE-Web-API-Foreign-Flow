﻿// <auto-generated />
using System;
using JKSE_Web_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace s.Migrations
{
    [DbContext(typeof(JkseDataContext))]
    partial class JkseDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JKSE_Web_API.Models.DailyStats", b =>
                {
                    b.Property<DateTime>("DateData")
                        .HasColumnType("Date");

                    b.Property<decimal>("ForeignNetBuy")
                        .HasColumnType("Decimal (8,3)");

                    b.Property<decimal>("IndexPrice")
                        .HasColumnType("Decimal (8,3)");

                    b.Property<decimal>("Turnover")
                        .HasColumnType("Decimal (8,3)");

                    b.Property<decimal>("VolumeTransaction")
                        .HasColumnType("Decimal (8,3)");

                    b.HasKey("DateData");

                    b.ToTable("DailyStats");
                });

            modelBuilder.Entity("JKSE_Web_API.Models.ForeignFlow", b =>
                {
                    b.Property<DateTime>("DateData")
                        .HasColumnType("Date");

                    b.Property<string>("TickerCode")
                        .HasColumnType("Varchar(4)");

                    b.Property<decimal>("DominationRatio")
                        .HasColumnType("Decimal (8,2)");

                    b.Property<decimal>("NetRatioVolume")
                        .HasColumnType("Decimal (8,2)");

                    b.Property<int>("TypeFlow")
                        .HasColumnType("int");

                    b.Property<long>("ValueTotal")
                        .HasColumnType("bigint");

                    b.Property<int>("VolumeBuy")
                        .HasColumnType("int");

                    b.Property<int>("VolumeSell")
                        .HasColumnType("int");

                    b.Property<int>("VolumeTotal")
                        .HasColumnType("int");

                    b.HasKey("DateData", "TickerCode");

                    b.ToTable("ForeignFlow");
                });

            modelBuilder.Entity("JKSE_Web_API.Models.ForeignFlowReport", b =>
                {
                    b.Property<int>("AccumulationValue")
                        .HasColumnType("int");

                    b.Property<int>("NetBuyVolume")
                        .HasColumnType("int");

                    b.Property<int>("NetForeignVolume")
                        .HasColumnType("int");

                    b.Property<int>("NetSellVolume")
                        .HasColumnType("int");

                    b.Property<decimal>("RatioNetFlow")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RatioNetFlowText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TickerCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalDays")
                        .HasColumnType("int");

                    b.Property<decimal>("VolatilityLevel")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("VolatilityLevelText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("VolumeTotal")
                        .HasColumnType("bigint");

                    b.ToTable((string)null);

                    b.ToView(null, (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
