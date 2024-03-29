﻿using JEngine.Core;
using UnityEngine;

namespace HotUpdateScripts.GameScript.Manager
{
    /// <summary>
    /// 游戏主管理器
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        public override void Init()
        {
            PoolManager.Instance.Init();
            UpdateManager.Instance.Init();
            UIManager.Instance.Init();
            EventManager.Instance.Init();
            InputManager.Instance.Init();
        }
    }
}
