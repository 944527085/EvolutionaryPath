using HotUpdateScripts.GameScript.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotUpdateScripts.GameScript.UI
{
   abstract class ViewItemBase : ViewBase
    {
        public abstract void SetData(object tData);
        public virtual void SelectOn(bool IsOn)
        {

        }
    }
}
