using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLogic : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float stoppingDistance;
    [SerializeField] float retreatDistance;
    public int MAX_HEALTH = 10;
    [SerializeField] int currentHealth;
    [SerializeField] Transform[] bulletSpawnPoint;
    public Transform toPoint;

    private float timeBtwShots;
    public float startTimeBtwShots;
    public float turnRate;
    public GameObject projectile;
    Animator anim;
    private Vector3 targetPos;
    private Vector3 thisPos;
    public float offset;

    [SerializeField] Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
        anim = GetComponent<Animator>();
        currentHealth = MAX_HEALTH;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, toPoint.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, toPoint.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, toPoint.position) < stoppingDistance
           && Vector2.Distance(transform.position, toPoint.position) > retreatDistance)
        {
            //transform.position = this.transform.position;
        }/*
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }*/
        targetPos = player.position;
        thisPos = transform.position;
        targetPos.x = targetPos.x - thisPos.x;
        targetPos.y = targetPos.y - thisPos.y;
        float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 180, -(angle + offset)));

        /*Vector3 dir = player.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 270, Vector3.forward);*/
        //transform.rotation = Quaternion.EulerAngles(0, 180,0);

        //transform.RotateAround(transform.position, turnAxis, Time.deltaTime * turnRate * angleToTarget);

        if (timeBtwShots <= 0)
        {
            foreach(Transform t in bulletSpawnPoint)
            {
                Instantiate(projectile, t.position, Quaternion.identity);
            }
            //anim.SetTrigger("Shoot");
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
