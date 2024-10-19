using Domain.Base.Entities.Connection;
using Domain.Base.Entities.DataContext;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.DataAccess;

public class UnitOfWork :  ModelDbContext, IUnitOfWork
{
    
    public ModelDbContext DbContext { get; set; }
    
    public UnitOfWork() : base(ConnectionString.GetConnectionString())      
    {
        DbContext = this;
    }


    public void Commit()
    {
        this .SaveChanges();
    }

    public async Task<IDbContextTransaction> Transaction()
    {
        return await this.Database.BeginTransactionAsync();
    }
}