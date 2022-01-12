using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLogic : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    public bool gunEquiped;
    PlayerController player;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-moveSpeed, 0) * Time.deltaTime;
        //screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            gunEquiped = true;
            player.anim.SetBool("GunEquipped", true);
            player.gunEquiped = true;
            Destroy(gameObject);
        }
    }
}
