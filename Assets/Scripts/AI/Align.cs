using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align : MonoBehaviour
{

    //目标物体
    public GameObject targetObject;
    //目标位置
    private Transform target;
    //角色位置
    private Transform character;

    //角色速度
    private Vector3 character_V = new Vector3(0, 0, 0);
    //最大加速度
    public float maxAcceleration = 5f;
    //最大速度
    public float maxSpeed = 10f;
    //到达目标的半径
    public float targetRadius = 5f;
    //开始减速的半径
    public float slowRadius = 30f;
    //到达目标速度的时间
    public float timeToTarget = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        target = targetObject.GetComponent<Transform>();
        character = this.GetComponent<Transform>();
        this.GetComponent<Arrive>().ar += GetStreeing;
    }
    public float GetStreeing(float character_Vr)
    {
        float result = 0;
        //获取目标到自身方向
        Vector3 direct = target.position - character.position;
        float rotation = Vector3.Angle(new Vector3(0, 0, 1), direct);
        //如果已经达到 则不转向
        if (rotation < targetRadius)
        {
            return result;
        }
        float targetRotation = 0;
        //如果在减速半径外 加速到最大速度
        if (rotation>slowRadius)
        {
            targetRotation = maxSpeed;
        }
        //否则按照比例计算速度 
        else
        {
            targetRotation = maxSpeed * rotation / slowRadius;
        }
        //目标速度将组合速度和方向

        //加速尝试达到目标速度
        result =  targetRotation - character_Vr*rotation;
        result/= timeToTarget;

        //检擦加速度是否过快
        if (result > maxAcceleration)
        {
            result = maxAcceleration;
        }
        result /= rotation;
        return result;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
