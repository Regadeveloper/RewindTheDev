using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{

    public Vector2 Pos1;
    public Vector2 Pos2;
    public Vector2 Pos3;
    public Vector2 Pos4;
    public Vector2 Pos5;
    public Vector2 Pos6;
    public Vector3 pos;
    public Camera mainCamera;
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
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                default:
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
