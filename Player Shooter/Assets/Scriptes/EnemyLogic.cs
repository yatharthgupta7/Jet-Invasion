using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float stoppingDistance;
    [SerializeField] float retreatDistance;
    [SerializeField] const int MAX_HEALTH = 2;
    [SerializeField] int currentHealth;

    private float timeBtwShots;
    public float startTimeBtwShots;
    public float turnRate;
    public GameObject projectile;
    Animator anim;

    [SerializeField] Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
        anim = GetComponent<Animator>();
        currentHealth = MAX_HEALTH;
    }
    private Vector3 targetPos;
    private Vector3 thisPos;
    public float offset;

    void Update()
    {
        if(Vector2.Distance(transform.position,player.position)>stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }else if(Vector2.Distance(transform.position,player.position)<stoppingDistance
            &&Vector2.Distance(transform.position,player.position)>retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }
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

        if (timeBtwShots<=0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            anim.SetTrigger("Shoot");
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
        if(currentHealth<=0)
        {
            Death();
        }
    }    

    public void Death()
    {
        Destroy(gameObject);
    }
}
