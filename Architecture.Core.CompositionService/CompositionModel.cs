﻿using Architecture.DataAccess.CustomerRepositories;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Core.CompositionService
{
    public class CompositionModel : DbContext
    {
        public CompositionModel(DbContextOptions options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CustomerEntityConfiguration());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
    /*
    public class CompositionModel : IModel
    {
    }

    public class CompositionDbContext : DbContext
    {
        private readonly MutableModelExtensions
        private readonly ModelBuilder _builder;
        public CompositionDbContext(DbContextOptions options) : base(options)
        {
            IMutableModel
            var _builder = new ModelBuilder(ConventionSet.);
            EntityTypeConfiguration;
            EntityTypeBuilder;
        }

        public void AddEntity<T>(IEntityTypeConfiguration<T> entity_config) where T : class
        {
            entity_config.Configure
            this.GetService<EntityTypeBuilder>().
            this.Model.
            entity_config.Configure()
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            throw new NotImplementedException("Use fluent API, fool!");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

    }
    */
}