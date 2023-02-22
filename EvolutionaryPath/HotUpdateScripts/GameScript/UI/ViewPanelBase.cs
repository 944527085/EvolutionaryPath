using HotUpdateScripts.GameScript.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotUpdateScripts.GameScript.UI
{
   abstract  class ViewPanelBase: ViewBase
    {
        private Canvas canvas;
        public override void Init(Transform Root)
        {
            base.Init(Root);
            canvas = gameObject.GetComponent<Canvas>();
            if (canvas!=null)
            {
                canvas.worldCamera = UIManager.Instance.GetUICamera;
            }
        }
    }
}
