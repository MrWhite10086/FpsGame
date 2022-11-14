using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StanderState : MonoBehaviour
{
    public int SceneNo;

    //生命属性
    public Health m_heal;
    // Start is called before the first frame update
    void Start()
    {
        m_heal = this.GetComponent<Health>();
        m_heal.OnDamaged += UpdateHealthState;
        m_heal.OnHealed += UpdateHealthState;
    }
    //处理生命状态———————————————————————
    void UpdateHealthState(float health, GameObject source)
    {
        Debug.Log("当前生命值" + health);
        if (health>0)
        {

        }
        else
        {
            Die();
        }
    }
    void Die()
    {
        SceneManager.LoadScene(SceneNo);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
