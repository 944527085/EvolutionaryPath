using HotUpdateScripts.GameScript.Manager;
using JEngine.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HotUpdateScripts.GameScript.UI.View.CommPanel
{
    class ScrollListCtrl
    {
        protected ScrollRect scrollRect { get; private set; }
        protected List<object> tDataList = new List<object>();
        private List<ViewItemBase> ListItem = new List<ViewItemBase>();

        public void Init(GameObject ScrollObj)
        {
            this.scrollRect = ScrollObj.GetComponent<ScrollRect>();
            ListItem.Clear();
            tDataList.Clear();
        }
        public void UpdateData<T>(List<object> tDataList) where T : ViewItemBase, new()
        {
            this.tDataList = tDataList;
            if (ListItem.Count > tDataList.Count)
            {
                int nRemoveCount = ListItem.Count - tDataList.Count;
                for (int i = 0; i < nRemoveCount; i++)
                {
                    ViewItemBase viewItem = ListItem[0];
                    ListItem.RemoveAt(0);
                    viewItem.Destroy();
                }
            }
            for (int i = 0; i < tDataList.Count; i++)
            {
                ViewItemBase viewItem = null;
                if (ListItem.Count <= i)
                {
                    viewItem = new T();
                    Log.PrintError("准备创建viewItem");
                    viewItem.CreatOn(scrollRect.content,()=> {
                        viewItem.SetData(tDataList[i]); 
                    });
                    ListItem.Add(viewItem);
                }
                else
                {
                    viewItem = ListItem[i];
                    viewItem.SetData(tDataList[i]);
                }
                
            }
        }
    }
}
