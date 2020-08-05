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
    public Camera mainCamera;
    int oldlevel;
    int newlevel;

 
    
    // Start is called before the first frame update
    void Start()
    {
        oldlevel = Globals.level;
        newlevel = oldlevel;
    }

    // Update is called once per frame
    void Update()
    {
        if (newlevel > oldlevel)
        {
            switch (Globals.level)
            {
                case 2:
                    oldlevel = newlevel;
                    cameraMovement(Pos1);
                    break;
                case 3:
                    oldlevel = newlevel;
                    cameraMovement(Pos2);
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

        if (Input.GetKeyDown("l")){
            Globals.level++;
            newlevel++;
        }

        void cameraMovement(Vector2 Pos)
        {
            Vector2 pos = Pos;
            float velocity = -2;
            int time = 3;
            transform.position = pos;
        }
        
    }
}
