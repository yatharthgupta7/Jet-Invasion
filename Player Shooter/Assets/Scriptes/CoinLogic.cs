using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLogic : MonoBehaviour
{
   public float moveSpeed = 10;
    PlayerController player;
    Rigidbody2D rb;
    Vector2 screenBounds;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = new Vector2(-moveSpeed, 0) * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, new Vector3(-30, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);
    }
}
