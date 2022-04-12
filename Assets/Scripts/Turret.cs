//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SocialPlatforms;

public class Turret : MonoBehaviour
{


    public float Range;

    public Transform Target;

    bool Detected = false;

    Vector2 Direction;

    public GameObject Alarmlight;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetPos = Target.position;
        
        Direction = targetPos - (Vector2)transform.position;
        
        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position,Direction,Range);
        
        if(rayInfo)
        {
            if (rayInfo.collider.gameObject.tag == "Player")
            {
                if (Detected == false)
                {
                    Detected = true;
                    Alarmlight.GetComponent<SpriteRenderer>().color = Color.red;
        
                }
            }
            else
            {
                
                if (Detected == true)
                {     
                    Detected = false;
                    Alarmlight.GetComponent<SpriteRenderer>().color = Color.green;
                }
                
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}


    
