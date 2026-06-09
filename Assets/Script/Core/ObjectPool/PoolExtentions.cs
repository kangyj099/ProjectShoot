using UnityEngine;

public static class PoolExtentions
{
    public static void Return(this IPoolable poolable)
    {
        if (poolable.Pool != null)
        {
            poolable.Pool.Release(poolable);
        }
        else
        {
            if (poolable is Component comp)
            {
                GameObject.Destroy(comp.gameObject);
            }
        }
    }
}
