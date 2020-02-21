using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Transform StartPoint;        //标记游戏开始位置
    public Transform SpawnPoint;        //标记针的实例化位置
    public GameObject PinPreferb;       //获得针的预制体
    private PinMovement tmpPin;         //获得控制针移动脚本
    private bool isDown = false;        //判断鼠标是否按下
    private bool isOver = false;        //判断游戏是否结束
    private int score = 0;      //标记游戏的分数
    public Text scoreText;      //获得UI的Text组件
    private Camera mainCamera;      //获得相机组件
    private float speed = 3;        //设置动画的播放速度

    


    // Start is called before the first frame update
    void Start()
    {
        SpawnPin();     //实例化针
        mainCamera = Camera.main;       //获得相机组件
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && isDown == false && isOver == false)      //判断鼠标左键按下
        {
            tmpPin.Fly();       //调用发射针的函数
            isDown = true;      //标记鼠标左键已经按下
            SpawnPin();     //实例化针
        }
        if (Input.GetMouseButtonUp(0))      //判断鼠标左键松开
        {
            isDown = false;     //标记鼠标左键已经松开
            if(isOver == false)
            {
                score++;        //分数 + 1
                scoreText.text = score.ToString();      //跟新游戏分数
                
            }
        }
    }

    void SpawnPin()     //实例化针
    {
       tmpPin =  GameObject.Instantiate(PinPreferb, SpawnPoint.position, PinPreferb.transform.rotation).GetComponent<PinMovement>();    
    }

    public void GameOver()      //游戏结束
    {
        if (isOver == false)    //判断游戏是否借宿
        {
            GameObject.Find("BigCircle").GetComponent<RollTheBall>().enabled = false;   //圆面禁止旋转
            StartCoroutine(GameOberAnimation());        //加载游戏结束动画
            isOver = true;
        }
    }


    IEnumerator GameOberAnimation()     //设置动画
    {
        while (true)
        {
            mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, Color.red, speed * Time.deltaTime);     //设置背景颜色的转变，时间为speed * Time.deltaTime
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 4, speed * Time.deltaTime);       //设置相机大小的转变，时间为speed * Time.deltaTime

            if (Mathf.Abs(mainCamera.orthographicSize - 4) < 0.05f)     //加载退出循环条件
            {
                break;
            }

            yield return 0;     //暂停一帧
        }
        yield return new WaitForSeconds(1);     //设置停顿1秒
        SceneManager.LoadScene("SampleScene");      //重新加载游戏场景
    }
}
