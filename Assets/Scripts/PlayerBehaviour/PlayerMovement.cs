using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerMovement  :NetworkBehaviour
{
    public bool teste;
    [Header("SETUP")]


    [SerializeField]
    MonoBehaviour[] componentesDesativados;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    [SerializeField]
    string dontDrawLayerName = "DontDraw";

    [SerializeField]
    GameObject playerGraphics;

    [SerializeField]
    GameObject playerUIPrefab;

    [HideInInspector]
    public GameObject playerUIInstance;

    [Space(3)]
    [Header("VARIAVEISPLAYER")]
    public int vida;
    public float speed;
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
   
    private CameraController cameraPlayer;
    Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    // Use this for initialization
    void Start ()
	{
		speed = walkSpeed;
		transform.tag = "Player";
		cameraPlayer = GetComponentInChildren<CameraController> ();
		controller = GetComponent<CharacterController> ();

        //seta a rotacao da camera
		cameraPlayer.transform.localRotation = Quaternion.identity;
        //posiciona camera na posicao 
        cameraPlayer.posicaoCamera ( 0, 0.6479495f, 0.005414106f);

        //verifica  se nao for o playerLocal desativa componentes e troca a layer
        if (!isLocalPlayer)
        {
            DesativaComponents();
            AtribuiRemoteLayer();
            return;
        }
        //se for o player esconde a mesh do player e cria a ui do player
        else
        {
            // Disable player graphics for local player
            SetLayerRecursively(playerGraphics, LayerMask.NameToLayer(dontDrawLayerName));
            // Create PlayerUI
            playerUIInstance = Instantiate(playerUIPrefab);
            playerUIInstance.name = playerUIPrefab.name;


        }
    }
   
	// Update is called once per frame
	private void Update ()
	{
        if (teste) {
            recebeDano(50);
            teste = false;
        }
        if (!isLocalPlayer)
        {
            return;
        }
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



    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }


    void AtribuiRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void DesativaComponents()
    {
        for (int i = 0; i < componentesDesativados.Length; i++)
        {
            componentesDesativados[i].enabled = false;
        }
    }
    public void recebeDano(int dano)
    {

        playerUIInstance.GetComponent<PlayerUI>().atualizaBarraVida(20);
    }

}
