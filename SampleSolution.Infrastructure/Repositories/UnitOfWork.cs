﻿using Microsoft.EntityFrameworkCore;
using SampleSolution.Core.Abstractions;
using SampleSolution.Domain.Entities;
using SampleSolution.Infrastructure.Contexts;

namespace SampleSolution.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
    {
        UpdateAuditableEntities();
        return await _context.SaveChangesAsync();
    }

    private void UpdateAuditableEntities()
    {
        var entries = _context.ChangeTracker.Entries<IAuditable>();

        foreach (var entry in entries)
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Property(e => e.CreatedAt).CurrentValue = DateTimeOffset.UtcNow;
                    entry.Property(e => e.UpdatedAt).CurrentValue = DateTimeOffset.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Property(e => e.UpdatedAt).CurrentValue = DateTimeOffset.UtcNow;
                    break;
            }
    }
}