using Cotide.Domain.Enum;
using Cotide.Framework.Attr.Desc;

namespace Cotide.Domain.Entity
{
    /// <summary>
    /// 积分
    /// </summary>
    [EntityDesc("积分")]
    public class Integration : Entity.Base.EntityWidthGuidType
    {
        /// <summary>
        /// 积分值
        /// </summary>
        [EntityPropertyDesc("积分值")]
        public long Value { get; set; }

        /// <summary>
        /// 积分描述
        /// </summary>
        [EntityPropertyDesc("积分描述")]
        public string Desc { get; set; }

        /// <summary>
        /// 积分状态 
        /// </summary>
        [EntityPropertyDesc("积分状态")]
        public IntegrationState IntegrationState { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [EntityPropertyDesc("备注")]
        public string Remark { get; set; }
    }
}
