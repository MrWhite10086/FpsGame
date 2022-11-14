using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooted : MonoBehaviour
{
    //受击伤害
    public float damage=5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //当有其他东西进入判断为被击中
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("被击中");
        this.GetComponent<Health>().TakeDamage(damage, null);
    }
}
