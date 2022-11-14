using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arrive : MonoBehaviour
{
    //声明委托
    public delegate float Ar(float vr);
    public delegate void IAmReady();

    //新建委托
    public Ar ar;
    public IAmReady iAmReady;

    //目标物体
    public GameObject targetObject;
    //目标位置
    private Transform target;
    //角色位置
    private Transform character;

    //角色速度
    private Vector3 character_V = new Vector3(0,0,0);
    //角色角速度
    private float character_Vr = 0f;//0~1 插值表示

    //最大加速度
    public float maxAcceleration=5f;
    //最大速度
    public float maxSpeed=10f;
    //最大角速度
    public float maxVr = 30f;
    //到达目标的半径
    public float targetRadius=5f;
    //开始减速的半径
    public float slowRadius=50f;
    //到达目标速度的时间
    public float timeToTarget = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        target = targetObject.GetComponent<Transform>();
        character = this.GetComponent<Transform>();
        iAmReady += empty;
    }
    void empty()
    {

    }
    public Vector3 getStreeing()
    {
        Vector3 result = new Vector3();
        //获取方向
        Vector3 direction = target.position - character.position;
        float distance = direction.magnitude;
        //如果已经达到 则不转向
        if (distance < targetRadius)
        {
            iAmReady();
            return result;
        }
        float targetSpeed;
        //如果在减速半径外 加速到最大速度
        if(distance>slowRadius)
        {
            targetSpeed = maxSpeed;
        }
        //否则按照比例计算速度
        else
        {
            targetSpeed = maxSpeed * distance / slowRadius;
        }
        //目标速度将组合速度和方向
        Vector3 targetVelocity = direction.normalized;
        targetVelocity *= targetSpeed;

        //加速尝试达到目标速度
        result = targetVelocity - character_V;
        result /= timeToTarget;

        //检擦速度是否过快
        if(result.magnitude>maxAcceleration)
        {
            result=result.normalized;
            result *= maxAcceleration;
        }
        return result;
    }
    // Update is called once per frame
    void Update()
    {
        character.position+= character_V * Time.deltaTime;
        Quaternion rotate = Quaternion.LookRotation(target.position - character.position,new Vector3(0,1,0));
        character.rotation = Quaternion.Slerp(character.rotation , rotate ,character_Vr);
        Vector3 resultA = getStreeing();
        if (resultA.magnitude != 0 )
        {
            character_V += resultA * Time.deltaTime;
        }
        else
        {
            character_V *= 0;
        }
        float resultB = ar(character_Vr);
        if(resultB==0)
        {
            character_Vr *= 0;
        }
        else
        {
            character_Vr += resultB*Time.deltaTime;
        }
        
    }
}
