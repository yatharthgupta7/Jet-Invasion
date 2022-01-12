using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectHandler : MonoBehaviour
{
    float timer = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>=3.0f)
        {
            Destroy(gameObject);
        }
    }
}
