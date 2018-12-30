using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovimentPlayer  : MonoBehaviour
{

    private CameraController cameraPlayer;
    Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    public float speed, walkSpeed, runSpeed, jumpForce;

    // Use this for initialization
    void Start()
    {
        speed = walkSpeed;
        transform.tag = "Player";
        cameraPlayer = GetComponentInChildren<CameraController>();
        controller = GetComponent<CharacterController>();

        cameraPlayer.transform.localRotation = Quaternion.identity;
        cameraPlayer.posicaoCamera(0, 0.55f, 0.245f);
    }

    // Update is called once per frame
    private void Update ()
	{
		//pega direcao da camera de frente e de lado e multiplica com inputs horizontal e vertical	
		Vector3 directFinal = Input.GetAxis ("Vertical") * cameraPlayer.direcaoFrente () + Input.GetAxis ("Horizontal") * cameraPlayer.direcaoDireita ();
		directFinal.Normalize ();
		if (controller.isGrounded) {
			//determina a direcao de movimento e multiplica com o moveSpeed

			moveDirection = new Vector3 (directFinal.x, 0, directFinal.z)*speed;


			if (Input.GetButtonDown ("Jump")) {
				moveDirection.y = jumpForce;
			}

			//muda velocidade para correr
			if (Input.GetButton("correr")||Input.GetButtonDown("correr")) {

				speed = runSpeed;
			}
			if(Input.GetButtonUp("correr")) {

				speed = walkSpeed ;
			}



		}
		//--------------agacha----------------------
		if(Input.GetButton("agachar")||Input.GetButtonDown ("agachar")){

			controller.height = 1.5f;
			speed = 1;
		}
		if(Input.GetButtonUp ("agachar")){
			controller.height = 2f;
			speed = walkSpeed;
		}
		//aplica gravidade
		moveDirection.y += Physics.gravity.y * Time.deltaTime;
		//move o personagem 
		controller.Move (moveDirection * Time.deltaTime);
		//controle da camera 	
		cameraPlayer.camController ();
	}



}
