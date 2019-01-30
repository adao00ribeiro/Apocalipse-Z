using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Camera camera;
    [SerializeField]
    private float  speedCam;
	private float rotationX,rotationY;
    public int cima, baixo;
    public GameObject rotacionaVertical;
    public Animator spine;
        public Transform spiner;
        public Transform eixoCamera;

    public float maxAnim, minAnim,maxCROS,minCROSS;
    void Start(){
        cursor(false);


    }
    public void cursor(bool ativa) {

        Cursor.visible = ativa;
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
		rotationY = clampAngle (rotationY,baixo, cima);

        //   Quaternion rotationFinal = Quaternion.identity * Quaternion.AngleAxis (rotationX , Vector3.up) * Quaternion.AngleAxis (rotationY , -Vector3.right); 
        //transform.localRotation = Quaternion.Lerp (transform.localRotation, Quaternion.AngleAxis(rotationY-10   , -Vector3.right), speedCam*Time.deltaTime); 


        if (rotationY > 0)
        {
            spine.SetFloat("angulo", rotationY);
            transform.localRotation = Quaternion.AngleAxis(rotationY, -Vector3.right);
  
            if (rotationY > maxAnim) {
                spine.SetFloat("angulo", maxAnim);
            }
            if (rotationY > maxCROS)
            {
                transform.localRotation = Quaternion.AngleAxis(maxCROS, -Vector3.right);
            }



        }
        else if(rotationY<0){
            spine.SetFloat("angulo", rotationY);
            transform.localRotation = Quaternion.AngleAxis(rotationY, -Vector3.right);
            if (rotationY < minAnim)
            {
                spine.SetFloat("angulo", minAnim);
            }
            if (rotationY < minCROSS)
            {
                transform.localRotation = Quaternion.AngleAxis(minCROSS, -Vector3.right);
            }



        }

     
        eixoCamera.transform.rotation = spiner.transform.rotation;
        // eixoCamera.transform.position = spiner.transform.position;    

        //  cross.LookHeight(rotationY);
        rotacionaVertical.transform.localRotation =  Quaternion.AngleAxis (rotationX , Vector3.up);
	}

	float clampAngle(float angle,float min, float max){
		if(angle<-360){angle += 360;}
		if(angle>360){angle -= 360;}

		return Mathf.Clamp (angle,min,max);
	}
    public float checkeangle(float value) {

        float angle = value - 180;
        if (angle>0) {
            return angle - 180;
        }
        return angle + 180;

    }



}
