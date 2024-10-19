using Infrastructure.DataAccess.Generic;
using Domain.Base.Entities.Models;

namespace Infrastructure.DataAccess.Repository.Publication
{

    public interface IPublicationRepository : IGenericRepository<Domain.Base.Entities.Models.Publication>
    {
        List<Domain.Base.Entities.Models.Publication> getAllPublication();
    }

}