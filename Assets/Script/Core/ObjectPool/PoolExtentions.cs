using UnityEngine;

public static class PoolExtentions
{
    public static void Release(this IPoolable poolable)
    {
        poolable.Pool.Release(poolable);
    }
}
