﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Kemijski_spojevi.Database
{
    public partial class DatabaseContext : DbContext
    {

        public virtual DbSet<Spoj> Spoj { get; set; }

        public virtual DbSet<Element> Element { get; set; }

        public virtual DbSet<SpojElement> SpojElement { get; set; }

        public virtual DbSet<VrstaSpoja> VrstaSpoja { get; set; }

        public DatabaseContext() { }

        public DatabaseContext( DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Element>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasColumnType("nchar(10)");
            });

            modelBuilder.Entity<Spoj>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Spoj)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Spoj_VrstaSpoja");


            });

            modelBuilder.Entity<SpojElement>(entity =>
            {
                entity.HasKey(e => new { e.SpojId, e.ElementId });

                entity.HasOne(d => d.Element)
                    .WithMany(p => p.SpojElement)
                    .HasForeignKey(d => d.ElementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpojElement_Element");

                entity.HasOne(d => d.Spoj)
                    .WithMany(p => p.SpojElement)
                    .HasForeignKey(d => d.SpojId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SpojElement_Spoj");
            });

            modelBuilder.Entity<VrstaSpoja>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=LUKA\SERVERZAOPP;Initial Catalog=BazaZaKemSpojeve;Integrated Security=True");
            }
        }
    }
}
