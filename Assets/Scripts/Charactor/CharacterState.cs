using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate void IAmAlive();
public class CharacterState : MonoBehaviour
{
    //生命状态
    public enum HealthState
    {
        Normol = 75,
        LightInjury = 25,
        SeriousInjury = 0,
        Dying = -1,
        IsBeCure =1,
        Die = 2,
    }
    //濒死事件
    public UnityAction IsDying;
    //死亡倒计时
    private float timeToDie=10f;
    //救助倒计时
    private float timeToAlive=3f;
    //救活事件
    public static IAmAlive iAmAlive;
    //生命属性
    public Health m_heal;
    public HealthState healthState;
    //自回血
    public bool ReHeal;
    public float hot;
    //自回护甲
    public bool ReArmor;
    public float aot;
    //角色属性
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
        //自回血不能越过当前生命状态。
        if(ReHeal&&m_heal.CurrentHealth<(float)healthState)
        {
            m_heal.CurrentHealth += Time.deltaTime * hot;
        }
        if (ReArmor && m_heal.CurrentArmor < m_heal.maxArmor) 
        {
            m_heal.upDateArmor(Time.deltaTime * aot);
        }
    }

    //处理生命状态———————————————————————
    void UpdateHealthState(float health,GameObject source)
    {
        //Debug.Log("当前生命值" + health);
        //健康
        if (health > (float)HealthState.Normol)
        {
            healthState = HealthState.Normol;
            m_character.SetMovingSpeed(1);
            m_character.SetSteeringSpeed(1);
            return;
        }
        //轻伤
        else if (health > (float)HealthState.LightInjury)
        {
            healthState = HealthState.LightInjury;
            m_character.SetMovingSpeed(0.8f);
            m_character.SetSteeringSpeed(0.8f);
            return;
        }
        //重伤
        else if (health > (float)HealthState.SeriousInjury)
        {
            healthState = HealthState.SeriousInjury;
            m_character.SetMovingSpeed(0.5f);
            m_character.SetSteeringSpeed(0.5f);
            return;
        }
        //濒死
        else
        {
            healthState = HealthState.Dying;
            m_character.SetMovingSpeed(0.1f);
            m_character.SetSteeringSpeed(0.5f);
            Dying();
            return;
        }
    }
    //处理其他状态——————————————————————
    //处理濒死
    void Dying()
    {
        m_heal.Invincible = true;
        Invoke("Die", timeToDie);
        IsDying?.Invoke();
    }
    //被救援
    void BeCure()
    {
        Invoke("IsAlive",timeToAlive);
    }
    //救援完成
    void IsAlive()
    {
        CancelInvoke("Die");
        m_heal.Invincible = false;
        m_heal.CurrentHealth = 50f;
        UpdateHealthState(50f, null);
        iAmAlive();
        Debug.Log("被救起"+ m_heal.CurrentHealth);
    }
    //处理死亡
    void Die()
    {
        Debug.Log("死亡");

    }
}
