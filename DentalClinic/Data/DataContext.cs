using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DentalClinic.Models;

namespace DentalClinic.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> tblPatients { get; set; } = default!;
        public DbSet<Treatment> tblTreatments { get; set; } = default!;
        public DbSet<Admin> tblAdmins { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          
            modelBuilder.Entity<Patient>().HasData(
                new Patient { PatientId = 1, PatientName = "John Doe", PatientAge = 30, Gender = eGender.Male},
                new Patient { PatientId = 2, PatientName = "Jane Smith", PatientAge = 25, Gender = eGender.Female}
            );

    
            modelBuilder.Entity<Treatment>().HasData(
                new Treatment { TreatmentId = 1,  Description = "Teeth Cleaning", Cost = 30},
                new Treatment { TreatmentId = 2,  Description = "Cavity Filling", Cost = 30 }
            );

        }

    }
}
