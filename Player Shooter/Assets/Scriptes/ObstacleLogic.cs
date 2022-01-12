using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLogic: MonoBehaviour
{
    public float moveSpeed = 10;
    int currentHealth = 2;
    PlayerController player;
    Rigidbody2D rb;
    Vector2 screenBounds;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = new Vector2(-moveSpeed*Time.deltaTime , 0);

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        //rb.velocity = new Vector2(-moveSpeed * Time.deltaTime, 0);
        transform.position = Vector2.MoveTowards(transform.position, new Vector3(-30, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        if (transform.position.x <=-30)
        {
            Destroy(this.gameObject);
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            player.TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth<=0)
        {
            Destroy(gameObject);
        }
    }
}
