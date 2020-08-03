using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour
{
    enum UnitState
    {
        MOVE_FORWARD = 0,
        ATTACK,
        DIE 
    }

    [SerializeField] bool playerUnit;
    [SerializeField] float moveSpeed;
    [SerializeField] int damage;
    [SerializeField] float life;
    [SerializeField] float attackRange;


    UnitState currentState;

    void Start()
    {
        currentState = UnitState.MOVE_FORWARD;
        //if (playerUnit) GetComponent<SpriteRenderer>().flipY = true;
    }

    public void OnEnable()
    {
        currentState = UnitState.MOVE_FORWARD;
        if (playerUnit) GetComponent<SpriteRenderer>().flipY = true;
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
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.right, attackRange, 1 << LayerMask.NameToLayer("Enemy"));
                    if (hit.collider)
                    { 
                        currentState = UnitState.ATTACK;
                        Debug.DrawRay(transform.position, -transform.right *  hit.distance, Color.red);
                    }
                    else
                    { 
                        currentState = UnitState.MOVE_FORWARD;
                        Debug.DrawRay(transform.position, -transform.right * attackRange, Color.green);
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
                        break;
                    case UnitState.DIE:
                        break;
                }
            }
            else 
            {
                if (life <= 0) currentState = UnitState.DIE;

                if (currentState != UnitState.DIE)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.right, attackRange, 1 << LayerMask.NameToLayer("Player"));
                    if (hit.collider)
                    {
                        currentState = UnitState.ATTACK;
                        Debug.DrawRay(transform.position, transform.right * hit.distance, Color.red);
                    }
                    else
                    {
                        currentState = UnitState.MOVE_FORWARD;
                        Debug.DrawRay(transform.position, transform.right * attackRange, Color.green);
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
                        break;
                    case UnitState.DIE:
                        break;
                }
            }
        }
    }
}
