using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PinMovement : MonoBehaviour
{

    private float speed = 30.0f;        //针的飞行速度  

    private bool isFly = false;     //判断针是否在飞
    private bool isReach = false;       //判断针是否到达游戏开始位置
    private Transform StartPoint;   //标记游戏开始位置
    public Transform CirclePoint;   //标记圆的位置
    public Vector3 TargetPoint;     //标记目标位置，即圆标面
    private Text scoreText;     //用来更新游戏分数



    // Start is called before the first frame update
    void Start()
    {
        
        StartPoint = GameObject.Find("StartPoint").transform;       //获得StartPoint的transform值
        CirclePoint = GameObject.Find("BigCircle").transform;       //获得CirclePoint的transform值

        TargetPoint = CirclePoint.transform.position;
        TargetPoint.y = TargetPoint.y - 2.76f;      //因为项目中 圆心坐标为 1.5   圆面坐标大致为 -1.26  所以相减即为目标坐标
    }

    // Update is called once per frame
    void Update()
    {
        if(isFly == false)
        {
            if(isReach == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, StartPoint.position, speed * Time.deltaTime);      //从实例化位置移动至游戏位置

                if(StartPoint.transform.position.y - transform.position.y < 0.05f)  //判断是否到达
                {
                    isReach = true;
                }
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPoint, speed * Time.deltaTime);      //将针发射出去

            if(TargetPoint.y - transform.position.y < 0.05f)
            {
                transform.parent = CirclePoint;     //让针随着圆一起旋转
                isFly = false;
            }
        }
    }




    public void Fly()
    {
        isFly = true;
        isReach = true;
    }

}
