using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLogic : MonoBehaviour
{

    [SerializeField] Transform[] t;
    [SerializeField] float speed;
    [SerializeField] float difficulty;
    void Start()
    {
        
    }
    void Update()
    {
        t[1].Translate(-speed * Time.deltaTime, 0, 0);
        //Time.timeScale += Time.deltaTime * difficulty * 0.01f;
        if(t[1].transform.position.x<-60)
        {
            t[1].transform.position = new Vector3(35, 0, 20f);
        }
        t[0].Translate(-speed * Time.deltaTime, 0, 0);
        //Time.timeScale += Time.deltaTime * difficulty * 0.01f;
        if (t[0].transform.position.x < -80)
        {
            t[0].transform.position = new Vector3(0, 0, 50f);
        }
    }

    public void SetSpeed(float multiplier)
    {
        speed *= multiplier;
    }
}
