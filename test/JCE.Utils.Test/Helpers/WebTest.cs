using System;
using System.Collections.Generic;
using System.Text;
using JCE.Utils.Helpers;
using JCE.Utils.Webs.Clients;
using Xunit;
using Xunit.Abstractions;

namespace JCE.Utils.Test.Helpers
{
    
    public class WebTest:TestBase
    {
        public WebTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void UploadTest()
        {
            var result = Web.Client()
                .Post("http://localhost:28774/api/Common/UploadImg")
                .ContentType(HttpContentType.FormData)
                .Header("Authorization",
                    "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyaWQiOiI0OTM4ZGMwMy1kZTYzLTQ2MTYtYjc3Yy01MmE3Zjk4Y2M1ZjIiLCJ1c2VybmFtZSI6Inp5bCIsInNjb3BlIjoiYWRtaW4iLCJsb2dpbiI6IntcIk1lcmNoYW50SWRcIjpcIjg4ODg4ODg4LTg4ODgtODg4OC04ODg4LTg4ODg4ODg4ODg4OFwiLFwiTG9naW5cIjpcInp5bFwiLFwiTmFtZVwiOlwi57O757uf566h55CG5ZGYXCIsXCJSb2xlVHlwZVwiOjEsXCJVc2VySWRcIjpcIjQ5MzhkYzAzLWRlNjMtNDYxNi1iNzdjLTUyYTdmOThjYzVmMlwifSIsImlzcyI6Imx4enlsIiwiYXVkIjoiQW55IiwiZXhwIjoxNTA5MDUwNzIxLCJuYmYiOjE1MDkwMjkxMjF9.RNIwQz66rgDOusuk9-q2v-UnN3p8F5dYupAKzobBQC8")
                .FileData("file", @"C:\Users\jianx\Pictures\逗逼卡.jpg")
                .Result();
            Output.WriteLine(result);
        }
    }
}
