using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject explosion;
    Rigidbody2D rb;
    float timer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Enemy")
        {
            //Destroy enemy
            Instantiate(explosion, transform.position, transform.rotation);
            collision.gameObject.GetComponent<EnemyLogic>().TakeDamage(1);
            Destroy(gameObject);
        }
        if(collision.gameObject.tag=="Obstacle")
        {
            collision.gameObject.GetComponent<ObstacleLogic>().TakeDamage(1);
            Instantiate(explosion, transform.position, transform.rotation);
        }
        if(collision.gameObject.tag=="Boss")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            collision.gameObject.GetComponent<BossLogic>().TakeDamage(1);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5.0f)
        {
            Destroy(gameObject);
        }

    }
}
