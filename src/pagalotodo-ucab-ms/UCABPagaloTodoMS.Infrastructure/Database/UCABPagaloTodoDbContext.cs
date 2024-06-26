﻿using System.Linq.Expressions;
using System.Reflection;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace UCABPagaloTodoMS.Infrastructure.Database;


public class UCABPagaloTodoDbContext : DbContext, IUCABPagaloTodoDbContext
{
    private readonly ILogger<UCABPagaloTodoDbContext> _logger;
    public UCABPagaloTodoDbContext(DbContextOptions<UCABPagaloTodoDbContext> options, ILogger<UCABPagaloTodoDbContext> logger)
        : base(options)
    {
        _logger = logger;
    }

    public virtual DbSet<ValoresEntity> Valores { get; set; } = null!;

    public virtual DbSet<UserEntity> UserEntities { get; set; } = null!;

    public virtual DbSet<AdminEntity> AdminEntities { get; set; } = null!;

	public virtual DbSet<ProviderEntity> ProviderEntities { get; set; } = null!;

	public virtual DbSet<ServiceEntity> ServiceEntities { get; set; } = null!;

	public virtual DbSet<BillEntity> BillEntities { get; set; } = null!;

    public virtual DbSet<PaymentOptionEntity> PaymentOptionEntities { get; set; } = null!;
    public virtual DbSet<PaymentRequiredFieldEntity> PaymentRequiredFieldEntities { get; set; } = null!;
    public virtual DbSet<PaymentDetailsEntity> PaymentDetailsEntities { get; set; } = null!;
    public virtual DbSet<PaymentByConciliationEntity> PaymentByConciliationEntities { get; set; } = null!;
    public virtual DbSet<ConciliationFileConfigureEntity> ConciliationFileConfigureEntities { get; set; } = null!;


    public DbContext DbContext
    {
        get
        {
            return this;
        }
    }

    public IDbContextTransactionProxy BeginTransaction()
    {
        return new DbContextTransactionProxy(this);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    virtual public void SetPropertyIsModifiedToFalse<TEntity, TProperty>(TEntity entity,
        Expression<Func<TEntity, TProperty>> propertyExpression) where TEntity : class
    {
        Entry(entity).Property(propertyExpression).IsModified = false;
    }

    virtual public void ChangeEntityState<TEntity>(TEntity entity, EntityState state)
    {
        if (entity != null)
        {
            Entry(entity).State = state;
        }
    }

    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
                Entry((BaseEntity)entityEntry.Entity).Property(x => x.CreatedAt).IsModified = false;
                Entry((BaseEntity)entityEntry.Entity).Property(x => x.CreatedBy).IsModified = false;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> SaveChangesAsync(string user, CancellationToken cancellationToken = default)
    {
        var state = new List<EntityState> { EntityState.Added, EntityState.Modified };

        var entries = ChangeTracker.Entries().Where(e =>
            e.Entity is BaseEntity && state.Any(s => e.State == s)
        );

        var dt = DateTime.UtcNow;

        foreach (var entityEntry in entries)
        {
            var entity = (BaseEntity)entityEntry.Entity;

            if (entityEntry.State == EntityState.Added)
            {
                entity.CreatedAt = dt;
                entity.CreatedBy = user;
                Entry(entity).Property(x => x.UpdatedAt).IsModified = false;
                Entry(entity).Property(x => x.UpdatedBy).IsModified = false;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entity.UpdatedAt = dt;
                entity.UpdatedBy = user;
                Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                Entry(entity).Property(x => x.CreatedBy).IsModified = false;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> SaveEfContextChanges(CancellationToken cancellationToken = default)
    {
        return await SaveChangesAsync(cancellationToken) >= 0;
    }

    public async Task<bool> SaveEfContextChanges(string user, CancellationToken cancellationToken = default)
    { 
        try
        {
         return await SaveChangesAsync(user, cancellationToken) >= 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al guardar los cambios en el contexto de EF");
            throw;
        }
    }
}
