using System.Collections.Generic;
using Cotide.Framework.Attr.Desc;

namespace Cotide.Domain.Entity
{
    /// <summary>
    /// 礼品
    /// </summary>
    [EntityDesc("礼品")]
    public class Gift : Entity.Base.EntityWidthGuidType
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Gift()
        {
            UserGift = new List<UserGift>();
            
        }

        /// <summary>
        /// 礼品类型
        /// </summary>
        [EntityPropertyDesc("礼品类型")]
        public GitType GitType { get; set; }

        /// <summary>
        /// 礼品图片(原图)
        /// </summary>
        [EntityPropertyDesc("礼品图片(原图)")]
        public string GiftImg { get; set; }

        /// <summary>
        /// 礼品图片(小图) 50 X 50
        /// </summary>
        [EntityPropertyDesc("礼品图片(小图) 50 X 50")]
        public string SmallGiftImgImg { get; set; }

        /// <summary>
        /// 礼品图片(标准图) 150 X 150
        /// </summary>
        [EntityPropertyDesc("礼品图片(标准图) 150 X 150")]
        public string StandardGiftImgImg { get; set; }

        /// <summary>
        /// 需要积分值
        /// </summary>
        [EntityPropertyDesc("需要积分值")]
        public int NeedIntegrationValue { get; set; }

        /// <summary>
        /// 礼品数量
        /// </summary>
        [EntityPropertyDesc("礼品数量")]
        public int Count { get; set; }

        /// <summary>
        /// 创建管理员
        /// </summary>
        public Admin CreateUser { get; set; }

        /// <summary>
        /// 用户礼品
        /// </summary>
        public IList<UserGift> UserGift { get; set; } 
    }
}
