using System;
using System.Collections.Generic;
using System.Text;
using JCE.Webs.Middlewares.Internals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace JCE.Webs.Middlewares
{
    /// <summary>
    /// WebApi结果处理中间件，处理空值为空字符串。
    /// 参考地址：http://www.cnblogs.com/xishuai/p/asp-net-core-webapi-json-convert-empty-string-instead-of-null.html
    /// </summary>
    public class WebApiResultMiddleware:ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.HttpContext.Request.Path.HasValue)
            {
                if (context.HttpContext.Request.Path.Value.ToLower().IndexOf(".inside.", StringComparison.Ordinal) < 0)
                {
                    if (context.Result is FileContentResult || context.Result is EmptyResult)
                    {
                        return;
                    }
                    if (context.Result is ObjectResult)
                    {
                        var objectResult = context.Result as ObjectResult;
                        var settings = new JsonSerializerSettings()
                        {
                            ContractResolver = new NullToEmptyStringResolver(),
                            DateFormatString = "yyyy-MM-dd HH:mm:ss"
                        };
                        context.Result=new JsonResult(new {data=objectResult.Value},settings);
                    }
                    else
                    {
                        context.Result = new ObjectResult(new {data = new { }});
                    }
                }
            }            
        }
    }
}
