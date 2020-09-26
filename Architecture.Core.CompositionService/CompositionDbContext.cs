using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Architecture.Core.GenericDAL
{
    public class CompositionModel : IModel
    {
    }

    public class CompositionDbContext: DbContext
    {
        private readonly MutableModelExtensions
        private readonly ModelBuilder _builder;
        public CompositionDbContext(DbContextOptions options): base(options)
        {
            IMutableModel
            var _builder = new ModelBuilder();
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
}
