using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JCE.Webs.Middlewares.Internals
{
    /// <summary>
    /// Json Null 值替换为空字符串 解析器
    /// </summary>
    public class NullToEmptyStringResolver:DefaultContractResolver
    {
        // 重写创建属性方法
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            return type.GetProperties().Select(p =>
            {
                var jp = base.CreateProperty(p, memberSerialization);
                jp.ValueProvider = new NullToEmptyStringValueProvider(p);
                return jp;
            }).ToList();
        }
    }
}
