using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooted : MonoBehaviour
{
    //�ܻ��˺�
    public float damage=5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //�����������������ж�Ϊ������
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("������");
        this.GetComponent<Health>().TakeDamage(damage, null);
    }
}