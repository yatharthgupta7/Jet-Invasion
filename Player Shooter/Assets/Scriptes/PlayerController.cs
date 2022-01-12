using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool gunEquiped;
    public Animator anim;
    [SerializeField] float upSpeed;
    [SerializeField] ParticleSystem jetpackParticle;
    [SerializeField] bool isGrounded;
    [SerializeField] const int MAX_HEALTH = 17;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] GameObject Jetpack;
    [SerializeField] ParticleSystem particleHit;
    [SerializeField] Transform hitPos;
    [SerializeField] GameObject particle;
    [SerializeField] AudioClip coin;
    [SerializeField]AudioSource audioSource;
    bool isDead = false;
    int currentHealth;
    Rigidbody2D rb;
    private ParticleSystem.EmissionModule em;
    void Start()
    {
        em = jetpackParticle.emission;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = MAX_HEALTH;
    }
    void Update()
    {
        if (!isDead) 
        {
            MovePlayer();
        }
    }
    
    void MovePlayer()
    {
        if (isGrounded)
        {
            if(gunEquiped)
            {
                anim.SetBool("runWithGun", true);
            }
            else
            {
                anim.SetBool("run", true);
            }
            //transform.localEulerAngles = new Vector3(0, 0, 0);
            Jetpack.SetActive(false);
            bulletSpawnPoint.localEulerAngles = new Vector3(0, 0, 0);
            bulletSpawnPoint.localPosition = new Vector3(0.978f, 1.286f, 0);
        }
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x < Screen.width / 2)
            {
                Vector2 upForce = new Vector2(0, upSpeed);
                rb.velocity = (upForce * Time.deltaTime);
                //transform.localEulerAngles = new Vector3(0, 0, -15);
                Jetpack.SetActive(true);
                bulletSpawnPoint.localPosition = new Vector3(0.994f, 1.535f, 0);
                em.enabled = true;
                if (gunEquiped)
                {
                    anim.SetBool("GunEquipped", true);
                    anim.SetBool("runWithGun", false);
                }
                else
                {
                    anim.SetBool("run", false);
                }
                isGrounded = false;
            }
        }
        else
        {
            em.enabled = false;
        }
        /*if (Input.GetKey(KeyCode.Space))
        {
            Vector2 upForce = new Vector2(0, upSpeed);
            rb.velocity=(upForce* Time.deltaTime);
            //transform.localEulerAngles = new Vector3(0, 0, -15);
            Jetpack.SetActive(true);
            bulletSpawnPoint.localPosition= new Vector3(0.994f, 1.535f, 0);
            em.enabled = true;
            if(gunEquiped)
            {
                anim.SetBool("GunEquipped", true);
                anim.SetBool("runWithGun", false);
            }
            else
            {
                anim.SetBool("run", false);
            }
            isGrounded = false;
        }
        else
        {
            em.enabled = false;
        }*/
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Coin")
        {
            GameManager.Instance.coins++;
            GameManager.Instance.coinsText.text = "Coins : " + GameManager.Instance.coins;
            GameManager.Instance.deathCoinsText.text = "Coins : " + GameManager.Instance.coins;
            audioSource.PlayOneShot(coin);
            Destroy(collision.gameObject);
        }
    }

    public void Death()
    {
        isDead = true;
        Jetpack.SetActive(false);
        anim.SetTrigger("Death");
        GameManager.Instance.Death();
        Time.timeScale = 0;
        Debug.Log("Dead");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Instantiate(particleHit, hitPos.position,Quaternion.identity);
        GameManager.Instance.ChangeHealthBar(MAX_HEALTH, currentHealth);
        if (currentHealth <= 0)
        {
            Death();
        }
    }
}
