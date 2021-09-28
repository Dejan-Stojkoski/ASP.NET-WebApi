﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Movies.Domain.Migrations
{
    [DbContext(typeof(MoviesDbContext))]
    [Migration("20210927182957_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Movies.Domain.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Genre")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "This is a comedy movie.",
                            Genre = 1,
                            Title = "The Hangover",
                            Year = 2009
                        },
                        new
                        {
                            Id = 2,
                            Description = "This is an action movie.",
                            Genre = 2,
                            Title = "Mortal Kombat",
                            Year = 2021
                        },
                        new
                        {
                            Id = 3,
                            Description = "This is a Sci-fi movie.",
                            Genre = 3,
                            Title = "Inception",
                            Year = 2010
                        },
                        new
                        {
                            Id = 4,
                            Description = "This is a comedy movie.",
                            Genre = 1,
                            Title = "Spy",
                            Year = 2015
                        });
                });

            modelBuilder.Entity("Movies.Domain.Models.MovieRent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("RentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("RentId");

                    b.ToTable("MovieRent");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MovieId = 2,
                            RentId = 1
                        },
                        new
                        {
                            Id = 2,
                            MovieId = 3,
                            RentId = 1
                        },
                        new
                        {
                            Id = 3,
                            MovieId = 1,
                            RentId = 2
                        },
                        new
                        {
                            Id = 4,
                            MovieId = 3,
                            RentId = 2
                        },
                        new
                        {
                            Id = 5,
                            MovieId = 1,
                            RentId = 3
                        });
                });

            modelBuilder.Entity("Movies.Domain.Models.Rent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Rents");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            UserId = 2
                        },
                        new
                        {
                            Id = 3,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("Movies.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Subscription")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FullName = "Jon Jonsky",
                            Password = "jonjon",
                            Subscription = 1,
                            Username = "jonS"
                        },
                        new
                        {
                            Id = 2,
                            FullName = "Jill Jillsky",
                            Password = "jilljill",
                            Subscription = 2,
                            Username = "jillJ"
                        },
                        new
                        {
                            Id = 3,
                            FullName = "Greg Gregsky",
                            Password = "greggreg",
                            Subscription = 1,
                            Username = "gregG"
                        });
                });

            modelBuilder.Entity("Movies.Domain.Models.MovieRent", b =>
                {
                    b.HasOne("Movies.Domain.Models.Movie", "Movie")
                        .WithMany("MovieRents")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Movies.Domain.Models.Rent", "Rent")
                        .WithMany("MovieRents")
                        .HasForeignKey("RentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Movies.Domain.Models.Rent", b =>
                {
                    b.HasOne("Movies.Domain.Models.User", "User")
                        .WithMany("Rents")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
