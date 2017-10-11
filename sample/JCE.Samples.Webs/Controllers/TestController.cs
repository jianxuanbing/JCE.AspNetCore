using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JCE.Logs;
using JCE.Logs.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace JCE.Samples.Webs.Controllers
{
    [Route("api/[controller]")]
    public class TestController:Controller
    {
        /// <summary>
        /// 日志操作
        /// </summary>
        public ILog Log { get; set; }

        public TestController(ILog log)
        {
            Log = log;
        }

        [HttpPost("[action]")]
        public void SendInfo()
        {
            Log.BussinessId(Guid.NewGuid().ToString())
                .Module("订单")
                .Method("PlaceOrder")
                .Caption("有人下单了")
                .Params("int", "a", "1")
                .Params("string", "b", "c")
                .Content("购买商品数量：{0}", 100)
                .Content("购买商品总额：{0}", 200)
                .Sql("select * from Users")
                .Sql("select * from Orders")
                .SqlParams("@a={0},@b={1}", 1, 2)
                .SqlParams("@userId={0}", Guid.NewGuid().ToString())
                .Info();
        }
    }
}
