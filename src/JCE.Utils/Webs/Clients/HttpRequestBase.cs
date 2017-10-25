using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using JCE.Utils.Extensions;
using JCE.Utils.Json;

namespace JCE.Utils.Webs.Clients
{
    /// <summary>
    /// Http请求基类
    /// </summary>
    /// <typeparam name="TRequest">Http请求基类</typeparam>
    public abstract class HttpRequestBase<TRequest> where TRequest:IRequest<TRequest>
    {
        /// <summary>
        /// 请求地址
        /// </summary>
        private readonly string _url;

        /// <summary>
        /// Http请求方法
        /// </summary>
        private readonly HttpMethod _httpMethod;

        /// <summary>
        /// 参数集合
        /// </summary>
        private readonly Dictionary<string, string> _params;

        /// <summary>
        /// Json参数
        /// </summary>
        private string _json;

        /// <summary>
        /// 内容类型
        /// </summary>
        private string _contentType;

        /// <summary>
        /// Cookie容器
        /// </summary>
        private readonly CookieContainer _cookieContainer;

        /// <summary>
        /// 超时时间
        /// </summary>
        private TimeSpan _timeSpan;

        /// <summary>
        /// 请求头集合
        /// </summary>
        private readonly Dictionary<string, string> _headers;

        /// <summary>
        /// 执行失败的回调函数
        /// </summary>
        private Action<string> _failAction;

        /// <summary>
        /// 执行失败的回调函数
        /// </summary>
        private Action<string, HttpStatusCode> _failStatusCodeAction;

        /// <summary>
        /// 初始化一个<see cref="HttpRequestBase{TRequest}"/>类型的实例
        /// </summary>
        /// <param name="httpMethod">Http请求方法</param>
        /// <param name="url">请求地址</param>
        protected HttpRequestBase(HttpMethod httpMethod, string url)
        {
            url.CheckNotNullOrEmpty(nameof(url));

            _url = url;
            _httpMethod = httpMethod;
            _params = new Dictionary<string, string>();
            _contentType = HttpContentType.FormUrlEncoded.Description();
            _cookieContainer = new CookieContainer();
            _timeSpan = new TimeSpan(0, 0, 30);
            _headers = new Dictionary<string, string>();
        }

        /// <summary>
        /// 设置内容类型
        /// </summary>
        /// <param name="contentType">内容类型</param>
        /// <returns></returns>
        public TRequest ContentType(HttpContentType contentType)
        {
            return ContentType(contentType.Description());
        }

        /// <summary>
        /// 设置内容类型
        /// </summary>
        /// <param name="contentType">内容类型</param>
        /// <returns></returns>
        public TRequest ContentType(string contentType)
        {
            _contentType = contentType;
            return This();
        }

        /// <summary>
        /// 返回自身
        /// </summary>
        /// <returns></returns>
        private TRequest This()
        {
            return (TRequest) ((object) this);
        }

        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="expiresDate">有效时间，单位：天</param>
        /// <returns></returns>
        public TRequest Cookie(string name, string value, double expiresDate)
        {
            return Cookie(name, value, null, null, DateTime.Now.AddDays(expiresDate));
        }

        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="expiresDate">到期时间</param>
        /// <returns></returns>
        public TRequest Cookie(string name, string value, DateTime expiresDate)
        {
            return Cookie(name, value, null, null, expiresDate);
        }

        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="path">源服务器URL子集</param>
        /// <param name="domain">所属域</param>
        /// <param name="expiresDate">到期时间</param>
        /// <returns></returns>
        public TRequest Cookie(string name, string value, string path = "/", string domain = null,
            DateTime? expiresDate = null)
        {
            return Cookie(new Cookie(name, value, path, domain) {Expires = expiresDate ?? DateTime.Now.AddYears(1)});
        }

        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="cookie">cookie</param>
        /// <returns></returns>
        public TRequest Cookie(Cookie cookie)
        {
            _cookieContainer.Add(new Uri(_url),cookie);
            return This();
        }

        /// <summary>
        /// 设置超时时间
        /// </summary>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        public TRequest Timeout(int timeout)
        {
            _timeSpan=new TimeSpan(0,0,timeout);
            return This();
        }

        /// <summary>
        /// 设置请求头
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public TRequest Header<T>(string key, T value)
        {
            _headers.Add(key,value.SafeString());
            return This();
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public TRequest Data<T>(string key, T value)
        {
            key.CheckNotNullOrEmpty(nameof(key));

            var data = value.SafeString();
            if (string.IsNullOrWhiteSpace(data))
            {
                return This();
            }
            _params.Add(key,data);
            return This();
        }

        /// <summary>
        /// 添加Json参数
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="value">值</param>
        /// <returns></returns>
        public TRequest JsonData<T>(T value)
        {
            ContentType(HttpContentType.Json);
            _json = JsonUtil.ToJson(value);
            return This();
        }

        /// <summary>
        /// 请求失败回调函数
        /// </summary>
        /// <param name="action">执行失败的回调函数，参数为响应结果</param>
        /// <returns></returns>
        public TRequest OnFail(Action<string> action)
        {
            _failAction = action;
            return This();
        }

        /// <summary>
        /// 请求失败回调函数
        /// </summary>
        /// <param name="action">执行失败的回调函数，第一参数为响应结果，第二个参数为状态码</param>
        /// <returns></returns>
        public TRequest OnFail(Action<string, HttpStatusCode> action)
        {
            _failStatusCodeAction = action;
            return This();
        }
    }
}
