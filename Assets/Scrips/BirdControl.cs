using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BirdControl : MonoBehaviour
{
    public Sprite[] bird;
    SpriteRenderer spriteRenderer;
    bool spritecontrol = true;
    int birdCount = 0;
    float birdAnimTime = 0f;
    Rigidbody2D physics;
    int point = 0;
    public Text pointtext;
    bool gameover = false;
    GameControl gameControl;

    //AudioSource sound;// Bu yöntemle yapılınca sesler çakışınca birbirini kesiyor.
    //public AudioClip hitsound;// Bu yöntemle yapılınca sesler çakışınca birbirini kesiyor.
    //public AudioClip pointsound;// Bu yöntemle yapılınca sesler çakışınca birbirini kesiyor.
    //public AudioClip wingsound;// Bu yöntemle yapılınca sesler çakışınca birbirini kesiyor.

    AudioSource[] sounds;

    int highscore = 0;
    float starttime = 0f;
    public Text starttext;


    void Start()
    {
        physics = GetComponent<Rigidbody2D>();        
        spriteRenderer = GetComponent<SpriteRenderer>();                
        pointtext.text = "Point = " + point;
        gameControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>();
        //sound = GetComponent<AudioSource>();
        sounds = GetComponents<AudioSource>();
        highscore = PlayerPrefs.GetInt("highscore");
        physics.GetComponent<Rigidbody2D>().gravityScale = 0f;     
        
    }

    
    void Update()
    {

        starttime += Time.deltaTime;
        if (starttime > 3f) 
        {
            physics.GetComponent<Rigidbody2D>().gravityScale = 0.7f;
            
        }
        
        if (Input.GetMouseButtonDown(0) && !gameover) 
        {
            physics.velocity = new Vector2(0,0); // Her tıklandığında düşme hızı sıfırlanır.
            physics.AddForce(new Vector2(0, 150));
            //sound.clip = wingsound; // Bu yöntemle yapılınca sesler çakışınca birbirini kesiyor.
            //sound.Play();// Bu yöntemle yapılınca sesler çakışınca birbirini kesiyor.
            sounds[0].Play();
            
        }
        transform.eulerAngles = new Vector3(0, 0, 0);

        if (physics.velocity.y > 0 && starttime > 2f)
        {
            transform.eulerAngles = new Vector3(0, 0, 30);
        }
        else if (physics.velocity.y < 0 && starttime > 2f)
        {
            transform.eulerAngles = new Vector3(0, 0, -30);
        }


        Animation();
    }
    void Animation()
    {
        birdAnimTime += Time.deltaTime;
        if (birdAnimTime > 0.2f)
        {
            birdAnimTime = 0;

            if (spritecontrol)
            {
                spriteRenderer.sprite = bird[birdCount];
                birdCount++;

                if (birdCount == bird.Length)
                {
                    birdCount--;
                    spritecontrol = false;
                }
            }
            else
            {
                birdCount--;
                spriteRenderer.sprite = bird[birdCount];

                if (birdCount == 0)
                {
                    birdCount++;
                    spritecontrol = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "point")
        {
            point++;
            pointtext.text = "Point = " + point;
            //sound.clip = pointsound;// Bu yöntemle yapılınca sesler çakışınca birbirini kesiyor.
            //sound.Play();// Bu yöntemle yapılınca sesler çakışınca birbirini kesiyor.
            sounds[1].Play();
        }

        if (collision.gameObject.tag == "barrier")
        {
            gameover = true;
            gameControl.GameOver();
            //sound.clip = hitsound;// Bu yöntemle yapılınca sesler çakışınca birbirini kesiyor.
            //sound.Play();// Bu yöntemle yapılınca sesler çakışınca birbirini kesiyor.
            sounds[2].Play();
            GetComponent<CircleCollider2D>().enabled = false;

            if ( point > highscore) 
            {
                highscore= point;
                PlayerPrefs.SetInt("highscore", highscore);
            }

            Invoke("ReturnMainMenu", 2);
        }
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
