using JCE.Dependency;
using JCE.Logs.Aspects;
using System;
using System.Collections.Generic;
using System.Text;

namespace JCE.Samples.Services
{
    public interface ITestService: IScopeDependency
    {
        [DebugLog]
        void WriteOtherLog(string content);
    }
}
