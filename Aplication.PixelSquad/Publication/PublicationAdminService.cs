using Domain.Base.Entities.Models;
using Infrastructure.DataAccess.Repository.Publication;
using Newtonsoft.Json;

namespace AplicationPixelSquad.Publication;

public class PublicationAdminService : IPublicationAdminService
{
    private readonly IPublicationRepository _iPublicationRepository;
    
    public PublicationAdminService(IPublicationRepository iPublicationRepository)
    {
        _iPublicationRepository = iPublicationRepository;
    }

    public String GetAllList()
    {
        try
        {
            var data = _iPublicationRepository.getAllPublication();
            // return new ActionResult(false, "", data);
            var result = new ActionResult(true, "", data);
           return JsonConvert.SerializeObject(result);
            // Console.WriteLine(json);
        }
        catch (Exception ex)
        {
            return  ex.Message;
        }
    }

}