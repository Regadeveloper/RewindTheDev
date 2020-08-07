using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 direction;
    float speed = 1f;
    int dmg;

    void Start()
    {

    }

    private void OnEnable()
    {

    }

    public void SetBullet(Vector3 obj,int d,bool visible)
    {
        dmg = d;
        direction = obj - transform.position;
        if (visible) GetComponent<SpriteRenderer>().enabled = true;
        else GetComponent<SpriteRenderer>().enabled = false;
    }

    public int GetDamage() { return dmg; } 

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
