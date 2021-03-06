﻿using System;
using System.Text;
using JCE.Utils.Timing;

namespace JCE.Events
{
    /// <summary>
    /// 事件
    /// </summary>
    public class Event:IEvent
    {
        /// <summary>
        /// 事件标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 事件时间
        /// </summary>
        public DateTime Time { get; }

        /// <summary>
        /// 初始化一个<see cref="Event"/>类型的实例
        /// </summary>
        public Event()
        {
            Id = Guid.NewGuid().ToString();
            Time=DateTime.Now;
        }

        /// <summary>
        /// 输出日志
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder result=new StringBuilder();
            result.AppendLine($"事件标识: {Id}");
            result.AppendLine($"事件时间: {Time.ToMillisecondString()}");
            result.AppendLine($"事件数据: {JCE.Utils.Json.JsonUtil.ToJson(this)}");
            return result.ToString();
        }
    }
}
