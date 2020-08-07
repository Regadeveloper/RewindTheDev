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
    [SerializeField] int life;
    [SerializeField] float attackRange;
    [SerializeField] float attackCD;
    public UnitType type;

    Vector3 atkPos;
    UnitState currentState;
    float attackCount;
    Animator anim;
    bool ground = false;

    void Start()
    {
        currentState = UnitState.MOVE_FORWARD;
        anim = GetComponent<Animator>();
        attackCount = 0;
        life += Globals.extraDefense;
        damage += Globals.extraDamage;
        if (playerUnit)
        {
            transform.Rotate(0, 180, 0);
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
                Vector3 airPos = transform.position;
                RaycastHit2D hitGround = Physics2D.Raycast(transform.position, transform.right, attackRange, 1 << LayerMask.NameToLayer("Enemy"));
                RaycastHit2D hitAir;
                Debug.Log(currentState.ToString());
                if (currentState != UnitState.DIE)
                {
                    if (attackCount < attackCD) attackCount += Time.deltaTime;

                    switch (type)
                    {
                        case UnitType.SOLDIER:
                        case UnitType.TANK:
                            if (hitGround.collider)
                            {
                                currentState = UnitState.ATTACK;
                                atkPos = hitGround.point;
                            }
                            else currentState = UnitState.MOVE_FORWARD;
                                
                            break;
                        case UnitType.PLANE:
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
                                }
                                else atkPos = hitGround.point;
                                    
                            }
                            else if (hitAir.collider)
                            {
                                currentState = UnitState.ATTACK;
                                atkPos = hitAir.point;
                            }
                            else currentState = UnitState.MOVE_FORWARD;
                               
                            break;
                        case UnitType.ARCHER:
                            airPos.x += 1;
                            airPos.y = 0;
                            hitAir = Physics2D.Raycast(airPos, transform.right, attackRange, 1 << LayerMask.NameToLayer("Enemy"));
                            if (hitGround.collider)
                            {
                                currentState = UnitState.ATTACK;
                                if (hitAir.collider)
                                {
                                    if (hitAir.distance < hitGround.distance)
                                    {
                                        atkPos = hitAir.point;
                                        ground = false;
                                    }
                                    else
                                    {
                                        atkPos = hitGround.point;
                                        ground = true;
                                    }
                                }
                                else
                                {
                                    atkPos = hitGround.point;
                                    ground = true;
                                }                                                                                                                                                            
                            }
                            else if (hitAir.collider)
                            {
                                currentState = UnitState.ATTACK;
                                atkPos = hitAir.point;
                                ground = false;
                            }
                            else  currentState = UnitState.MOVE_FORWARD;                                                  
                            break;
                    }                                  
                }

                switch (currentState)
                {
                    case UnitState.MOVE_FORWARD:
                        Vector3 move = Vector3.zero;
                        move.x = -moveSpeed * Time.deltaTime;
                        transform.position += move;
                        switch (type)
                        {
                            case UnitType.TANK:
                                anim.Play("TankPlayerWalk");
                                break;
                            case UnitType.SOLDIER:
                                anim.Play("WarriorPlayerWalk");
                                break;
                            case UnitType.PLANE:
                                anim.Play("Fly2Player");
                                break;
                            case UnitType.ARCHER:
                                anim.SetBool("Walk",true);
                                break;
                        }
                        break;
                    case UnitState.ATTACK:
                        if (attackCount >= attackCD)
                        {
                            GameObject bullet = null;
                            switch (type)
                            {
                                case UnitType.TANK:
                                    bullet = ObjectPooling.instance.SpawnFromPool("FlyBullet", transform.position, Quaternion.identity);
                                    bullet.GetComponent<Bullet>().SetBullet(atkPos, damage, false);
                                    anim.Play("TankPlayerAttack");
                                    break;
                                case UnitType.SOLDIER:
                                    bullet = ObjectPooling.instance.SpawnFromPool("FlyBullet", transform.position, Quaternion.identity);                                    
                                    bullet.GetComponent<Bullet>().SetBullet(atkPos, damage, false);
                                    anim.Play("WarriorPlayerAttack");
                                    break;
                                case UnitType.PLANE:
                                    bullet = ObjectPooling.instance.SpawnFromPool("FlyBullet", transform.position, Quaternion.identity);
                                    bullet.GetComponent<Bullet>().SetBullet(atkPos, damage,true);
                                    anim.Play("Fly2PlayerAttack");
                                    break;
                                case UnitType.ARCHER:
                                    bullet = ObjectPooling.instance.SpawnFromPool("ArcherBullet", transform.position, Quaternion.identity);
                                    bullet.GetComponent<Bullet>().SetBullet(atkPos, damage,true);
                                    if (ground) anim.SetTrigger("AttackGround");
                                    else anim.SetTrigger("AttackAir");
                                    break;
                            }
                                
                            bullet.tag = "PlayerBullet";                        
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
                Vector3 airPos = transform.position;
                RaycastHit2D hitGround = Physics2D.Raycast(transform.position, transform.right, attackRange, 1 << LayerMask.NameToLayer("Player"));
                RaycastHit2D hitAir;
               

                if (currentState != UnitState.DIE)
                {                 
                    if (attackCount < attackCD) attackCount += Time.deltaTime;

                    switch (type)
                    {
                        case UnitType.SOLDIER:
                        case UnitType.TANK:
                            if (hitGround.collider)
                            {
                                currentState = UnitState.ATTACK;
                                atkPos = hitGround.point;
                            }
                            else currentState = UnitState.MOVE_FORWARD;
                               
                            break;
                        case UnitType.PLANE:                        
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
                                }
                                else atkPos = hitGround.point;
                                    
                            }
                            else if (hitAir.collider)
                            {
                                currentState = UnitState.ATTACK;
                                atkPos = hitAir.point;
                            }
                            else currentState = UnitState.MOVE_FORWARD;
                                
                            break;
                        case UnitType.ARCHER:                      
                            airPos.x += 1;
                            airPos.y = 0;
                            hitAir = Physics2D.Raycast(airPos, transform.right, attackRange, 1 << LayerMask.NameToLayer("Player"));
                            if (hitGround.collider)
                            {
                                currentState = UnitState.ATTACK;
                                if (hitAir.collider)
                                {
                                    if (hitAir.distance < hitGround.distance)
                                    {
                                        atkPos = hitAir.point;
                                        ground = false;
                                    }
                                    else
                                    {
                                        atkPos = hitGround.point;
                                        ground = true;
                                    }
                                }
                                else
                                {
                                    atkPos = hitGround.point;
                                    ground = true;
                                }
                            }
                            else if (hitAir.collider)
                            {
                                currentState = UnitState.ATTACK;
                                atkPos = hitAir.point;
                                ground = false;
                            }
                            else currentState = UnitState.MOVE_FORWARD;
                               
                            break;
                    }
                }

                switch (currentState)
                {
                    case UnitState.MOVE_FORWARD:
                        Vector3 move = Vector3.zero;
                        move.x = moveSpeed * Time.deltaTime;
                        transform.position += move;
                        switch (type)
                        {
                            case UnitType.TANK:
                                break;
                            case UnitType.SOLDIER:
                                break;
                            case UnitType.PLANE:
                                break;
                            case UnitType.ARCHER:
                                break;
                        }
                        break;
                    case UnitState.ATTACK:
                        if (attackCount >= attackCD)
                        {
                            GameObject bullet = null;
                            switch (type)
                            {
                                case UnitType.TANK:
                                case UnitType.SOLDIER:
                                    bullet = ObjectPooling.instance.SpawnFromPool("FlyBullet", transform.position, Quaternion.identity);
                                    bullet.GetComponent<Bullet>().SetBullet(atkPos, damage, false);
                                    break;
                                case UnitType.PLANE:
                                    bullet = ObjectPooling.instance.SpawnFromPool("FlyBullet", transform.position, Quaternion.identity);
                                    bullet.GetComponent<Bullet>().SetBullet(atkPos, damage, true);
                                    break;
                                case UnitType.ARCHER:
                                    bullet = ObjectPooling.instance.SpawnFromPool("ArcherBullet", transform.position, Quaternion.identity);
                                    bullet.GetComponent<Bullet>().SetBullet(atkPos, damage, true);
                                    break;
                            }
                            bullet.tag = "EnemyBullet";                           
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