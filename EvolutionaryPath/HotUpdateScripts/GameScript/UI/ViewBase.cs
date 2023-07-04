using HotUpdateScripts.GameScript.Manager;
using JEngine.Core;
using System;
using UnityEngine;

namespace HotUpdateScripts.GameScript.UI
{
    abstract class ViewBase
    {
        public abstract ViewModel viewModel { get; }
        public GameObject gameObject { get; private set; }
        public Transform transform { get; private set; }
        public bool IsActive
        {
            get
            {
                if (gameObject == null)
                {
                    return false;
                }
                return gameObject.activeSelf;
            }
        }
        public void CreatOn(Transform parent, Action action = null)
        {
            string paht = UIManager.Instance.GetJPrefabPath(viewModel);
            PoolManager.Instance.GetPoolObject(paht, (GameObject obj) =>
            {
                Log.Print("创建成功" + paht);
                gameObject = obj;
                transform = gameObject.transform;
                transform.SetParent(parent);
                transform.localScale = Vector3.one;
                Init();
                action?.Invoke();
            });

        }
        public virtual void Init()
        {

        }
        public virtual void Show()
        {
            if (gameObject == null)
            {
                return;
            }
            gameObject.SetActive(true);
        }
        public virtual void Hide()
        {
            if (gameObject == null)
            {
                return;
            }
            gameObject.SetActive(false);
        }
        public virtual void Destroy()
        {
            GameObject.Destroy(gameObject);
            gameObject = null;
            transform = null;
        }
    }
}
