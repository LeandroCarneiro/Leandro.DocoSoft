﻿using System;
using System.Threading.Tasks;
using System.Threading;
using SertaoArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SertaoArch.Domain.Interfaces
{
    public interface IDbContext : IDisposable
    {
        DbSet<User> TblUsers { get; set; }
        DbSet<TEntity> GetEntity<TEntity>() where TEntity : EntityBase<long>;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
