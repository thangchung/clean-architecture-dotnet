using System;
using System.Linq;
using System.Linq.Expressions;
using CoolStore.AppContracts.Dtos;
using Mapster.Utils;
using ProductService.Core.Entities;

namespace CoolStore.AppContracts.Dtos
{
    public static partial class ProductMapper
    {
        public static ProductDto AdaptToDto(this Product p1)
        {
            return p1 == null ? null : new ProductDto()
            {
                Name = p1.Name,
                Active = p1.Active,
                Quantity = p1.Quantity,
                Cost = p1.Cost,
                ProductCodeId = p1.ProductCodeId,
                Code = p1.Code == null ? null : new ProductCodeDto()
                {
                    Name = p1.Code.Name,
                    Id = p1.Code.Id,
                    Created = p1.Code.Created,
                    Updated = p1.Code.Updated
                },
                Returns = p1.Returns == null ? null : p1.Returns.Select<Return, ReturnDto>(funcMain1),
                Id = p1.Id,
                Created = p1.Created,
                Updated = p1.Updated
            };
        }
        public static ProductDto AdaptTo(this Product p3, ProductDto p4)
        {
            if (p3 == null)
            {
                return null;
            }
            ProductDto result = p4 ?? new ProductDto();
            
            result.Name = p3.Name;
            result.Active = p3.Active;
            result.Quantity = p3.Quantity;
            result.Cost = p3.Cost;
            result.ProductCodeId = p3.ProductCodeId;
            result.Code = funcMain2(p3.Code, result.Code);
            result.Returns = p3.Returns == null ? null : p3.Returns.Select<Return, ReturnDto>(funcMain3);
            result.Id = p3.Id;
            result.Created = p3.Created;
            result.Updated = p3.Updated;
            return result;
            
        }
        public static Expression<Func<Product, ProductDto>> ProjectToDto => p8 => new ProductDto()
        {
            Name = p8.Name,
            Active = p8.Active,
            Quantity = p8.Quantity,
            Cost = p8.Cost,
            ProductCodeId = p8.ProductCodeId,
            Code = p8.Code == null ? null : new ProductCodeDto()
            {
                Name = p8.Code.Name,
                Id = p8.Code.Id,
                Created = p8.Code.Created,
                Updated = p8.Code.Updated
            },
            Returns = p8.Returns.Select<Return, ReturnDto>(p9 => new ReturnDto()
            {
                ProductId = p9.ProductId,
                CustomerId = p9.CustomerId,
                Reason = Enum<ReturnReason>.ToString(p9.Reason),
                Note = p9.Note,
                Id = p9.Id,
                Created = p9.Created,
                Updated = p9.Updated
            }),
            Id = p8.Id,
            Created = p8.Created,
            Updated = p8.Updated
        };
        
        private static ReturnDto funcMain1(Return p2)
        {
            return p2 == null ? null : new ReturnDto()
            {
                ProductId = p2.ProductId,
                CustomerId = p2.CustomerId,
                Reason = Enum<ReturnReason>.ToString(p2.Reason),
                Note = p2.Note,
                Id = p2.Id,
                Created = p2.Created,
                Updated = p2.Updated
            };
        }
        
        private static ProductCodeDto funcMain2(ProductCode p5, ProductCodeDto p6)
        {
            if (p5 == null)
            {
                return null;
            }
            ProductCodeDto result = p6 ?? new ProductCodeDto();
            
            result.Name = p5.Name;
            result.Id = p5.Id;
            result.Created = p5.Created;
            result.Updated = p5.Updated;
            return result;
            
        }
        
        private static ReturnDto funcMain3(Return p7)
        {
            return p7 == null ? null : new ReturnDto()
            {
                ProductId = p7.ProductId,
                CustomerId = p7.CustomerId,
                Reason = Enum<ReturnReason>.ToString(p7.Reason),
                Note = p7.Note,
                Id = p7.Id,
                Created = p7.Created,
                Updated = p7.Updated
            };
        }
    }
}