using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class UnitBehaviour : MonoBehaviour
{
    enum UnitState
    {
        MOVE_FORWARD = 0,
        ATTACK,
        DIE 
    }

    public bool playerUnit;
    [SerializeField] float moveSpeed;
    [SerializeField] int damage;
    [SerializeField] float life;
    [SerializeField] float attackRange;
    [SerializeField] float attackCD;
    public UnitType type;

    Vector3 atkPos;
    UnitState currentState;
    [SerializeField]float attackCount;

    void Start()
    {
        currentState = UnitState.MOVE_FORWARD;
        //if (playerUnit) GetComponent<SpriteRenderer>().flipY = true;
    }

    public void OnEnable()
    {
        currentState = UnitState.MOVE_FORWARD;
        attackCount = 0;
        if (playerUnit)
        {
            transform.Rotate(0,180,0);
            gameObject.layer = 9;
        }
        else 
        {
            gameObject.layer = 8;
        }
    }

    void Update()
    {
        if (Time.deltaTime > 0)
        {
            if (playerUnit)
            {
                if (life <= 0) currentState = UnitState.DIE;

                if (currentState != UnitState.DIE)
                {
                    RaycastHit2D hitGround;
                    RaycastHit2D hitAir;
                    Vector3 airPos = transform.position;
                    if (attackCount < attackCD) attackCount += Time.deltaTime;

                    switch (type)
                    {
                        case UnitType.SOLDIER:
                        case UnitType.TANK:
                            hitGround = Physics2D.Raycast(transform.position, transform.right, attackRange, 1 << LayerMask.NameToLayer("Enemy"));
                            if (hitGround.collider)
                            {
                                currentState = UnitState.ATTACK;
                                atkPos = hitGround.point;
                                //Debug.DrawRay(transform.position, transform.right * hitGround.distance, Color.red);
                            }
                            else
                            {
                                currentState = UnitState.MOVE_FORWARD;
                                //Debug.DrawRay(transform.position, transform.right * attackRange, Color.green);
                            }
                            break;
                        case UnitType.PLANE:
                            hitGround = Physics2D.Raycast(transform.position, transform.right, attackRange, 1 << LayerMask.NameToLayer("Enemy"));
                            airPos.x += 1;
                            airPos.y -= 3.5f;
                            hitAir = Physics2D.Raycast(airPos, transform.right, attackRange, 1 << LayerMask.NameToLayer("Enemy"));

                            if (hitGround.collider)
                            {
                                currentState = UnitState.ATTACK;
                                if (hitAir.collider)
                                {
                                    if (hitAir.distance < hitGround.distance) atkPos = hitAir.point;
                                    else atkPos = hitGround.point;

                                    //Debug.DrawRay(transform.position, transform.right * hitGround.distance, Color.red);
                                    //Debug.DrawRay(airPos, transform.right * hitAir.distance, Color.red);
                                }
                                else
                                {
                                    atkPos = hitGround.point;
                                    //Debug.DrawRay(transform.position, transform.right * hitGround.distance, Color.red);
                                }
                            }
                            else if (hitAir.collider)
                            {
                                currentState = UnitState.ATTACK;
                                atkPos = hitAir.point;
                                //Debug.DrawRay(airPos, transform.right * hitAir.distance, Color.red);
                            }
                            else
                            {
                                currentState = UnitState.MOVE_FORWARD;
                                //Debug.DrawRay(transform.position, transform.right * attackRange, Color.green);
                                //Debug.DrawRay(airPos, transform.right * attackRange, Color.green);
                            }
                            break;
                        case UnitType.ARCHER:

                            hitGround = Physics2D.Raycast(transform.position, transform.right, attackRange, 1 << LayerMask.NameToLayer("Enemy"));                            
                            airPos.x += 1;
                            airPos.y = 0;
                            hitAir = Physics2D.Raycast(airPos, transform.right, attackRange, 1 << LayerMask.NameToLayer("Enemy"));

                            if (hitGround.collider)
                            {
                                currentState = UnitState.ATTACK;
                                if (hitAir.collider)
                                {
                                    if (hitAir.distance < hitGround.distance) atkPos = hitAir.point;
                                    else atkPos = hitGround.point;

                                    //Debug.DrawRay(transform.position, transform.right * hitGround.distance, Color.red);
                                    //Debug.DrawRay(airPos, -transform.right * hitAir.distance, Color.red);
                                }
                                else 
                                {
                                    atkPos = hitGround.point;
                                    //Debug.DrawRay(transform.position, transform.right * hitGround.distance, Color.red);
                                }                                                                                                                            
                            }
                            else if (hitAir.collider)
                            {
                                currentState = UnitState.ATTACK;
                                atkPos = hitAir.point;
                                //Debug.DrawRay(airPos, transform.right * hitAir.distance, Color.red);
                            }
                            else
                            {
                                currentState = UnitState.MOVE_FORWARD;
                                //Debug.DrawRay(transform.position, transform.right * attackRange, Color.green);
                                //Debug.DrawRay(airPos, transform.right * attackRange, Color.green);
                            }                          
                            break;
                    }                                  
                }

                switch (currentState)
                {
                    case UnitState.MOVE_FORWARD:
                        Vector3 move = Vector3.zero;
                        move.x = -moveSpeed * Time.deltaTime;
                        transform.position += move;
                        break;
                    case UnitState.ATTACK:
                        if (attackCount >= attackCD)
                        {
                            GameObject bullet = ObjectPooling.instance.SpawnFromPool("Bullets", transform.position, Quaternion.identity);
                            if (bullet != null)
                            {
                                bullet.GetComponent<Bullet>().SetBullet(atkPos, damage);
                                bullet.tag = "PlayerBullet";
                            }
                            attackCount = 0;
                        }
                        break;
                    case UnitState.DIE:
                        gameObject.SetActive(false);
                        break;
                }
            }
            else 
            {
                if (life <= 0) currentState = UnitState.DIE;

                if (currentState != UnitState.DIE)
                {
                    RaycastHit2D hitGround;
                    RaycastHit2D hitAir;
                    Vector3 airPos = transform.position;
                    if (attackCount < attackCD) attackCount += Time.deltaTime;

                    switch (type)
                    {
                        case UnitType.SOLDIER:
                        case UnitType.TANK:
                            hitGround = Physics2D.Raycast(transform.position, transform.right, attackRange, 1 << LayerMask.NameToLayer("Player"));
                            if (hitGround.collider)
                            {
                                currentState = UnitState.ATTACK;
                                atkPos = hitGround.point;
                                //Debug.DrawRay(transform.position, transform.right * hitGround.distance, Color.red);
                            }
                            else
                            {
                                currentState = UnitState.MOVE_FORWARD;
                                //Debug.DrawRay(transform.position, transform.right * attackRange, Color.green);
                            }
                            break;
                        case UnitType.PLANE:
                            hitGround = Physics2D.Raycast(transform.position, transform.right, attackRange, 1 << LayerMask.NameToLayer("Player"));
                            airPos.x += 1;
                            airPos.y -= 3.5f;
                            hitAir = Physics2D.Raycast(airPos, transform.right, attackRange, 1 << LayerMask.NameToLayer("Player"));

                            if (hitGround.collider)
                            {
                                currentState = UnitState.ATTACK;
                                if (hitAir.collider)
                                {
                                    if (hitAir.distance < hitGround.distance) atkPos = hitAir.point;
                                    else atkPos = hitGround.point;

                                    //Debug.DrawRay(transform.position, transform.right * hitGround.distance, Color.red);
                                    //Debug.DrawRay(airPos, transform.right * hitAir.distance, Color.red);
                                }
                                else
                                {
                                    atkPos = hitGround.point;
                                    //Debug.DrawRay(transform.position, transform.right * hitGround.distance, Color.red);
                                }
                            }
                            else if (hitAir.collider)
                            {
                                currentState = UnitState.ATTACK;
                                atkPos = hitAir.point;
                                //Debug.DrawRay(airPos, transform.right * hitAir.distance, Color.red);
                            }
                            else
                            {
                                currentState = UnitState.MOVE_FORWARD;
                                //Debug.DrawRay(transform.position, transform.right * attackRange, Color.green);
                                //Debug.DrawRay(airPos, transform.right * attackRange, Color.green);
                            }
                            break;
                        case UnitType.ARCHER:

                            hitGround = Physics2D.Raycast(transform.position, transform.right, attackRange, 1 << LayerMask.NameToLayer("Player"));                         
                            airPos.x += 1;
                            airPos.y = 0;
                            hitAir = Physics2D.Raycast(airPos, transform.right, attackRange, 1 << LayerMask.NameToLayer("Player"));

                            if (hitGround.collider)
                            {
                                currentState = UnitState.ATTACK;
                                if (hitAir.collider)
                                {
                                    if (hitAir.distance < hitGround.distance) atkPos = hitAir.point;
                                    else atkPos = hitGround.point;

                                    //Debug.DrawRay(transform.position, transform.right * hitGround.distance, Color.red);
                                    //Debug.DrawRay(airPos, transform.right * hitAir.distance, Color.red);
                                }
                                else
                                {
                                    atkPos = hitGround.point;
                                    //Debug.DrawRay(transform.position, transform.right * hitGround.distance, Color.red);
                                }
                            }
                            else if (hitAir.collider)
                            {
                                currentState = UnitState.ATTACK;
                                atkPos = hitAir.point;
                                //Debug.DrawRay(airPos, transform.right * hitAir.distance, Color.red);
                            }
                            else
                            {
                                currentState = UnitState.MOVE_FORWARD;
                                //Debug.DrawRay(transform.position, transform.right * attackRange, Color.green);
                                //Debug.DrawRay(airPos, transform.right * attackRange, Color.green);
                            }
                            break;
                    }
                }

                switch (currentState)
                {
                    case UnitState.MOVE_FORWARD:
                        Vector3 move = Vector3.zero;
                        move.x = moveSpeed * Time.deltaTime;
                        transform.position += move;
                        break;
                    case UnitState.ATTACK:
                        if (attackCount >= attackCD)
                        {
                            GameObject bullet = ObjectPooling.instance.SpawnFromPool("Bullets", transform.position, Quaternion.identity);
                            if (bullet != null)
                            {
                                bullet.GetComponent<Bullet>().SetBullet(atkPos, damage);
                                bullet.tag = "EnemyBullet";
                            }
                            attackCount = 0;
                        }
                        break;
                    case UnitState.DIE:
                        gameObject.SetActive(false);
                        break;
                }
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerUnit)
        {
            if (collision.gameObject.CompareTag("EnemyBullet"))
            {
                life -= collision.gameObject.GetComponent<Bullet>().GetDamage();
                collision.gameObject.SetActive(false);
                
            }
        }
        else 
        {
            if (collision.gameObject.CompareTag("PlayerBullet"))
            {
                life -= collision.gameObject.GetComponent<Bullet>().GetDamage();
                collision.gameObject.SetActive(false);
            }
        }
    }
}
