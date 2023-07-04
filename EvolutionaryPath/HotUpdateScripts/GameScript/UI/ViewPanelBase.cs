using HotUpdateScripts.GameScript.Manager;
using System;
using UnityEngine;

namespace HotUpdateScripts.GameScript.UI
{
    abstract class ViewPanelBase : ViewBase
    {
        public virtual string LayerName => UISortingLayerEm.Default;
        private Canvas canvas;
        public override void Init()
        {
            canvas = gameObject.GetComponent<Canvas>();
            canvas.overrideSorting = true;
            canvas.sortingLayerName = LayerName;
        }
        public int GetSortingLayer()
        {
            if (canvas == null)
            {
                return 0;
            }
            return canvas.sortingOrder;
        }
        public void SetSortingLayer(int layer)
        {
            if (canvas == null)
            {
                return;
            }
            canvas.sortingOrder = layer;
        }
    }
}
