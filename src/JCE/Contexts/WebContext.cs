using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace JCE.Contexts
{
    /// <summary>
    /// Web上下文
    /// </summary>
    public class WebContext : IContext
    {
        /// <summary>
        /// 跟踪号
        /// </summary>
        public string TraceId => HttpContextAccessor?.HttpContext?.TraceIdentifier;

        /// <summary>
        /// Http上下文访问器
        /// </summary>
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        /// <summary>
        /// 初始化一个<see cref="WebContext"/>类型的实例
        /// </summary>
        /// <param name="httpContextAccessor">Http上下文访问器</param>
        public WebContext(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">键名</param>
        /// <param name="value">对象</param>
        public void Add<T>(string key, T value)
        {
            if (HttpContextAccessor?.HttpContext == null)
            {
                return;
            }
            HttpContextAccessor.HttpContext.Items[key] = value;
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">键名</param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            if (HttpContextAccessor?.HttpContext == null)
            {
                return default(T);
            }
            return JCE.Utils.Helpers.Conv.To<T>(HttpContextAccessor.HttpContext.Items[key]);
        }

        /// <summary>
        /// 移除对象
        /// </summary>
        /// <param name="key">键名</param>
        public void Remove(string key)
        {
            HttpContextAccessor?.HttpContext?.Items.Remove(key);
        }
    }
}
