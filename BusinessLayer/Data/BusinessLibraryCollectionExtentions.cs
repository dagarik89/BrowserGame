using BusinessLayer.Services;
using DataLayer.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Data
{
    public static class BusinessLibraryCollectionExtentions
    {
        public static IServiceCollection AddBusinessLibraryCollection(this IServiceCollection services)
        {
            services.AddScoped<IPersonsBusinessService, PersonsBusinessService>();
            services.AddScoped<IUserBusinessService, UserBusinessService>();

            return services;
        }
    }
}