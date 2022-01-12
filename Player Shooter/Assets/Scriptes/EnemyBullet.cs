using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] GameObject explosion;
    Transform enemy;
    private Vector2 target;
    Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        target = new Vector2(player.position.x, player.position.y);
        transform.rotation = enemy.rotation;
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(transform.position.x==target.x&& transform.position.y==target.y)
        {
            DestroyBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(1);
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
