﻿// <auto-generated />
using System;
using ChatServer.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ChatServer.DAL.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ChatServer.DAL.Models.Connection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Connected");

                    b.Property<DateTimeOffset>("CreatedTime");

                    b.Property<int>("EntityStatus");

                    b.Property<DateTimeOffset>("UpdatedTime");

                    b.Property<string>("UserAgent");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Connection");
                });

            modelBuilder.Entity("ChatServer.DAL.Models.Conversation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedTime");

                    b.Property<int>("EntityStatus");

                    b.Property<DateTimeOffset>("UpdatedTime");

                    b.HasKey("Id");

                    b.ToTable("Conversation");
                });

            modelBuilder.Entity("ChatServer.DAL.Models.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedTime");

                    b.Property<int>("EntityStatus");

                    b.Property<string>("MessageContent")
                        .IsRequired();

                    b.Property<DateTimeOffset>("UpdatedTime");

                    b.Property<Guid>("UserConversationId");

                    b.HasKey("Id");

                    b.HasIndex("UserConversationId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("ChatServer.DAL.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AvatarUrl");

                    b.Property<DateTimeOffset>("CreatedTime");

                    b.Property<string>("DisplayName")
                        .HasMaxLength(40);

                    b.Property<int>("EntityStatus");

                    b.Property<DateTimeOffset>("UpdatedTime");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ChatServer.DAL.Models.UserConversation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ConversationId");

                    b.Property<DateTimeOffset>("CreatedTime");

                    b.Property<int>("EntityStatus");

                    b.Property<DateTimeOffset>("UpdatedTime");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ConversationId");

                    b.HasIndex("UserId");

                    b.ToTable("UserConversation");
                });

            modelBuilder.Entity("ChatServer.DAL.Models.Connection", b =>
                {
                    b.HasOne("ChatServer.DAL.Models.User", "User")
                        .WithMany("Connections")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ChatServer.DAL.Models.Message", b =>
                {
                    b.HasOne("ChatServer.DAL.Models.UserConversation", "UserConversation")
                        .WithMany("Messages")
                        .HasForeignKey("UserConversationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ChatServer.DAL.Models.UserConversation", b =>
                {
                    b.HasOne("ChatServer.DAL.Models.Conversation", "Conversation")
                        .WithMany("UserConversations")
                        .HasForeignKey("ConversationId");

                    b.HasOne("ChatServer.DAL.Models.User", "User")
                        .WithMany("UserConversations")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
