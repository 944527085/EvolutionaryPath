using JEngine.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HotUpdateScripts.GameScript.Manager
{
    class TimerManager : Singleton<TimerManager>
    {
        /// <summary>
        /// 当前激活的触发型计时器计时器列表
        /// </summary>
        private List<ITimer> TriggerTimers;
        /// <summary>
        /// 当前激活的更新型计时器列表
        /// </summary>
        private List<ITimer> UpdateTimers;
        /// <summary>
        /// 当前失活的计时器列表
        /// </summary>
        private Queue<ITimer> TimerPoll;
        public override void Init()
        {
            TriggerTimers = new List<ITimer>();
            UpdateTimers = new List<ITimer>();
            TimerPoll = new Queue<ITimer>();
            UpdateManager.Instance.AddUpdate(UpdateType.Update, Update);
        }
        /// <summary>
        /// 获取到一个计时器对象
        /// </summary>
        /// <returns>计时器实例</returns>
        private ITimer GetTimer()
        {
            if (TimerPoll.Count > 0)
            {
                return TimerPoll.Dequeue();
            }
            else
            {
                return new Timer(SetTimer);
            }
        }
        /// <summary>
        /// 回收计时器
        /// </summary>
        /// <param name="timer"></param>
        private void SetTimer(ITimer timer)
        {
            TriggerTimers.Remove(timer);
            TimerPoll.Enqueue(timer);
        }
        public ITimer CreatTimer(float _time, Action EndAction = null, Action<float> UpdateAction = null)
        {
            ITimer timer = GetTimer();
            timer.Start(_time, EndAction, UpdateAction);
            if (EndAction != null && UpdateAction == null)
            {
                int ID = TriggerTimers.Count;//默认在末尾
                for (int i = 0; i < TriggerTimers.Count; i++)
                {
                    if (_time < TriggerTimers[i].ResidueTime())//获得插入的最优位置
                    {
                        ID = i;
                        break;
                    }
                }
                TriggerTimers.Insert(ID, timer);
            }
            else if (EndAction != null && UpdateAction != null)
            {
                UpdateTimers.Add(timer);
            }
            return timer;
        }
        public void Update()
        {
            if (TriggerTimers.Count > 0)
            {
                if (TriggerTimers[0].ResidueTime() <= 0)//只需要判断计时器队列中的第一个就行。
                {
                    TriggerTimers[0].EndTrigger();
                }
            }
            for (int i = 0; i < UpdateTimers.Count; i++)
            {
                if (TriggerTimers[i].ResidueTime() > 0)
                    TriggerTimers[i].UpdateOn();
            }
        }
    }
    public class Timer : ITimer
    {
        private float StartTime;
        private float TargetTime;
        private Action EndAction;
        private Action<float> UpdateAction;
        private Action<ITimer> PollSet;
        public Timer(Action<ITimer> timer)
        {
            PollSet = timer;
        }

        public float GetRoatValue()
        {
            return (TargetTime - Time.time) / StartTime;
        }

        public float ResidueTime()
        {
            return TargetTime - Time.time;
        }

        public void Start(float _timerV, Action EndAction = null, Action<float> UpdateAction = null)
        {
            StartTime = _timerV;
            TargetTime = Time.time + _timerV;
            this.EndAction = EndAction;
            this.UpdateAction = UpdateAction;
        }

        public void EndTrigger()
        {
            EndAction?.Invoke();
            EndAction = null;
            PollSet(this);
        }

        public void UpdateOn()
        {
            UpdateAction?.Invoke(GetRoatValue());
        }
    }
    public interface ITimer
    {
        /// <summary>
        /// 初始化计时器
        /// </summary>
        /// <param name="_timerV">本次计时时间</param>
        /// <param name="action">触发事件</param>
        void Start(float _time, Action EndAction = null, Action<float> UpdateAction = null);
        /// <summary>
        /// 获取剩余时间
        /// </summary>
        /// <returns></returns>
        float ResidueTime();
        /// <summary>
        /// 剩余时间百分比
        /// </summary>
        /// <returns></returns>
        float GetRoatValue();
        /// <summary>
        /// 触发事件
        /// </summary>
        void EndTrigger();

        void UpdateOn();

    }
}
