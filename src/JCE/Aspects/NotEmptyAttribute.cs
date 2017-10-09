using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AspectCore.DynamicProxy.Parameters;
using JCE.Aspects.Base;
using JCE.Utils.Extensions;

namespace JCE.Aspects
{
    /// <summary>
    /// 验证不能为空
    /// </summary>
    public class NotEmptyAttribute:ParameterInterceptorBase
    {
        /// <summary>
        /// 执行
        /// </summary>
        public override Task Invoke(ParameterAspectContext context, ParameterAspectDelegate next)
        {
            if (string.IsNullOrWhiteSpace(context.Parameter.Value.SafeString()))
            {
                throw new ArgumentNullException(context.Parameter.Name);
            }
            return next(context);
        }
    }
}
