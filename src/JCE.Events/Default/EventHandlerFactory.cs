

using System.Collections.Generic;
using JCE.Events.Handlers;
using JCE.Helpers;

namespace JCE.Events.Default
{
    /// <summary>
    /// 事件处理器工厂
    /// </summary>
    public class EventHandlerFactory:IEventHandlerFactory
    {
        /// <summary>
        /// 获取事件处理器列表
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <returns></returns>
        public List<IEventHandler<TEvent>> GetHandlers<TEvent>() where TEvent : IEvent
        {
            return Ioc.CreateList<IEventHandler<TEvent>>();
        }
    }
}