﻿// <auto-generated />
using System;
using MediaZone.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MediaZone.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230520071708_UnrequiredImageTitle")]
    partial class UnrequiredImageTitle
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AppRoleAppUser", b =>
                {
                    b.Property<Guid>("RolesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("AppRoleAppUser");
                });

            modelBuilder.Entity("MediaZone.Data.Entities.Face", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RekognitionFaceId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Faces");
                });

            modelBuilder.Entity("MediaZone.Data.Entities.Folder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ParentFolderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("ParentFolderId");

                    b.ToTable("Folders");
                });

            modelBuilder.Entity("MediaZone.Data.Entities.FolderSubscription", b =>
                {
                    b.Property<Guid>("FolderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubscriberId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FolderId", "SubscriberId");

                    b.HasIndex("SubscriberId");

                    b.ToTable("FolderSubscriptions");
                });

            modelBuilder.Entity("MediaZone.Data.Entities.Identity.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("MediaZone.Data.Entities.Identity.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("HomeFolderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("MediaZone.Data.Entities.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("FolderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OriginalFilename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("SizeInBytes")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("FolderId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("MediaZone.Data.Entities.ImageFace", b =>
                {
                    b.Property<Guid?>("FaceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ImageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("Confirmed")
                        .HasColumnType("bit");

                    b.Property<float>("Similarity")
                        .HasColumnType("real");

                    b.HasKey("FaceId", "ImageId");

                    b.HasIndex("ImageId");

                    b.ToTable("ImagesFaces");
                });

            modelBuilder.Entity("MediaZone.Data.Entities.JoinEntities.FolderShare", b =>
                {
                    b.Property<Guid>("AllowedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FolderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AllowedUserId", "FolderId");

                    b.HasIndex("FolderId");

                    b.ToTable("FolderShares");
                });

            modelBuilder.Entity("MediaZone.Data.Entities.JoinEntities.SubjectTagJoinModel", b =>
                {
                    b.Property<Guid>("SubjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TagId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TagText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubjectId", "TagId");

                    b.ToTable("SubjectsTags");

                    b.HasDiscriminator<string>("Discriminator").HasValue("SubjectTagJoinModel");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("MediaZone.Data.Entities.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TagText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Tag");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MediaZone.Data.Entities.JoinEntities.FolderTagJoinModel", b =>
                {
                    b.HasBaseType("MediaZone.Data.Entities.JoinEntities.SubjectTagJoinModel");

                    b.ToTable("SubjectsTags");

                    b.HasDiscriminator().HasValue("FLDR");
                });

            modelBuilder.Entity("MediaZone.Data.Entities.JoinEntities.ImageTagJoinModel", b =>
                {
                    b.HasBaseType("MediaZone.Data.Entities.JoinEntities.SubjectTagJoinModel");

                    b.ToTable("SubjectsTags");

                    b.HasDiscriminator().HasValue("IMG");
                });

            modelBuilder.Entity("MediaZone.Data.Entities.FolderTag", b =>
                {
                    b.HasBaseType("MediaZone.Data.Entities.Tag");

                    b.ToTable("Tags");

                    b.HasDiscriminator().HasValue("FolderTag");
                });

            modelBuilder.Entity("MediaZone.Data.Entities.ImageTag", b =>
                {
                    b.HasBaseType("MediaZone.Data.Entities.Tag");

                    b.ToTable("Tags");

                    b.HasDiscriminator().HasValue("ImageTag");
                });

            modelBuilder.Entity("AppRoleAppUser", b =>
                {
                    b.HasOne("MediaZone.Data.Entities.Identity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediaZone.Data.Entities.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MediaZone.Data.Entities.Face", b =>
                {
                    b.HasOne("MediaZone.Data.Entities.Identity.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MediaZone.Data.Entities.Folder", b =>
                {
                    b.HasOne("MediaZone.Data.Entities.Identity.AppUser", "Owner")
                        .WithMany("OwnedFolders")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediaZone.Data.Entities.Folder", "ParentFolder")
                        .WithMany("ChildFolders")
                        .HasForeignKey("ParentFolderId");

                    b.Navigation("Owner");

                    b.Navigation("ParentFolder");
                });

            modelBuilder.Entity("MediaZone.Data.Entities.FolderSubscription", b =>
                {
                    b.HasOne("MediaZone.Data.Entities.Folder", "Folder")
                        .WithMany("FolderSubscriptions")
                        .HasForeignKey("FolderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MediaZone.Data.Entities.Identity.AppUser", "Subscriber")
                        .WithMany("FolderSubscriptions")
                        .HasForeignKey("SubscriberId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Folder");

                    b.Navigation("Subscriber");
                });

            modelBuilder.Entity("MediaZone.Data.Entities.Image", b =>
                {
                    b.HasOne("MediaZone.Data.Entities.Folder", "Folder")
                        .WithMany("Images")
                        .HasForeignKey("FolderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MediaZone.Data.Entities.Identity.AppUser", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Folder");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("MediaZone.Data.Entities.ImageFace", b =>
                {
                    b.HasOne("MediaZone.Data.Entities.Face", "Face")
                        .WithMany("FacesImages")
                        .HasForeignKey("FaceId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MediaZone.Data.Entities.Image", "Image")
                        .WithMany("ImagesFaces")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Face");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("MediaZone.Data.Entities.JoinEntities.FolderShare", b =>
                {
                    b.HasOne("MediaZone.Data.Entities.Identity.AppUser", "AllowedUser")
                        .WithMany("FolderShares")
                        .HasForeignKey("AllowedUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MediaZone.Data.Entities.Folder", "Folder")
                        .WithMany("FolderShares")
                        .HasForeignKey("FolderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AllowedUser");

                    b.Navigation("Folder");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("MediaZone.Data.Entities.Identity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("MediaZone.Data.Entities.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("MediaZone.Data.Entities.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("MediaZone.Data.Entities.Identity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediaZone.Data.Entities.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("MediaZone.Data.Entities.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MediaZone.Data.Entities.JoinEntities.FolderTagJoinModel", b =>
                {
                    b.HasOne("MediaZone.Data.Entities.Folder", "Folder")
                        .WithMany("FoldersTags")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MediaZone.Data.Entities.FolderTag", "Tag")
                        .WithMany("FolderTags")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Folder");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("MediaZone.Data.Entities.JoinEntities.ImageTagJoinModel", b =>
                {
                    b.HasOne("MediaZone.Data.Entities.Image", "Image")
                        .WithMany("ImagesTags")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MediaZone.Data.Entities.ImageTag", "Tag")
                        .WithMany("ImageTags")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Image");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("MediaZone.Data.Entities.Face", b =>
                {
                    b.Navigation("FacesImages");
                });

            modelBuilder.Entity("MediaZone.Data.Entities.Folder", b =>
                {
                    b.Navigation("ChildFolders");

                    b.Navigation("FolderShares");

                    b.Navigation("FolderSubscriptions");

                    b.Navigation("FoldersTags");

                    b.Navigation("Images");
                });

            modelBuilder.Entity("MediaZone.Data.Entities.Identity.AppUser", b =>
                {
                    b.Navigation("FolderShares");

                    b.Navigation("FolderSubscriptions");

                    b.Navigation("OwnedFolders");
                });

            modelBuilder.Entity("MediaZone.Data.Entities.Image", b =>
                {
                    b.Navigation("ImagesFaces");

                    b.Navigation("ImagesTags");
                });

            modelBuilder.Entity("MediaZone.Data.Entities.FolderTag", b =>
                {
                    b.Navigation("FolderTags");
                });

            modelBuilder.Entity("MediaZone.Data.Entities.ImageTag", b =>
                {
                    b.Navigation("ImageTags");
                });
#pragma warning restore 612, 618
        }
    }
}
