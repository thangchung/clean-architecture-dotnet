using System;
using System.Reflection;
using CustomerService.Core.Entities;
using Mapster;
using ProductService.Core.Entities;
using SettingService.Core.Entities;

namespace CoolStore.AppContracts
{
    public class MappingRegister : ICodeGenerationRegister
    {
        public void Register(CodeGenerationConfig config)
        {
            config.AdaptTo("[name]Dto", MapType.Map | MapType.MapToTarget | MapType.Projection)
                .ApplyDefaultRule();

            config.GenerateMapper("[name]Mapper")
                .ForType<Product>()
                .ForType<Customer>()
                .ForType<Country>();
        }
    }

    static class RegisterExtensions
    {
        public static AdaptAttributeBuilder ApplyDefaultRule(this AdaptAttributeBuilder builder)
        {
            return builder
                    .ForAllTypesInNamespace(Assembly.GetAssembly(typeof(Product))!, "ProductService.Core.Entities")
                    .ForAllTypesInNamespace(Assembly.GetAssembly(typeof(Customer))!, "CustomerService.Core.Entities")
                    .ForAllTypesInNamespace(Assembly.GetAssembly(typeof(Country))!, "SettingService.Core.Entities")
                    .ExcludeTypes(type => type.IsEnum)
                    .AlterType(type => type.IsEnum || Nullable.GetUnderlyingType(type)?.IsEnum == true, typeof(string))
                    .ShallowCopyForSameType(true)
                    .ForType<Product>(cfg => cfg.Ignore(it => it.DomainEvents))
                    .ForType<ProductCode>(cfg => cfg.Ignore(it => it.DomainEvents))
                    //.ForType<Return>(cfg => cfg.Ignore(it => it.DomainEvents).Ignore(it => it.Product))
                    .ForType<Customer>(cfg => cfg.Ignore(it => it.DomainEvents).Ignore(it => it.CreditCards))
                    .ForType<CreditCard>(cfg => cfg.Ignore(it => it.DomainEvents).Ignore(it => it.Customer))
                    .ForType<Country>(cfg => cfg.Ignore(it => it.DomainEvents))
                ;
        }
    }
}
