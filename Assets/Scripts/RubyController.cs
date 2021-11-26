using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RubyController : MonoBehaviour
{
    public AudioClip throwSound;
    public AudioClip hitSound;
    public bool gameOver;
    public GameObject Losescreen;
    public GameObject Winscreen;
    public int currentrobotsfixt =0;
    public int robotsfixuntillevel =4;
    public Text cogstext;

    public GameObject gainHealtheffect;

    public int cogs = 4;

    public GameObject loseHealtheffect;
    public AudioClip collectedClip;
    public float speed = 3.0f;

    public int maxHealth = 5;

    public GameObject projectilePrefab;

    public int health { get { return currentHealth; } }
    int currentHealth;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    AudioSource audioSource;
    public AudioClip losesound;
    public AudioClip winsound;
    public AudioSource BGmusic;
    public GameObject dialogbox;

    public Text scoreText;

    void Start()
    {
        uptadecogstext();
        if (SceneManager.GetActiveScene().name =="Level 22")
        {
            dialogbox.SetActive(false);
        }
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
       audioSource.PlayOneShot(clip);
    }
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            
            if (cogs > 0)
            {
              Launch();
              cogs --;
              uptadecogstext();
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }
         // remember you need to add "using UnityEngine.SceneManagement" to the top of your script to use the scene manager!  
        if (Input.GetKey(KeyCode.R))

        {

            if (gameOver == true)

            {

              SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // this loads the currently active scene

            }
        }

        if(Input.GetKey(KeyCode.Escape)){
            Application.Quit();
        }
    }
    public void uptadecogstext
    ()
    {
        cogstext.text="cogs: " + cogs.ToString();   
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            if (isInvincible)
                return;
            Instantiate(loseHealtheffect,transform.position,Quaternion.identity);
            isInvincible = true;
            invincibleTimer = timeInvincible;
            PlaySound(hitSound);
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
        if (currentHealth <= 0)
        {
            Losescreen.SetActive(true);
            gameOver = true;
            BGmusic.clip=losesound;
            BGmusic.Play();
        }
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
    }
       
       
}