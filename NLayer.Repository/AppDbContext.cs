﻿using Microsoft.EntityFrameworkCore;
using NLayer.Core;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Assembly=> class library ler.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
            
            //bu sayede ayrı classlarda tanımladığımız configuration ayarlarını uygulamasını sağladık. Bu assembly içerisinden yapıyor. Nasıl anlıyor IEntityTypeConfiguration interfacesini implement eden classlara bakıyor. GetExecute da çalışmış olduğun assemblyi tara demiş olduk.
        }
    }
}
