using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	[SerializeField]
	private float  speedCam;
	private float rotationX,rotationY;
    public int cima, baixo;
    public GameObject obj;
    private void Awake()
    {
        obj = GameObject.Find("Main Camera");
    }
    void Start(){
		Cursor.visible = false;
        
	}

	public void posicaoCamera(float x,float y,float z){
		transform.localPosition = new Vector3 (x, y, z);
	}
	public Vector3 direcaoFrente(){
		return new Vector3 (transform.forward.x,0,transform.forward.z);
	}
	public Vector3 direcaoDireita(){
		return new Vector3 (transform.right.x,0,transform.right.z);
	}

	public void camController(){
		rotationX += Input.GetAxis ("Mouse X") * speedCam; 
		rotationX = clampAngle (rotationX,-360,360);
		rotationY += Input.GetAxis ("Mouse Y") * speedCam; 
		rotationY = clampAngle (rotationY,cima, baixo);

        Quaternion rotationFinal = Quaternion.identity * Quaternion.AngleAxis (rotationX , Vector3.up) * Quaternion.AngleAxis (rotationY , -Vector3.right); 
        //transform.localRotation = Quaternion.Lerp (transform.localRotation,rotationFinal,speedCam*Time.deltaTime); 
        transform.localRotation = rotationFinal;


        obj.transform.localRotation =  Quaternion.AngleAxis (rotationX , Vector3.up);
	}

	float clampAngle(float angle,float min, float max){
		if(angle<-360){angle += 360;}
		if(angle>360){angle -= 360;}

		return Mathf.Clamp (angle,min,max);
	}



}
