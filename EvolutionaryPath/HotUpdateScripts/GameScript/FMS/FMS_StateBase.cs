using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotUpdateScripts.GameScript.FMS
{
    abstract class FMS_StateBase
    {
        protected FMS_Entity entity;
        public abstract void EnterOn();
        public abstract void UpdateOn();
        public abstract void ExitOn();
    }
}
