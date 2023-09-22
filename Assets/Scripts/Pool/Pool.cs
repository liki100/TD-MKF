using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour
{
    private readonly T _prefab;
    private readonly Transform _container;
    private readonly List<T> _objects;

    public Pool(T prefab, int count, Transform container = null)
    {
        _prefab = prefab;
        _container = container;
        _objects = new List<T>();

        for (var i = 0; i < count; i++)
        {
            Create();
        }
    }
        
    public T Get()
    {
        var obj = _objects.FirstOrDefault(x => !x.isActiveAndEnabled);

        if (obj == null)
        {
            obj = Create();
        }

        obj.gameObject.SetActive(true);
        return obj;
    }

    public bool Has(string id)
    {
        return _objects.Any(x => x.name == id);
    }

    public void Release(T obj)
    {
        obj.gameObject.SetActive(false);
    }
        
    private T Create()
    {
        var obj = Object.Instantiate(_prefab, _container);
        obj.gameObject.SetActive(false);
        _objects.Add(obj);
        return obj;
    }
}