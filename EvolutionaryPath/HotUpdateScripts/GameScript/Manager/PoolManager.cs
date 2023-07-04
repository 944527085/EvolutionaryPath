using JEngine.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HotUpdateScripts.GameScript.Manager
{

    class PoolManager : Singleton<PoolManager>
    {
        private Dictionary<string, JPrefab> JprefabDic;
        private Dictionary<string, Queue<GameObject>> PoolDic;
        public override void Init()
        {
            PoolDic = new Dictionary<string, Queue<GameObject>>();
            JprefabDic = new Dictionary<string, JPrefab>();
        }
        public void GetPoolObject(string path, Action<GameObject> callback = null)
        {
            GameObject @object = null;
            Queue<GameObject> PoolQueue = default;
            if (PoolDic.TryGetValue(path, out PoolQueue) == false)
            {
                PoolQueue = new Queue<GameObject>();
                PoolDic[path] = PoolQueue;
            }
            if (PoolQueue.Count > 0)
            {
                @object = PoolQueue.Dequeue();
                callback?.Invoke(@object);
            }
            else if (JprefabDic.ContainsKey(path))
            {
                JPrefab jPrefab = JprefabDic[path];
                @object = jPrefab.Instantiate();
                callback?.Invoke(@object);
            }
            else
            {
                new JPrefab(path, (bool isFinish, JPrefab _JPrefab) =>
                {
                   

                    if (!isFinish)
                    {
                        Log.PrintError("加载资源失败path" + path);
                        return;
                    }
                    Log.PrintError("加载资源成功path" + path);
                    JprefabDic.Add(path, _JPrefab);
                     @object = _JPrefab.Instantiate();
                    callback?.Invoke(@object);
                });
                
            }
        }
        public void SetPoolObject(string paht, GameObject gameObject)
        {
            Queue<GameObject> PoolQueue = default;
            if (PoolDic.TryGetValue(paht, out PoolQueue) == false)
            {
                PoolQueue = new Queue<GameObject>();
                PoolDic[paht] = PoolQueue;
            }
            PoolQueue.Enqueue(gameObject);
        }
    }
}
