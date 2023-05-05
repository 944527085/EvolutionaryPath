using BM;
using HotUpdateScripts.GameScript.UI;
using JEngine.Core;
using System.Collections.Generic;
using UnityEngine;

namespace HotUpdateScripts.GameScript.Manager
{
    class UIManager : Singleton<UIManager>
    {
        private Dictionary<ViewModel, string> ViewPrefabDic;
        private Dictionary<ViewModel, ViewBase> ViewDic;
        public Transform UIRoot { get; private set; }
        private Camera UICamera;
        public Camera GetUICamera
        {
            get
            {
                if (UICamera == null)
                {
                    UICamera = UIRoot.transform.Find("UICamera").GetComponent<Camera>();
                }
                return UICamera;
            }
        }

        public override void Init()
        {
            InitViewPrefabDic();
            var prefab = new JPrefab("Assets/HotUpdateResources/Prefab/UI/UIRoot.prefab", false);
            UIRoot = GameObject.Instantiate(prefab.Instance).transform;
            UICamera = UIRoot.Find("UICamera").GetComponent<Camera>();
            GameObject.DontDestroyOnLoad(UIRoot.gameObject);
            ShowView<VMainPanel>(ViewModel.VMainPanel);
        }
        /// <summary>
        /// 显示一个界面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="viewModel"></param>
        public void ShowView<T>(ViewModel viewModel) where T : ViewBase, new()
        {
            ViewBase viewBase = GetView<T>(viewModel);
            if (viewBase == null)
            {
                return;
            }
            viewBase.Show();
        }

        /// <summary>
        /// 隐藏一个界面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="viewModel"></param>
        public void HideView<T>(ViewModel viewModel) where T : ViewBase, new()
        {
            ViewBase viewBase = GetView<T>(viewModel);
            if (viewBase == null)
            {
                return;
            }
            viewBase.Hide();
        }
        /// <summary>
        /// 检查一个界面的激活状态
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public bool ViewIsActive(ViewModel viewModel) 
        {
            if (viewModel == ViewModel.Null)
            {
                return false;
            }
            if (ViewDic == null)
            {
                return false;
            }
            ViewBase viewBase = null;
            if (ViewDic.TryGetValue(viewModel,out viewBase))
            {
                return viewBase.IsActive;
            }
            return false;
        }
        /// <summary>
        /// 获取一个界面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public T GetView<T>(ViewModel viewModel) where T : ViewBase, new()
        {
            if (viewModel == ViewModel.Null)
            {
                return null;
            }
            if (ViewDic == null)
            {
                ViewDic = new Dictionary<ViewModel, ViewBase>();
            }
            if (!ViewDic.ContainsKey(viewModel))
            {
                if (ViewPrefabDic == null)
                {
                    Debug.LogError("还未初始化ViewPrefabDic");
                    InitViewPrefabDic();
                }
                if (!ViewPrefabDic.ContainsKey(viewModel))
                {
                    Debug.LogError("请在InitViewPrefabDic添加"+ viewModel+"对应资源的路径");
                    return null;
                }

                ViewBase viewBase = new T();
                viewBase.Init(Instance.UIRoot);
                ViewDic[viewModel] = viewBase;
            }
            return ViewDic[viewModel] as T;
        }
        /// <summary>
        /// 获取一个界面的资源路径
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public string GetJPrefabPath(ViewModel viewModel)
        {
            return ViewPrefabDic[viewModel];
        }
        /// <summary>
        /// 初始化资源路径配置
        /// </summary>
        private void InitViewPrefabDic()
        {
            ViewPrefabDic = new Dictionary<ViewModel, string>();
            ViewPrefabDic.Add(ViewModel.VMainPanel, BPath.Assets_HotUpdateResources_Prefab_UI_VMainPanel_VMainPanel__prefab);
            ViewPrefabDic.Add(ViewModel.VSelectLevelPanel, BPath.Assets_HotUpdateResources_Prefab_UI_VSelectLevelPanel_VSelectLevelPanel__prefab);
            ViewPrefabDic.Add(ViewModel.VSelectLevelItem, BPath.Assets_HotUpdateResources_Prefab_UI_VSelectLevelPanel_VSelectLevelItem__prefab);
        }
    }
}
