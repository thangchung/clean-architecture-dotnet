using System;
using System.Threading.Tasks;
using CoolStore.AppContracts.Common;
using CoolStore.AppContracts.Dtos;
using RestEase;

namespace CoolStore.AppContracts.RestApi
{
    public interface ICountryApi
    {
        [Get("api/v1/countries/{countryId}")]
        Task<ResultDto<CountryDto>> GetCountryByIdAsync([Path] Guid countryId);
    }
}
