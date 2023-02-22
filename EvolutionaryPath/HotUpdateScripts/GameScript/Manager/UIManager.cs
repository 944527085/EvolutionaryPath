using HotUpdateScripts.GameScript.UI;
using JEngine.Core;
using System.Collections.Generic;
using UnityEngine;

namespace HotUpdateScripts.GameScript.Manager
{
    class UIManager : Singleton<UIManager>
    {
        public readonly static int ShowViewLayer = LayerMask.NameToLayer("UI");
        public readonly static int HideViewLayer = LayerMask.NameToLayer("HideView");
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
      
        public void ShowView<T>(ViewModel viewModel) where T : ViewBase, new()
        {
            ViewBase viewBase = GetView<T>(viewModel);
            if (viewBase==null)
            {
                return;
            }
            viewBase.Show();
        }
        public void HideView<T>(ViewModel viewModel) where T : ViewBase, new()
        {
            ViewBase viewBase = GetView<T>(viewModel);
            if (viewBase == null)
            {
                return;
            }
            viewBase.Hide();
        }
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
                    return null;
                }
                if (!ViewPrefabDic.ContainsKey(viewModel))
                {
                    return null;
                }

                ViewBase viewBase = new T();
                viewBase.Init(Instance.UIRoot);
                ViewDic[viewModel] = viewBase;
            }
            return ViewDic[viewModel] as T;
        }
        public string GetJPrefabPath(ViewModel viewModel)
        {
            return ViewPrefabDic[viewModel];
        }
        private void InitViewPrefabDic()
        {
            ViewPrefabDic = new Dictionary<ViewModel, string>();
            ViewPrefabDic.Add(ViewModel.VMainPanel, "Assets/HotUpdateResources/Prefab/UI/VMainPanel/VMainPanel.prefab");
            ViewPrefabDic.Add(ViewModel.VSelectLevelPanel, "Assets/HotUpdateResources/Prefab/UI/VSelectLevelPanel/VSelectLevelPanel.prefab");
            ViewPrefabDic.Add(ViewModel.VSelectLevelItem, "Assets/HotUpdateResources/Prefab/UI/VSelectLevelPanel/VSelectLevelItem.prefab");
        }
    }
}
