using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEGUITcAMERA : MonoBehaviour
{
    public Transform cam;
    public  Transform  crossHair;
    
  

    // Update is called once per frame
    void Update()
    {
        cam.LookAt(crossHair);
        
    }

}
