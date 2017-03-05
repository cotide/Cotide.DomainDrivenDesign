 

using System.Data;

namespace Cotide.Framework.EF
{
    /// <summary>
    ///  
    /// </summary>
    /// <remark>
    ///     <para>    Creator：hcli </para>
    ///     <para>CreatedTime：2014/10/9 9:14:27</para>
    /// </remark>
    public class ParameterItem
    {
        /// <summary>
        /// name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// value
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// size
        /// </summary>
        public int Size { set; get; }

        /// <summary>
        /// Direction
        /// </summary>
        public ParameterDirection Direction { get; set; }
    }
}
