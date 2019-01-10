using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovimentPlayer  : PlayerVariaveis
{
    public bool ganhandoStamina = true;

    public GameObject playerUIInstance;

    [SerializeField]
    GameObject playerUIPrefab;

    private CameraController cameraPlayer;
    Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    // Use this for initialization
    void Start()
    {
        
        Speed = WalkSpeed;
        transform.tag = "Player";
        cameraPlayer = GetComponentInChildren<CameraController>();
        controller = GetComponent<CharacterController>();

        cameraPlayer.transform.localRotation = Quaternion.identity;
        cameraPlayer.posicaoCamera(0, 0.55f, 0.245f);
    }

    // Update is called once per frame
    private void Update ()
	{
        

       // FicarComFome();
        //pega direcao da camera de frente e de lado e multiplica com inputs horizontal e vertical	
        Vector3 directFinal = Input.GetAxis ("Vertical") * cameraPlayer.direcaoFrente () + Input.GetAxis ("Horizontal") * cameraPlayer.direcaoDireita ();
		directFinal.Normalize ();
		if (controller.isGrounded) {
			//determina a direcao de movimento e multiplica com o moveSpeed

			moveDirection = new Vector3 (directFinal.x, 0, directFinal.z)*Speed;


			if (Input.GetButtonDown ("Jump")) {
				moveDirection.y = JumpForce;
			}

			//muda velocidade para correr
			if (Input.GetButton("correr")||Input.GetButtonDown("correr")) {

				Speed = RunSpeed;
                this.Stamina1 = this.Stamina1 - 5;

            }
			if(Input.GetButtonUp("correr")) {

				Speed = WalkSpeed ;
			}



		}
		//--------------agacha----------------------
		if(Input.GetButton("agachar")||Input.GetButtonDown ("agachar")){

			controller.height = 1.5f;
			Speed = 1;
		}
		if(Input.GetButtonUp ("agachar")){
			controller.height = 2f;
			Speed = WalkSpeed;
		}
		//aplica gravidade
		moveDirection.y += Physics.gravity.y * Time.deltaTime;
		//move o personagem 
		controller.Move (moveDirection * Time.deltaTime);
		//controle da camera 	
		cameraPlayer.camController ();
	}

    public static implicit operator MovimentPlayer(PlayerMovement v)
    {
        throw new NotImplementedException();
    }
}
