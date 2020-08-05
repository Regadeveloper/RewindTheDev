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

 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (Globals.level)
        {
            case 2:
                cameraMovement(Pos1);
                break;
            case 3:
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

        if (Input.GetKeyDown("l")){
            Globals.level++;
        }

        void cameraMovement(Vector2 Pos)
        {
            Vector2 pos = Pos;
            Vector2 velocity = new Vector2 (-4,0);
            int time = 2;
            transform.position = Vector2.SmoothDamp (transform.position, Pos, ref velocity, time) ; 
        }
    }
}
