using JEngine.Core;
using UnityEngine;
namespace HotUpdateScripts.GameScript.Manager
{
    class InputManager : Singleton<InputManager>
    {

        public override void Init()
        {

        }
        /// <summary>
        /// 获取鼠标位置
        /// </summary>
        /// <returns></returns>
        public Vector3 GetMousePosition()
        {
            return Input.mousePosition;
        }

    }
}
