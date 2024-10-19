using DistribuitedServices.Global.Services.Publication;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Generic;
using Infrastructure.DataAccess.Repository.Publication;
using Microsoft.Extensions.DependencyInjection;
using AplicationPixelSquad.Publication;

namespace DistribuitedServices.Global.DependencyInjection;

public static class Ioc
{
    public static IServiceCollection? AddDependency(this IServiceCollection service)
    {
        //Inyectar los servicios
        try
        {
            //Inyectar los servicios del repositorio Generico
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            service.AddTransient<IPublicationDistribuitedService, PublicationDistribuitedService>();
            service.AddScoped<IPublicationAdminService, PublicationAdminService>();
            service.AddScoped<IPublicationRepository, PublicationRepository>();
            
            return service;
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());
            return null;
        }
    }
}