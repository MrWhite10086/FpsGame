using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate void IAmAlive();
public class CharacterState : MonoBehaviour
{
    //����״̬
    public enum HealthState
    {
        Normol = 75,
        LightInjury = 25,
        SeriousInjury = 0,
        Dying = -1,
        IsBeCure =1,
        Die = 2,
    }
    //�����¼�
    public UnityAction IsDying;
    //��������ʱ
    private float timeToDie=10f;
    //��������ʱ
    private float timeToAlive=3f;
    //�Ȼ��¼�
    public static IAmAlive iAmAlive;
    //��������
    public Health m_heal;
    public HealthState healthState;
    //�Ի�Ѫ
    public bool ReHeal;
    public float hot;
    //�Իػ���
    public bool ReArmor;
    public float aot;
    //��ɫ����
    Character m_character;
    // Start is called before the first frame update
    void Start()
    {
        healthState = HealthState.Normol;
        m_heal = this.GetComponent<Health>();
        m_character = this.GetComponent<Character>();
        m_heal.OnDamaged += UpdateHealthState;
        m_heal.OnHealed += UpdateHealthState;
        NpcOne.cure += BeCure;
    }

    // Update is called once per frame
    void Update()
    {
        //�Ի�Ѫ����Խ����ǰ����״̬��
        if(ReHeal&&m_heal.CurrentHealth<(float)healthState)
        {
            m_heal.CurrentHealth += Time.deltaTime * hot;
        }
        if (ReArmor && m_heal.CurrentArmor < m_heal.maxArmor) 
        {
            m_heal.upDateArmor(Time.deltaTime * aot);
        }
    }

    //��������״̬����������������������������������������������
    void UpdateHealthState(float health,GameObject source)
    {
        //Debug.Log("��ǰ����ֵ" + health);
        //����
        if (health > (float)HealthState.Normol)
        {
            healthState = HealthState.Normol;
            m_character.SetMovingSpeed(1);
            m_character.SetSteeringSpeed(1);
            return;
        }
        //����
        else if (health > (float)HealthState.LightInjury)
        {
            healthState = HealthState.LightInjury;
            m_character.SetMovingSpeed(0.8f);
            m_character.SetSteeringSpeed(0.8f);
            return;
        }
        //����
        else if (health > (float)HealthState.SeriousInjury)
        {
            healthState = HealthState.SeriousInjury;
            m_character.SetMovingSpeed(0.5f);
            m_character.SetSteeringSpeed(0.5f);
            return;
        }
        //����
        else
        {
            healthState = HealthState.Dying;
            m_character.SetMovingSpeed(0.1f);
            m_character.SetSteeringSpeed(0.5f);
            Dying();
            return;
        }
    }
    //��������״̬��������������������������������������������
    //�������
    void Dying()
    {
        m_heal.Invincible = true;
        Invoke("Die", timeToDie);
        IsDying?.Invoke();
    }
    //����Ԯ
    void BeCure()
    {
        Invoke("IsAlive",timeToAlive);
    }
    //��Ԯ���
    void IsAlive()
    {
        CancelInvoke("Die");
        m_heal.Invincible = false;
        m_heal.CurrentHealth = 50f;
        UpdateHealthState(50f, null);
        iAmAlive();
        Debug.Log("������"+ m_heal.CurrentHealth);
    }
    //��������
    void Die()
    {
        Debug.Log("����");

    }
}
