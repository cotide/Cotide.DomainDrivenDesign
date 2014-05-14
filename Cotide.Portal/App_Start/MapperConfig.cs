using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper; 
using Cotide.Domain.Dtos;
using Cotide.Domain.Entity;
using Cotide.Framework.Mapper;

namespace Cotide.Portal.App_Start
{
    public class MapperConfig
    {
        /// <summary>
        /// 注册Mapping规则
        /// </summary>
        public static void Register()
        { 
                Mapper.Initialize(cfg =>
                {
                    
                    // 用户
                    cfg.AddProfile<BaseAutoMapperProfile<UserInfo, UserDto>>(); 
                    // 客户端
                    cfg.AddProfile<BaseAutoMapperProfile<Client, ClientDto>>();
                  
                    /*  // 管理员
                    cfg.AddProfile<BaseAutoMapperProfile<Admin, AdminDto>>();
                    // 供应商
                    cfg.AddProfile<BaseAutoMapperProfile<Supplier,SupplierDto>>();
                    // 购物车
                    cfg.AddProfile<ShopCartDtoMapper>();
                    // 订单
                    cfg.AddProfile<OrderDtoMapper>();
                    // 订单详细
                    cfg.AddProfile<OrderMoreDtoMapper>();
                    // 订单项
                    cfg.AddProfile<OrderItemDtoMapper>();
                    // 商品
                    cfg.AddProfile<ProductDtoMapper>();*/
                }); 

        }
    }
}