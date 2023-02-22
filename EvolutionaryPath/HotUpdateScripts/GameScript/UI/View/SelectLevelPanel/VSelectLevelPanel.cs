using HotUpdateScripts.GameScript.UI.View.CommPanel;
using HotUpdateScripts.GameScript.UI.View.SelectLevelPanel;
using System.Collections.Generic;
using UnityEngine;

namespace HotUpdateScripts.GameScript.UI.View
{
    class VSelectLevelPanel : ViewPanelBase
    {
        public override ViewModel viewModel => ViewModel.VSelectLevelPanel;
        private ScrollListCtrl SelectScroll;
        public override void Init(Transform Root)
        {
            base.Init(Root);
            SelectScroll = new ScrollListCtrl();
            SelectScroll.Init(gameObject.transform.Find("SelectScroll").gameObject);
        }
        public override void Show()
        {
            base.Show();
            List<object> vSelectLevelItemDatas = new List<object>();
            for (int i = 0; i < 10; i++)
            {
                VSelectLevelItemData vSelectLevelItemData = new VSelectLevelItemData();
                vSelectLevelItemData.strName = i.ToString();
                vSelectLevelItemDatas.Add(vSelectLevelItemData);
            }
            SelectScroll.UpdateData<VSelectLevelItem>(vSelectLevelItemDatas);
        }
    }
}
