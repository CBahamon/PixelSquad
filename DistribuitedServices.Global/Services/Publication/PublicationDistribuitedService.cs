using Domain.Base.Entities.Models;
using AplicationPixelSquad.Publication;

namespace DistribuitedServices.Global.Services.Publication;

public class PublicationDistribuitedService : IPublicationDistribuitedService
{
    private readonly IPublicationAdminService _iPublicationAdminService;


    public PublicationDistribuitedService(IPublicationAdminService iPublicationAdminService)
    {
        _iPublicationAdminService = iPublicationAdminService;
    }

    public String GetAllList()
    {
        return _iPublicationAdminService.GetAllList();
    }
}