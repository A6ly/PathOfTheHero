using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolManager
{
    class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }
        Stack<Poolable> poolStack = new Stack<Poolable>();

        public void Init(GameObject original, int count = 10)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root";

            for (int i = 0; i < count; i++)
            {
                Push(Creat());
            }
        }

        Poolable Creat()
        {
            GameObject go = Object.Instantiate(Original);
            go.name = Original.name;

            return go.GetOrAddComponent<Poolable>();
        }

        public void Push(Poolable poolable)
        {
            if (poolable == null)
            {
                return;
            }

            poolable.transform.SetParent(Root);
            poolable.gameObject.SetActive(false);
            poolable.IsUsing = false;

            poolStack.Push(poolable);
        }

        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            if (poolStack.Count > 0)
            {
                poolable = poolStack.Pop();
            }
            else
            {
                poolable = Creat();
            }

            poolable.transform.SetParent(parent);
            poolable.gameObject.SetActive(true);
            poolable.IsUsing = true;

            return poolable;
        }
    }

    Dictionary<string, Pool> poolDic = new Dictionary<string, Pool>();
    Transform root;

    public void Init()
    {
        if (root == null)
        {
            root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(root);
        }
    }

    public void CreatPool(GameObject original, int count = 2)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.SetParent(root);

        poolDic.Add(original.name, pool);
    }

    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;

        if (!poolDic.ContainsKey(name))
        {
            Object.Destroy(poolable.gameObject);

            return;
        }

        poolDic[name].Push(poolable);
    }

    public Poolable Pop(GameObject original, Transform parent = null)
    {
        if (!poolDic.ContainsKey(original.name))
        {
            CreatPool(original);
        }

        return poolDic[original.name].Pop(parent);
    }

    public GameObject GetOriginal(string name)
    {
        if (!poolDic.ContainsKey(name))
        {
            return null;
        }

        return poolDic[name].Original;
    }

    public void Clear()
    {
        foreach (Transform child in root)
        {
            Object.Destroy(child.gameObject);
        }

        poolDic.Clear();
    }
}
