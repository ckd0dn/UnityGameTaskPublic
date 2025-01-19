using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ObjectManager
{
    public PlayerController Player { get; private set; }
    public HashSet<MonsterController> Monsters { get; private set; } = new HashSet<MonsterController>();
    public HashSet<HpBar> HpBars { get; private set; } = new HashSet<HpBar>();
    
    public T Spawn<T>(string key) where T : BaseController
    {
        System.Type type = typeof(T);

        if (type == typeof(PlayerController))
        {
            GameObject go = Managers.Resource.Instantiate(key, pooling: true);
            PlayerController p = go.GetComponent<PlayerController>();

            Player = p;

            return p as T;
        }
        else if (type == typeof(MonsterController))
        {
            GameObject go = Managers.Resource.Instantiate(key, pooling: true);
            MonsterController m = go.GetComponent<MonsterController>();
            
            Monsters.Add(m);
            return m as T;
        }
        else if (type == typeof(HpBar))
        {
            GameObject go = Managers.Resource.Instantiate(key, pooling: true);
            HpBar h = go.GetComponent<HpBar>();

            HpBars.Add(h);
            return h as T;
        }

        return null;
    }

    public void Despawn<T>(T obj) where T : BaseController
    {
        System.Type type = typeof(T);

        if (type == typeof(PlayerController))
        {

        }
        else if (type == typeof(MonsterController))
        {
            Monsters.Remove(obj as MonsterController);
            Managers.Resource.Destroy(obj.gameObject);
        }
        else if (type == typeof(HpBar))
        {
            HpBars.Remove(obj as HpBar);
            Managers.Resource.Destroy(obj.gameObject);
        }

    }
}
