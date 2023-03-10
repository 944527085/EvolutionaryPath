using HotUpdateScripts.GameScript.Manager;
using HotUpdateScripts.GameScript.UI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace HotUpdateScripts.GameScript.UI
{
    class VMainPanel: ViewPanelBase
    {
        public override ViewModel viewModel => ViewModel.VMainPanel;
        public override void Init(Transform Root)
        {
            base.Init(Root);
            Button PlayGameBtn = gameObject.transform.Find("PlayGame").GetComponent<Button>();
            PlayGameBtn.onClick.AddListener(PlayGameOn);
        }
        void PlayGameOn()
        {
            Hide();
            UIManager.Instance.ShowView<VSelectLevelPanel>(ViewModel.VSelectLevelPanel);
        }
    }
}
