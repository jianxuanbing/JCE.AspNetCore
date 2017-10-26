using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Extensions;
using JCE.Utils.Helpers;
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

        /// <summary>
        /// 获取结果
        /// </summary>
        /// <returns></returns>
        public string Result()
        {
            return Async.Run(async () =>
            {
                SendBefore();
                var response = await SendAsync();
                var result = await response.Content.ReadAsStringAsync();
                SendAfter(result,response.StatusCode,GetContentType(response));
                return result;
            });
        }

        /// <summary>
        /// 获取内容类型
        /// </summary>
        /// <param name="response">响应消息</param>
        /// <returns></returns>
        private string GetContentType(HttpResponseMessage response)
        {
            return response?.Content?.Headers?.ContentType == null
                ? string.Empty
                : response.Content.Headers.ContentType.MediaType;
        }

        /// <summary>
        /// 发送前操作
        /// </summary>
        protected virtual void SendBefore()
        {
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <returns></returns>
        private async Task<HttpResponseMessage> SendAsync()
        {
            HttpClient client = CreateHttpClient();
            return await client.SendAsync(CreateRequestMessage());
        }

        /// <summary>
        /// 发送后操作
        /// </summary>
        /// <param name="result">响应结果</param>
        /// <param name="statusCode">状态码</param>
        /// <param name="contentType">内容类型</param>
        protected virtual void SendAfter(string result,HttpStatusCode statusCode,string contentType)
        {
            if (IsSuccess(statusCode))
            {
                SuccessHandler(result,statusCode,contentType);
                return;
            }
            FailHandler(result,statusCode,contentType);
        }

        /// <summary>
        /// 发送请求是否成功
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        protected virtual bool IsSuccess(HttpStatusCode statusCode)
        {
            return statusCode.Value() < 400;
        }

        /// <summary>
        /// 成功处理操作
        /// </summary>
        /// <param name="result">响应结果</param>
        /// <param name="statusCode">状态码</param>
        /// <param name="contentType">内容类型</param>
        protected virtual void SuccessHandler(string result, HttpStatusCode statusCode, string contentType)
        {            
        }

        protected virtual void FailHandler(string result, HttpStatusCode statusCode, string contentType)
        {
            _failAction?.Invoke(result);
            _failStatusCodeAction?.Invoke(result,statusCode);
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        /// <returns></returns>
        public async Task<string> ResultAsync()
        {
            SendBefore();
            var response = await SendAsync();
            return await response.Content.ReadAsStringAsync().ContinueWith((task, state) =>
            {
                var result = task.Result;
                SendAfter(result, response.StatusCode, state.SafeString());
                return result;
            }, GetContentType(response));
        }

        /// <summary>
        /// 创建请求客户端
        /// </summary>
        /// <returns></returns>
        private HttpClient CreateHttpClient()
        {
            return new HttpClient(new HttpClientHandler(){CookieContainer = _cookieContainer}){Timeout = _timeSpan};
        }

        /// <summary>
        /// 创建请求消息
        /// </summary>
        /// <returns></returns>
        private HttpRequestMessage CreateRequestMessage()
        {
            var message=new HttpRequestMessage()
            {
                Method = _httpMethod,
                RequestUri = new Uri(_url),
                Content = CreateContent()
            };
            foreach (var header in _headers)
            {
                message.Headers.Add(header.Key,header.Value);
            }
            return message;
        }

        /// <summary>
        /// 创建请求内容
        /// </summary>
        /// <returns></returns>
        private HttpContent CreateContent()
        {
            var contentType = _contentType.SafeString().ToLower();
            switch (contentType)
            {
                case "application/x-www-form-urlencoded":
                    return new FormUrlEncodedContent(_params);
                case "application/json":
                    return CreateJsonContent();
            }
            throw new NotImplementedException("未实现该ContentType");
        }

        /// <summary>
        /// 创建json内容类型
        /// </summary>
        /// <returns></returns>
        private HttpContent CreateJsonContent()
        {
            if (string.IsNullOrWhiteSpace(_json))
            {
                _json = JsonUtil.ToJson(_params);
            }
            return new StringContent(_json, Encoding.UTF8, "application/json");
        }
    }
}
