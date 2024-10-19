using Domain.Base.Entities.DataContext;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.DataAccess;

public interface IUnitOfWork : IDisposable
{
    ModelDbContext DbContext { get; set; }

    void Commit();

    Task<IDbContextTransaction> Transaction();
}