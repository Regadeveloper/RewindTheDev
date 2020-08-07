using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] Vector2 PosInicial;
    [SerializeField] Vector2 Pos1;
    [SerializeField] Vector2 Pos2;
    [SerializeField] Vector2 Pos3;
    [SerializeField] Vector2 Pos4;
    [SerializeField] Vector2 Pos5;
    [SerializeField] Vector2 Pos6;
    Vector3 pos;
    [SerializeField] Camera mainCamera;
    int oldlevel;
    int newlevel;

 
    
    // Start is called before the first frame update
    void Start()
    {
        oldlevel = Globals.level;
        newlevel = oldlevel;
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown("l")){
            Globals.level++;
            newlevel++;
        }

        if (newlevel > oldlevel)
        {
            switch (Globals.level)
            {
                case 2:

                    pos = Pos1;
                    oldlevel = newlevel;
                    break;
                case 3:
                    pos = Pos2;
                    oldlevel = newlevel;
                    break;
                case 4:
                    pos = Pos3;
                    oldlevel = newlevel;
                    break;
                case 5:
                    pos = Pos4;
                    oldlevel = newlevel;
                    break;
                case 6:
                    pos = Pos5;
                    oldlevel = newlevel;
                    break;
                case 7:
                    pos = Pos6;
                    oldlevel = newlevel;
                    break;
                default:
                    pos = PosInicial;
                    oldlevel = 0;
                    newlevel = 0; 
                    break;


            }

        }



    }


    public void LateUpdate()
    {
        
        float velocity = 0.004f;
        transform.position = Vector3.Lerp(transform.position, pos, velocity);
    }
}
