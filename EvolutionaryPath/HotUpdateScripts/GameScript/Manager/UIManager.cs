using BM;
using HotUpdateScripts.GameScript.UI;
using JEngine.Core;
using System.Collections.Generic;
using UnityEngine;

namespace HotUpdateScripts.GameScript.Manager
{
    class UIManager : Singleton<UIManager>
    {
        /// <summary>
        /// 资源配置表
        /// </summary>
        private Dictionary<ViewModel, string> ViewPrefabDic;
        /// <summary>
        /// UIWindow索引表
        /// </summary>
        private Dictionary<ViewModel, ViewPanelBase> UIViewDic;
        /// <summary>
        /// UIWindow排序表
        /// </summary>
        private Dictionary<string, int> UIViewSoringDic;
        public GameObject UIRoot { get; private set; }
        public Canvas UIRootCanvas { get; private set; }
        public Camera UICamera { get; private set; }

        public override void Init()
        {
            InitViewPrefabDic();
            InitUIViewSoringDic();
            InitUIViewDic();
            UIRoot =  PoolManager.Instance.GetPoolObject<GameObject>(BPath.Assets_HotUpdateResources_Prefab_UI_UIRoot__prefab);
            UIRootCanvas = UIRoot.transform.Find("UIRootCanvas").GetComponent<Canvas>();
            UICamera = UIRoot.transform.Find("UICamera").GetComponent<Camera>();
            GameObject.DontDestroyOnLoad(UIRoot);
            ShowView<VMainPanel>(ViewModel.VMainPanel);
        }
        /// <summary>
        /// 显示一个界面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="viewModel"></param>
        public void ShowView<T>(ViewModel viewModel) where T : ViewPanelBase, new()
        {
            ViewPanelBase viewBase = GetView<T>(viewModel);
            if (viewBase == null)
            {
                return;
            }
            int nLayer = -1;
            if (UIViewSoringDic.TryGetValue(viewBase.LayerName,out nLayer))
            {
                nLayer = nLayer + 100;
                viewBase.SetSortingLayer(nLayer);
                UIViewSoringDic[viewBase.LayerName] = nLayer;
            }
            viewBase.Show();
        }

        /// <summary>
        /// 隐藏一个界面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="viewModel"></param>
        public void HideView<T>(ViewModel viewModel) where T : ViewPanelBase, new()
        {
            ViewPanelBase viewBase = GetView<T>(viewModel);
            if (viewBase == null)
            {
                return;
            }
            int nLayer = -1;
            if (UIViewSoringDic.TryGetValue(viewBase.LayerName, out nLayer))
            {
                if (viewBase.GetSortingLayer()== nLayer)
                {
                    nLayer = nLayer - 100;
                }
                UIViewSoringDic[viewBase.LayerName] = nLayer;
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
            if (UIViewDic == null)
            {
                return false;
            }
            ViewPanelBase viewBase = null;
            if (UIViewDic.TryGetValue(viewModel,out viewBase))
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
        public T GetView<T>(ViewModel viewModel) where T : ViewPanelBase, new()
        {
            if (viewModel == ViewModel.Null)
            {
                return null;
            }
            if (UIViewDic == null)
            {
                UIViewDic = new Dictionary<ViewModel, ViewPanelBase>();
            }
            if (!UIViewDic.ContainsKey(viewModel))
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

                ViewPanelBase viewBase = new T();
                viewBase.Init(Instance.UIRootCanvas.transform);
                UIViewDic[viewModel] = viewBase;
            }
            return UIViewDic[viewModel] as T;
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
        private void InitUIViewDic()
        {
            UIViewDic = new Dictionary<ViewModel, ViewPanelBase>();
        }
        private void InitUIViewSoringDic()
        {
            UIViewSoringDic = new Dictionary<string, int>();
            UIViewSoringDic.Add(UISortingLayerEm.Default, 0);
            UIViewSoringDic.Add(UISortingLayerEm.Bottom, 0);
            UIViewSoringDic.Add(UISortingLayerEm.Centre, 0);
            UIViewSoringDic.Add(UISortingLayerEm.Top, 0);
        }
    }
}
