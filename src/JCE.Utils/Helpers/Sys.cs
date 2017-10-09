using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JCE.Utils.Extensions;

namespace JCE.Utils.Helpers
{
    /// <summary>
    /// 系统操作
    /// </summary>
    public static class Sys
    {
        #region GetPhysicalPath(获取物理路径)
        /// <summary>
        /// 获取物理路径
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <returns></returns>
        public static string GetPhysicalPath(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
            {
                return string.Empty;
            }
            if (Web.HttpContext == null)
            {
                if (relativePath.StartsWith("~"))
                {
                    relativePath = relativePath.Remove(0, 2);
                }
                return Path.GetFullPath(relativePath);
            }
            if (relativePath.StartsWith("~"))
            {
                return Web.HostingEnvironment.ContentRootPath.MapPath(relativePath);
            }
            if (relativePath.StartsWith("/") || relativePath.StartsWith("\\"))
            {
                return Web.HostingEnvironment.ContentRootPath.MapPath("~" + relativePath);
            }
            return Web.HostingEnvironment.ContentRootPath.MapPath("~/" + relativePath);
        }
        #endregion
    }
}
