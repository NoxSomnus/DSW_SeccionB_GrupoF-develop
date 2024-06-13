﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using UCABPagaloTodoMS.Infrastructure.Database;

#nullable disable

namespace UCABPagaloTodoMS.Infrastructure.Migrations
{
    [DbContext(typeof(UCABPagaloTodoDbContext))]
    [Migration("20230618192143_Conciliation_File_Config")]
    partial class Conciliation_File_Config
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("UCABPagaloTodoMS.Core.Entities.BillEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double?>("Amount")
                        .HasColumnType("double precision");

                    b.Property<string>("ContractNumber")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PaymentOptionId")
                        .HasColumnType("uuid");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PaymentOptionId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("UserId");

                    b.ToTable("BillEntities");
                });

            modelBuilder.Entity("UCABPagaloTodoMS.Core.Entities.ConciliationFileConfigureEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<bool>("IncludeAmount")
                        .HasColumnType("boolean");

                    b.Property<bool>("IncludeBillDate")
                        .HasColumnType("boolean");

                    b.Property<bool>("IncludeContractnumber")
                        .HasColumnType("boolean");

                    b.Property<bool>("IncludeDni")
                        .HasColumnType("boolean");

                    b.Property<bool>("IncludeEmail")
                        .HasColumnType("boolean");

                    b.Property<bool>("IncludeLastname")
                        .HasColumnType("boolean");

                    b.Property<bool>("IncludeName")
                        .HasColumnType("boolean");

                    b.Property<bool>("IncludePhoneNumber")
                        .HasColumnType("boolean");

                    b.Property<bool>("IncludeUsername")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ProviderId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProviderId");

                    b.HasIndex("ServiceId");

                    b.ToTable("ConciliationFileConfigureEntities");
                });

            modelBuilder.Entity("UCABPagaloTodoMS.Core.Entities.PaymentByConciliationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<double?>("Debt")
                        .HasColumnType("double precision");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.HasIndex("UserId");

                    b.ToTable("PaymentByConciliationEntities");
                });

            modelBuilder.Entity("UCABPagaloTodoMS.Core.Entities.PaymentDetailsEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BillId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("FieldContent")
                        .HasColumnType("text");

                    b.Property<Guid>("RequiredFieldId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.HasIndex("RequiredFieldId");

                    b.ToTable("PaymentDetailsEntities");
                });

            modelBuilder.Entity("UCABPagaloTodoMS.Core.Entities.PaymentOptionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.ToTable("PaymentOptionEntities");
                });

            modelBuilder.Entity("UCABPagaloTodoMS.Core.Entities.PaymentRequiredFieldEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("FieldName")
                        .HasColumnType("text");

                    b.Property<string>("Length")
                        .HasColumnType("text");

                    b.Property<Guid>("PaymentOptionId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.Property<bool?>("isNumber")
                        .HasColumnType("boolean");

                    b.Property<bool?>("isString")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("PaymentOptionId");

                    b.ToTable("PaymentRequiredFieldEntities");
                });

            modelBuilder.Entity("UCABPagaloTodoMS.Core.Entities.ServiceEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ContactNumber")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<Guid>("ProviderId")
                        .HasColumnType("uuid");

                    b.Property<string>("ServiceName")
                        .HasColumnType("text");

                    b.Property<string>("TypeService")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProviderId");

                    b.ToTable("ServiceEntities");
                });

            modelBuilder.Entity("UCABPagaloTodoMS.Core.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Dni")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Lastname")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool?>("Status")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.Property<string>("VerificationCode")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserEntities");

                    b.HasDiscriminator<string>("Discriminator").HasValue("UserEntity");
                });

            modelBuilder.Entity("UCABPagaloTodoMS.Core.Entities.ValoresEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Apellido")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Identificacion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Valores");
                });

            modelBuilder.Entity("UCABPagaloTodoMS.Core.Entities.AdminEntity", b =>
                {
                    b.HasBaseType("UCABPagaloTodoMS.Core.Entities.UserEntity");

                    b.Property<bool?>("isAdmin")
                        .HasColumnType("boolean");

                    b.HasDiscriminator().HasValue("AdminEntity");
                });

            modelBuilder.Entity("UCABPagaloTodoMS.Core.Entities.ProviderEntity", b =>
                {
                    b.HasBaseType("UCABPagaloTodoMS.Core.Entities.UserEntity");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasDiscriminator().HasValue("ProviderEntity");
                });

            modelBuilder.Entity("UCABPagaloTodoMS.Core.Entities.BillEntity", b =>
                {
                    b.HasOne("UCABPagaloTodoMS.Core.Entities.PaymentOptionEntity", "PaymentOption")
                        .WithMany()
                        .HasForeignKey("PaymentOptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UCABPagaloTodoMS.Core.Entities.ServiceEntity", "Service")
                        .WithMany("Bills")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UCABPagaloTodoMS.Core.Entities.UserEntity", "User")
                        .WithMany("Bills")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentOption");

                    b.Navigation("Service");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UCABPagaloTodoMS.Core.Entities.ConciliationFileConfigureEntity", b =>
                {
                    b.HasOne("UCABPagaloTodoMS.Core.Entities.ProviderEntity", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UCABPagaloTodoMS.Core.Entities.ServiceEntity", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Provider");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("UCABPagaloTodoMS.Core.Entities.PaymentByConciliationEntity", b =>
                {
                    b.HasOne("UCABPagaloTodoMS.Core.Entities.ServiceEntity", "Service")
                        .WithMany("PaymentByConciliations")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UCABPagaloTodoMS.Core.Entities.UserEntity", "User")
                        .WithMany("PaymentByConciliations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UCABPagaloTodoMS.Core.Entities.PaymentDetailsEntity", b =>
                {
                    b.HasOne("UCABPagaloTodoMS.Core.Entities.BillEntity", "Bill")
                        .WithMany("PaymentDetails")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UCABPagaloTodoMS.Core.Entities.PaymentRequiredFieldEntity", "RequiredField")
                        .WithMany("PaymentDetails")
                        .HasForeignKey("RequiredFieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("RequiredField");
                });

            modelBuilder.Entity("UCABPagaloTodoMS.Core.Entities.PaymentOptionEntity", b =>
                {
                    b.HasOne("UCABPagaloTodoMS.Core.Entities.ServiceEntity", "Service")
                        .WithMany("PaymentValidation")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");
                });

            modelBuilder.Entity("UCABPagaloTodoMS.Core.Entities.PaymentRequiredFieldEntity", b =>
                {
                    b.HasOne("UCABPagaloTodoMS.Core.Entities.PaymentOptionEntity", "PaymentOption")
                        .WithMany("RequiredFields")
                        .HasForeignKey("PaymentOptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentOption");
                });

            modelBuilder.Entity("UCABPagaloTodoMS.Core.Entities.ServiceEntity", b =>
                {
                    b.HasOne("UCABPagaloTodoMS.Core.Entities.ProviderEntity", "Provider")
                        .WithMany("Service")
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("UCABPagaloTodoMS.Core.Entities.BillEntity", b =>
                {
                    b.Navigation("PaymentDetails");
                });

            modelBuilder.Entity("UCABPagaloTodoMS.Core.Entities.PaymentOptionEntity", b =>
                {
                    b.Navigation("RequiredFields");
                });

            modelBuilder.Entity("UCABPagaloTodoMS.Core.Entities.PaymentRequiredFieldEntity", b =>
                {
                    b.Navigation("PaymentDetails");
                });

            modelBuilder.Entity("UCABPagaloTodoMS.Core.Entities.ServiceEntity", b =>
                {
                    b.Navigation("Bills");

                    b.Navigation("PaymentByConciliations");

                    b.Navigation("PaymentValidation");
                });

            modelBuilder.Entity("UCABPagaloTodoMS.Core.Entities.UserEntity", b =>
                {
                    b.Navigation("Bills");

                    b.Navigation("PaymentByConciliations");
                });

            modelBuilder.Entity("UCABPagaloTodoMS.Core.Entities.ProviderEntity", b =>
                {
                    b.Navigation("Service");
                });
#pragma warning restore 612, 618
        }
    }
}
