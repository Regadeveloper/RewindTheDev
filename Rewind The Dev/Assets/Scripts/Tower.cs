using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] bool isplayer;
    [SerializeField] int life;
    [SerializeField] LifeBar bar;

    void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isplayer)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                life -= collision.gameObject.GetComponent<Bullet>().GetDamage();
                bar.ChangeValue(collision.gameObject.GetComponent<Bullet>().GetDamage());
                collision.gameObject.SetActive(false);
                if (life <= 0) TowerDestroyed();
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                life -= collision.gameObject.GetComponent<Bullet>().GetDamage();
                bar.ChangeValue(collision.gameObject.GetComponent<Bullet>().GetDamage());
                collision.gameObject.SetActive(false);
                if (life <= 0) TowerDestroyed();
            }
        }
    }

    private void TowerDestroyed()
    {

    }
}
