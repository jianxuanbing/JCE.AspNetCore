using System;
using System.Collections.Generic;
using System.Text;

namespace JCE.Contexts
{
    /// <summary>
    /// 空用户上下文
    /// </summary>
    public class NullUserContext:IUserContext
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId => string.Empty;

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName => string.Empty;
    }
}
