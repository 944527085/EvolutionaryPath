using HotUpdateScripts.GameScript.Event;
using JEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotUpdateScripts.GameScript.Manager
{
    class EventManager : Singleton<EventManager>
    {
        /// <summary>
        /// 注册的消息字典
        /// </summary>
        private Dictionary<EventCode, Action<object>> EventDic;
        /// <summary>
        /// 消息对象字典
        /// </summary>
        private Dictionary<EventCode, List<IEventObject>> EventObjectDic;
        public override void Init()
        {
            EventDic = new Dictionary<EventCode, Action<object>>();
        }
        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="eventCode">消息码</param>
        /// <param name="object">消息参数</param>
        public void FireEvent(EventCode eventCode,object @object=null)
        {
            if (EventDic.ContainsKey(eventCode)==false)
            {
                return;
            }
            EventDic[eventCode]?.Invoke(@object);
        }
        public void Register(EventCode eventCode, IEventObject eventObject)
        {
            if (EventObjectDic.ContainsKey(eventCode)==false)
            {
                EventObjectDic.Add(eventCode, new List<IEventObject>());
            }
            if (EventObjectDic[eventCode].Contains(eventObject))
            {
                Debug.LogError("事件对象已存在不需要重复注册"+"事件码:"+ eventCode.ToString());
                return;
            }
            EventObjectDic[eventCode].Add(eventObject);
        }
    }
    /// <summary>
    /// 事件对象
    /// </summary>
    interface IEventObject
    {
        /// <summary>
        /// 接收事件
        /// </summary>
        /// <param name="object"></param>
        void ReceiveFireOn(object @object);
    }

}
