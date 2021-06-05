using System;
using System.Threading.Tasks;
using CoolStore.AppContracts.Dtos;
using N8T.Core.Domain;
using RestEase;

namespace CoolStore.AppContracts.RestApi
{
    public interface ICountryApi
    {
        [Get("api/v1/countries/{countryId}")]
        Task<ResultModel<CountryDto>> GetCountryByIdAsync([Path] Guid countryId);
    }
}
