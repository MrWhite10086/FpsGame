using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    //��ǰ����ֵ
    public float CurrentHealth { get; set; }
    //��ǰ����ֵ
    public float CurrentArmor { get; set; }
    //����ֵ����
    [Tooltip("Maximum amount of health")]
    public float maxHealth;
    //����ֵ����
    [Tooltip("Maximum amount of armor")]
    public float maxArmor;

    //�޵�
    public bool Invincible;

    //�˺��¼�
    public UnityAction<float, GameObject> OnDamaged;
    //�����¼�
    public UnityAction<float, GameObject> OnHealed;
    //�����¼�
    public UnityAction OnDie;
    //���������¼�
    public UnityAction ArmorDestroyed;
    //���׻����¼�
    public UnityAction ArmorFull;

    //����
    public bool m_IsDead;
     void Start()
    {
        CurrentHealth = maxHealth;
        CurrentArmor = maxArmor;
    }
    //����ֵ�仯��������������������������������������������
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
    //����ֵ�仯��������������������������������������������
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


    //����or����
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
