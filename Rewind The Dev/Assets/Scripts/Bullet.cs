using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 direction;
    float speed = 1f;
    float dmg;

    void Start()
    {

    }

    private void OnEnable()
    {

    }

    public void SetBullet(Vector3 obj,float d)
    {
        dmg = d;
        direction = obj - transform.position;
    }

    public float GetDamage() { return dmg; } 

    void Update()
    {
        if (Time.deltaTime > 0)
        {
            if (direction != null)
            {
                Vector3 move = direction * speed * Time.deltaTime;
                transform.position += move;
            }
        }
    }
}
