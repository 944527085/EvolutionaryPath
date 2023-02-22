using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotUpdateScripts.GameScript.FMS
{
    interface FMS_Entity
    {
        void StandOn();
        void RunOn(Vector2Binder vector2);
    }
}
