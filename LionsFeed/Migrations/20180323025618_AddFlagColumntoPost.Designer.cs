﻿// <auto-generated />
using LionsFeed.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace LionsFeed.Migrations
{
    [DbContext(typeof(LionsContext))]
    [Migration("20180323025618_AddFlagColumntoPost")]
    partial class AddFlagColumntoPost
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LionsFeed.Models.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("Gender");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Role");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LionsFeed.Models.Flag", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PostId");

                    b.Property<int>("UserId");

                    b.HasKey("ID");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Flags");
                });

            modelBuilder.Entity("LionsFeed.Models.Post", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArtistId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("FlagCount");

                    b.Property<int>("LikeCount");

                    b.Property<string>("PostText");

                    b.HasKey("ID");

                    b.HasIndex("ArtistId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("LionsFeed.Models.Upvote", b =>
                {
                    b.Property<int>("PostId");

                    b.Property<int>("UpvoterId");

                    b.HasKey("PostId", "UpvoterId");

                    b.HasIndex("UpvoterId");

                    b.ToTable("Upvotes");
                });

            modelBuilder.Entity("LionsFeed.Models.Flag", b =>
                {
                    b.HasOne("LionsFeed.Models.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LionsFeed.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LionsFeed.Models.Post", b =>
                {
                    b.HasOne("LionsFeed.Models.ApplicationUser", "Artist")
                        .WithMany()
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LionsFeed.Models.Upvote", b =>
                {
                    b.HasOne("LionsFeed.Models.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LionsFeed.Models.ApplicationUser", "UpVoter")
                        .WithMany()
                        .HasForeignKey("UpvoterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
