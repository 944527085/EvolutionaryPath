using HotUpdateScripts.GameScript.Manager;
using JEngine.Core;
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
                if (gameObject==null)
                {
                    return false;
                }
                return gameObject.layer== UIManager.ShowViewLayer;
            }
        }
        public virtual void Init(Transform parent)
        {
            string paht = UIManager.Instance.GetJPrefabPath(viewModel);
            gameObject = PoolManager.Instance.GetPoolObject<GameObject>(paht);
            transform = gameObject.transform;
            transform.SetParent(parent);
            transform.localScale = Vector3.one;
        }
        public virtual void Show()
        {
            if (gameObject==null)
            {
                return;
            }
            gameObject.layer = UIManager.ShowViewLayer;
        }
        public virtual void Hide()
        {
            if (gameObject == null)
            {
                return;
            }
            gameObject.layer = UIManager.HideViewLayer;
        }
        public virtual void Destroy()
        {
            Hide();
            string paht = UIManager.Instance.GetJPrefabPath(viewModel);
            gameObject = null;
            transform = null;
            PoolManager.Instance.SetPoolObject(paht,gameObject);
        }
    }
}
