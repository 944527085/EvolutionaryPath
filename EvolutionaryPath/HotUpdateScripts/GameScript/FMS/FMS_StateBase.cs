using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotUpdateScripts.GameScript.FMS
{
    abstract class FMS_StateBase
    {
        protected FMS_Entity entity { get; private set; }
        public FMS_StateBase(FMS_Entity fMS_Entity)
        {
            this.entity = fMS_Entity;
        }
        public abstract void EnterOn();
        public abstract void UpdateOn();
        public abstract void ExitOn();
    }
}
