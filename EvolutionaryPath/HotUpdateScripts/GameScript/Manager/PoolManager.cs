using HotUpdateScripts.GameScript.UI;
using JEngine.Core;
using System.Collections.Generic;
using UnityEngine;

namespace HotUpdateScripts.GameScript.Manager
{
    class PoolManager : Singleton<PoolManager>
    {
        private Dictionary<string, Queue<object>> PoolDic;
        public override void Init()
        {
            PoolDic = new Dictionary<string, Queue<object>>();
        }
        public T GetPoolObject<T>(string paht) where T: class
        {
            object @object = null;
            Queue<object> PoolQueue = default;
            string PrefabID = paht;
            if (PoolDic.TryGetValue(PrefabID, out PoolQueue) == false)
            {
                PoolQueue = new Queue<object>();
                PoolDic[PrefabID] = PoolQueue;
            }
            if (PoolQueue.Count > 0)
            {
                @object = PoolQueue.Dequeue();
            }
            else
            {
                var _JPrefab = new JPrefab("paht", false);
                @object = _JPrefab.Instantiate();
            }
            return @object as T;
        }
        public void SetPoolObject(string paht, object gameObject)
        {
            Queue<object> PoolQueue = default;
            if (PoolDic.TryGetValue(paht, out PoolQueue) == false)
            {
                PoolQueue = new Queue<object>();
                PoolDic[paht] = PoolQueue;
            }
            PoolQueue.Enqueue(gameObject);
        }
    }
}
