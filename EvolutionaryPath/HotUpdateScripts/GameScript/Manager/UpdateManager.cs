
using JEngine.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HotUpdateScripts.GameScript.Manager
{
    public enum UpdateType{
        Update,
        LastUpdate,
        FiexdUpdate,
        nCout,
    }
   public class UpdateManager: MonoSingleton<UpdateManager>
    {
        private Dictionary<UpdateType, Action> UpdateList;
        private Action _UpdateAction;
        private Action _LastUpdate;
        private Action _FiexdUpdate;



        public override void Init()
        {
            DontDestroyOnLoad(gameObject);
            UpdateList = new Dictionary<UpdateType, Action>();
            UpdateList[UpdateType.Update] = _UpdateAction;
            UpdateList[UpdateType.LastUpdate] = _LastUpdate;
            UpdateList[UpdateType.FiexdUpdate] = _FiexdUpdate;
        }

        public void AddUpdate(UpdateType type, Action update)
        {
            if (UpdateList.ContainsKey(type) ==false)
            {
                return;
            }
            if (UpdateList[type] ==null)
            {
                UpdateList[type] += update;
            }
        }
        #region 生命周期
        private void Update()
        {
            UpdateList[UpdateType.Update]?.Invoke();
        }
        private void LastUpdate()
        {
            UpdateList[UpdateType.LastUpdate]?.Invoke();
        }
        private void FiexdUpdate()
        {
            UpdateList[UpdateType.FiexdUpdate]?.Invoke();
        }
        #endregion
    }
}
