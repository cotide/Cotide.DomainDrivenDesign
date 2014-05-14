using System.Collections.Generic;
using Cotide.Framework.Attr.Desc;

namespace Cotide.Domain
{
    /// <summary>
    /// 礼品类型
    /// </summary>
    [EntityDesc("礼品")]
    public class GitType : Entity.Base.EntityWidthGuidType
    {
        /// <summary>
        /// 类型名
        /// </summary>
        [EntityPropertyDesc("类型名")]
        public string TypeName { get; set; }
    }
}
