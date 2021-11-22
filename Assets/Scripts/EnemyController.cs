using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;
    public int score;
    public ParticleSystem smokeEffect;
    public Text text;
    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;
    bool broken = true;
    public bool hard;
    public GameObject DialogueBoxCanvas;
    
    public GameObject loseCanvas;
    public GameObject nextlevelCanvas;
    public GameObject winCanvas;
    Animator animator;

    SpriteRenderer sprite;

    public GameObject mainCanvas;

    public RubyController ruby;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        score = 0;
        // mainCanvas = GameObject.Find("MyCanvas");
        // DialogueBoxCanvas = mainCanvas.transform.Find("DialogueBoxCanvas").gameObject;
        // mainCanvas.SetActive(false);
        winCanvas.SetActive(false);
        loseCanvas.SetActive(false);
        //text.SetActive(false);
        if(hard){
            sprite.material.SetColor("_Color", Color.red);
            speed = 5;
        }
        else{
            speed = 3;
        }
    }

    void Update()
    {
        //remember ! inverse the test, so if broken is true !broken will be false and return won’t be executed.
        if (!broken)
        {
            return;
        }
        if (score ==4)
        {
         mainCanvas.SetActive(true);
         winCanvas.SetActive(true);
         loseCanvas.SetActive(false);
         DialogueBoxCanvas.SetActive(false);
        }
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    void FixedUpdate()
    {
        //remember ! inverse the test, so if broken is true !broken will be false and return won’t be executed.
        if (!broken)
        {
            return;
        }

        Vector2 position = rigidbody2D.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null && hard == false)
        {
            player.ChangeHealth(-1);
        }
        else if (player != null && hard == true)
        {
            player.ChangeHealth(-2);
        }
    }

    //Public because we want to call it from elsewhere like the projectile script
    public void Fix()
    {
        ruby.currentrobotsfixt++;
        broken = false;
        rigidbody2D.simulated = false;
        //optional if you added the fixed animation
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
        ruby.scoreText.text = "Score: " + ruby.currentrobotsfixt;
        if(FindObjectOfType<RubyController>().currentrobotsfixt>=4)
        {
            if(SceneManager.GetActiveScene().name=="Level 22")
            {
            FindObjectOfType<RubyController>().gameOver = true;
            mainCanvas.SetActive(true);
            winCanvas.SetActive(true);
            loseCanvas.SetActive(false);
            DialogueBoxCanvas.SetActive(false);
            FindObjectOfType<RubyController>().BGmusic.clip=ruby.winsound;
            FindObjectOfType<RubyController>().BGmusic.Play();
            }
            else
            {
            mainCanvas.SetActive(true);
            nextlevelCanvas.SetActive(true);
            loseCanvas.SetActive(false);
            DialogueBoxCanvas.SetActive(false);
            //FindObjectOfType<RubyController>().BGmusic.clip=ruby.winsound;
            //FindObjectOfType<RubyController>().BGmusic.Play();
            }
         
        }
    }
}