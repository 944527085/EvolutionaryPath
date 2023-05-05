using HotUpdateScripts.GameScript.Event;
using JEngine.Core;
using System.Collections.Generic;
using UnityEngine;

namespace HotUpdateScripts.GameScript.Manager
{
    class EventManager : Singleton<EventManager>
    {
        /// <summary>
        /// 消息对象字典
        /// </summary>
        private Dictionary<EventCode, List<IEventObject>> EventObjectDic;
        public override void Init()
        {
            EventObjectDic = new Dictionary<EventCode, List<IEventObject>>();
        }
        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="eventCode">消息码</param>
        /// <param name="object">消息参数</param>
        public void FireEvent(EventCode eventCode, object @object = null)
        {
            if (EventObjectDic.ContainsKey(eventCode) == false)
            {
                return;
            }
            List<IEventObject> EventObjectS = EventObjectDic[eventCode];
            if (EventObjectS == null)
            {
                return;
            }
            for (int i = 0; i < EventObjectS.Count; i++)
            {
                EventObjectS[i].ReceiveFireOn(@object);
            }
        }
        /// <summary>
        /// 注册一个事件对象
        /// </summary>
        /// <param name="eventCode">事件码</param>
        /// <param name="eventObject">事件对象</param>
        public void Register(EventCode eventCode, IEventObject eventObject)
        {
            if (EventObjectDic.ContainsKey(eventCode) == false)
            {
                EventObjectDic.Add(eventCode, new List<IEventObject>());
            }
            if (EventObjectDic[eventCode].Contains(eventObject))
            {
                Debug.LogError("事件对象已存在不需要重复注册" + "事件码:" + eventCode.ToString());
                return;
            }
            EventObjectDic[eventCode].Add(eventObject);
        }
        /// <summary>
        /// 注销一个事件对象
        /// </summary>
        /// <param name="eventCode">事件码</param>
        /// <param name="eventObject">事件对象</param>
        public void UnRegister(EventCode eventCode, IEventObject eventObject)
        {
            if (EventObjectDic.ContainsKey(eventCode) == false)
            {
                return;
            }
            if (EventObjectDic[eventCode].Contains(eventObject))
            {
                EventObjectDic[eventCode].Remove(eventObject);
            }
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
