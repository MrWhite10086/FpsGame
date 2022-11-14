using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    //当前生命值
    public float CurrentHealth { get; set; }
    //当前护甲值
    public float CurrentArmor { get; set; }
    //生命值上限
    [Tooltip("Maximum amount of health")]
    public float maxHealth;
    //护甲值上限
    [Tooltip("Maximum amount of armor")]
    public float maxArmor;

    //无敌
    public bool Invincible;

    //伤害事件
    public UnityAction<float, GameObject> OnDamaged;
    //治疗事件
    public UnityAction<float, GameObject> OnHealed;
    //死亡事件
    public UnityAction OnDie;
    //护甲破损事件
    public UnityAction ArmorDestroyed;
    //护甲回满事件
    public UnityAction ArmorFull;

    //死亡
    public bool m_IsDead;
     void Start()
    {
        CurrentHealth = maxHealth;
        CurrentArmor = maxArmor;
    }
    //护甲值变化——————————————————————
    public float upDateArmor(float plus)
    {
        CurrentArmor += plus;
        float damage = CurrentArmor;
        CurrentArmor = Mathf.Clamp(CurrentArmor, 0f, maxArmor);
        if (CurrentArmor==0f)
        {
            ArmorDestroyed?.Invoke();
        }
        if(CurrentArmor==maxArmor)
        {
            ArmorFull?.Invoke();
        }
        return damage;
    }
    //生命值变化——————————————————————
    public void Heal(float plus,GameObject CureSource)
    {
        CurrentHealth += plus;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);
        OnHealed?.Invoke(CurrentHealth,CureSource);
    }
    public void TakeDamage(float damage, GameObject damageSource)
    {
        if (Invincible)
            return;

        CurrentHealth += upDateArmor(-damage);
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);
        OnDamaged?.Invoke(CurrentHealth, damageSource);

        HandleDeath();
    }


    //死亡or濒死
    public void HandleDeath()
    {
        if (m_IsDead)
            return;
        if(CurrentHealth<=0f)
        {
            m_IsDead = true;
            OnDie?.Invoke();
        }
    }
}
