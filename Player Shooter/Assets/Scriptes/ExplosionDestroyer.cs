using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroyer : MonoBehaviour
{
    float time;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time>=3.0f)
        {
            Destroy(gameObject);
        }
    }
}
