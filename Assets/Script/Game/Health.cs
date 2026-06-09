using System;
using UnityEngine;

[RequireComponent(typeof(BaseObject))]
[DisallowMultipleComponent]
public class Health : MonoBehaviour, ICollisionReceiver
{
    private BaseObject owner;

    public event Action OnDead;

    [field:SerializeField]
    public int MaxHPCap {  get; private set; }  // 피통 증가 상한
    [field: SerializeField]
    public int MaxHP { get; private set; }  // 피통
    [field: SerializeField]
    public int CurrentHP { get; private set; }  // 현재 HP

    public CollisionType CollType => CollisionType.Damage;
    public bool ProccessCollisionContext(ICollisionContext collision)
    {
        if (collision.CollType != CollisionType.Damage || collision is not DamageCollCtx damageCtx)
        {
            Debug.LogWarning("Health에 대미지타입이 아닌 컨텍스트가 전달됨");
            return false;
        }

        TakeDamage(damageCtx.damage);

        return true;
    }

    public void ModifyMaxHP(int amount)
    {
        MaxHP = Mathf.Clamp(MaxHP +  amount, 1, MaxHPCap);
    }

    public void Heal(int amount)
    {
        CurrentHP = Mathf.Min(MaxHP, CurrentHP + amount);
    }

    public void TakeDamage(int amount)
    {
        CurrentHP -= amount;
        if (CurrentHP < 0)
        {
            Debug.Log($"{gameObject.name} 사망");
            OnDead?.Invoke();
        }
    }

    public void SetData(BaseObject ownerObj, int dataMaxHPCap, int dataMaxHP, int dataCurrentHP)
    {
        owner = ownerObj;
        MaxHPCap = dataMaxHPCap;
        MaxHP = dataMaxHP;
        CurrentHP = dataCurrentHP;
    }
}
