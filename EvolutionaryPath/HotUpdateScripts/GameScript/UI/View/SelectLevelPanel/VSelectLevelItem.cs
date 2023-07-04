using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace HotUpdateScripts.GameScript.UI.View.SelectLevelPanel
{
    class VSelectLevelItem: ViewItemBase
    {
        public VSelectLevelItemData tData { get; private set; }

        public override ViewModel viewModel => ViewModel.VSelectLevelItem;

        private TextMeshProUGUI Name;
        public override void Init()
        {
            base.Init();
            Name = gameObject.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        }
        public override void SetData(object tData)
        {
            this.tData = (VSelectLevelItemData)tData;
            Name.text = this.tData.strName;
        }
    }
    struct VSelectLevelItemData
    {
       public string strName;
    }
}
