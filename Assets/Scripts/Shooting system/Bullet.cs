using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //����ʱ��
    private float liveTime;
    private float t=0;
    //��ʼλ��
    private Vector3 position;
    private Transform tra;
    // Start is called before the first frame update
    void Start()
    {
        tra = this.GetComponent<Transform>();
    }
    public void setLiveTime(float flo)
    {
        liveTime = flo;
    }
    public void setPosition(Vector3 vec)
    {
        position = vec;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if(t>liveTime)
        {
            Object.Destroy(this.gameObject);
        }
    }
}
