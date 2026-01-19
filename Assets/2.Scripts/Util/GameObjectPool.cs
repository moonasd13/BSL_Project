using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool<T> where T : Component
{
    Queue<T> m_pool;
    Func<T> m_CreateFunc;
    int m_count;

    public GameObjectPool(int count, Func<T> createFunc)
    {
        m_count = count;
        m_CreateFunc = createFunc;
        m_pool = new Queue<T>(count);
        for (int i = 0; i < m_count; i++)
        {
            m_pool.Enqueue(createFunc());
        }
    }
    public T Get()
    {
        if (m_pool.Count > 0)
            return m_pool.Dequeue();
        return m_CreateFunc();
    }
    public void Set(T obj)
    {
        m_pool.Enqueue(obj);
    }
}