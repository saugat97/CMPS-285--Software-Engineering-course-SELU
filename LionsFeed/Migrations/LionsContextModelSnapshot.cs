﻿// <auto-generated />
using LionsFeed.Data;
using LionsFeed.Models;
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
    partial class LionsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("LionsFeed.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CommentDate");

                    b.Property<string>("CommentText");

                    b.Property<int>("PostId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
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

            modelBuilder.Entity("LionsFeed.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActorId");

                    b.Property<DateTime>("DateTime");

                    b.Property<int>("PostID");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("PostID");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("LionsFeed.Models.Post", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArtistId");

                    b.Property<int>("CommentsCount");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("FlagCount");

                    b.Property<string>("ImageUrl");

                    b.Property<bool>("IsFlagged");

                    b.Property<bool>("IsLiked");

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

            modelBuilder.Entity("LionsFeed.Models.UserNotification", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("NotificationId");

                    b.Property<bool>("IsRead");

                    b.HasKey("UserId", "NotificationId");

                    b.HasIndex("NotificationId");

                    b.ToTable("UserNotifications");
                });

            modelBuilder.Entity("LionsFeed.Models.Comment", b =>
                {
                    b.HasOne("LionsFeed.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LionsFeed.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
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

            modelBuilder.Entity("LionsFeed.Models.Notification", b =>
                {
                    b.HasOne("LionsFeed.Models.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostID")
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

            modelBuilder.Entity("LionsFeed.Models.UserNotification", b =>
                {
                    b.HasOne("LionsFeed.Models.Notification", "Notification")
                        .WithMany()
                        .HasForeignKey("NotificationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LionsFeed.Models.ApplicationUser", "User")
                        .WithMany("UserNotifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
