using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Transform StartPoint;        
    public Transform SpawnPoint;        
    public GameObject PinPreferb;       
    private PinMovement tmpPin;         
    private bool isDown = false;        
    private bool isOver = false;        
    private int score = 0;      
    public Text scoreText;     
    private Camera mainCamera;      
    private float speed = 3;        

    


    // Start is called before the first frame update
    void Start()
    {
        SpawnPin();    
        mainCamera = Camera.main;       
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && isDown == false && isOver == false)      
        {
            tmpPin.Fly();      
            isDown = true;      
            SpawnPin();    
        }
        if (Input.GetMouseButtonUp(0))      
        {
            isDown = false;     
            if(isOver == false)
            {
                score++;        
                scoreText.text = score.ToString();      
                
            }
        }
    }

    void SpawnPin()     
    {
       tmpPin =  GameObject.Instantiate(PinPreferb, SpawnPoint.position, PinPreferb.transform.rotation).GetComponent<PinMovement>();    
    }

    public void GameOver()     
    {
        if (isOver == false)    
        {
            GameObject.Find("BigCircle").GetComponent<RollTheBall>().enabled = false;  
            StartCoroutine(GameOberAnimation());        
            isOver = true;
        }
    }


    IEnumerator GameOberAnimation()     
    {
        while (true)
        {
            mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, Color.red, speed * Time.deltaTime);     
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 4, speed * Time.deltaTime);       

            if (Mathf.Abs(mainCamera.orthographicSize - 4) < 0.05f)     
            {
                break;
            }

            yield return 0;     
        }
        yield return new WaitForSeconds(1);    
        SceneManager.LoadScene("SampleScene");    
    }
}
