using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PinMovement : MonoBehaviour
{

    private float speed = 30.0f;        

    private bool isFly = false;    
    private bool isReach = false;       
    private Transform StartPoint;   
    public Transform CirclePoint;   
    public Vector3 TargetPoint;     
    private Text scoreText;     



    // Start is called before the first frame update
    void Start()
    {
        
        StartPoint = GameObject.Find("StartPoint").transform;       
        CirclePoint = GameObject.Find("BigCircle").transform;      

        TargetPoint = CirclePoint.transform.position;
        TargetPoint.y = TargetPoint.y - 2.76f;      
    }

    // Update is called once per frame
    void Update()
    {
        if(isFly == false)
        {
            if(isReach == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, StartPoint.position, speed * Time.deltaTime);      

                if(StartPoint.transform.position.y - transform.position.y < 0.05f)  
                {
                    isReach = true;
                }
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPoint, speed * Time.deltaTime);      

            if(TargetPoint.y - transform.position.y < 0.05f)
            {
                transform.parent = CirclePoint;     
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
