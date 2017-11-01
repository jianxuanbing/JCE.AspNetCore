using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace JCE.Webs.Middlewares.Internals
{
    /// <summary>
    /// Json Null 值替换为空字符串 提供程序
    /// </summary>
    public class NullToEmptyStringValueProvider:IValueProvider
    {
        /// <summary>
        /// 属性信息
        /// </summary>
        private PropertyInfo _propertyInfo;

        /// <summary>
        /// 初始化一个<see cref="NullToEmptyStringValueProvider"/>类型的实例
        /// </summary>
        /// <param name="propertyInfo">属性信息</param>
        public NullToEmptyStringValueProvider(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;
        }

        public void SetValue(object target, object value)
        {
            _propertyInfo.SetValue(target,value);
        }

        public object GetValue(object target)
        {
            object result = _propertyInfo.GetValue(target) ?? "";
            return result;
        }
    }
}
