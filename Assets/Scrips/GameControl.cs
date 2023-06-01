using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public GameObject sky1, sky2;    
    Rigidbody2D physics1, physics2;
    float length = 0;

    public GameObject barrier;
    public int barriernumber = 5;
    GameObject[] barriers;
    float changetime = 0;
    int count = 0;
    bool gameover = false;

    public Text countDown;    
    

    void Start()
    {
        physics1 = sky1.GetComponent<Rigidbody2D>();
        physics2 = sky2.GetComponent<Rigidbody2D>();

        physics1.velocity = new Vector2(-1.5f, 0);
        physics2.velocity = new Vector2(-1.5f, 0);

        length = sky1.GetComponent<BoxCollider2D>().size.x;

        barriers = new GameObject[barriernumber];

        for (int i = 0; i < barriers.Length; i++)
        {
           
            barriers[i] = Instantiate(barrier, new Vector2(-20, -20), Quaternion.identity);
            Rigidbody2D physics = barriers[i].AddComponent<Rigidbody2D>();
            physics.gravityScale = 0;
            physics.velocity = new Vector2(-1.5f, 0);            
            
        }
        StartCoroutine(CountDown());
    }
    
    void Update()
    {
        if(!gameover)
        {
            // Sky Move
            if (sky1.transform.position.x <= -length)
            {
                sky1.transform.position += new Vector3(length * 2, 0);
            }

            if (sky2.transform.position.x <= -length)
            {
                sky2.transform.position += new Vector3(length * 2, 0);
            }

            // Barrier rondom change
            changetime += Time.deltaTime;
            if (changetime > 2f)
            {
                changetime = 0;
                float Yaxis = Random.Range(-1f, -2.8f);
                barriers[count].transform.position = new Vector3(6.3f, Yaxis);
                count++;
                if (count >= barriers.Length)
                {
                    count = 0;
                }
            }
        }  
    }
    public void GameOver()
    {
        for (int i = 0; i < barriers.Length; i++)
        {
            barriers[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            physics1.velocity = Vector2.zero;
            physics2.velocity = Vector2.zero;
        }     
        gameover = true;
    }

    IEnumerator CountDown()
    {
        countDown.text = "3";
        yield return new WaitForSeconds(1);
        countDown.text = "2";
        yield return new WaitForSeconds(1);
        countDown.text = "1";
        yield return new WaitForSeconds(1);
        countDown.gameObject.SetActive(false);

    }
}
