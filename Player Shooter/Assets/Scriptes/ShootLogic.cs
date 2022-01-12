using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLogic : MonoBehaviour
{
    PlayerController player;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float FireTime = 0.1f;
    [SerializeField] float fireTimer;
    [SerializeField] AudioClip shoot;
    AudioSource shootSound;
    
    public float fireRate = 0.25F;
    private float nextFire = 0.0F;
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        fireTimer = FireTime;
        shootSound = GetComponentInParent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!player.gunEquiped)
        {
            return;
        }
        fireTimer -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x > Screen.width / 2 && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Shoot();
            //fireTimer = FireTime;
            }
        }
    }
    public void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        shootSound.PlayOneShot(shoot);
    }
}
