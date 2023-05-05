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
                return gameObject.activeSelf;
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
