using Domain.Base.Entities.Connection;
using Domain.Base.Entities.DataContext;
using Infrastructure.DataAccess.Generic;

namespace Infrastructure.DataAccess.Repository.Publication
{

    public class PublicationRepository : GenericRepository<Domain.Base.Entities.Models.Publication>,IPublicationRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public PublicationRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Domain.Base.Entities.Models.Publication> getAllPublication()
        {
            return _unitOfWork.DbContext.Publication.ToList();
        }
        
    }
}