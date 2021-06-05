using System;
using System.Linq.Expressions;
using CoolStore.AppContracts.Dtos;
using SettingService.Core.Entities;

namespace CoolStore.AppContracts.Dtos
{
    public static partial class CountryMapper
    {
        public static CountryDto AdaptToDto(this Country p1)
        {
            return p1 == null ? null : new CountryDto()
            {
                Name = p1.Name,
                Id = p1.Id,
                Created = p1.Created,
                Updated = p1.Updated
            };
        }
        public static CountryDto AdaptTo(this Country p2, CountryDto p3)
        {
            if (p2 == null)
            {
                return null;
            }
            CountryDto result = p3 ?? new CountryDto();
            
            result.Name = p2.Name;
            result.Id = p2.Id;
            result.Created = p2.Created;
            result.Updated = p2.Updated;
            return result;
            
        }
        public static Expression<Func<Country, CountryDto>> ProjectToDto => p4 => new CountryDto()
        {
            Name = p4.Name,
            Id = p4.Id,
            Created = p4.Created,
            Updated = p4.Updated
        };
    }
}