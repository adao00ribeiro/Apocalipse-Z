using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement  :PlayerVariaveis          
{
    public Transform packArmas;
    public GameObject ArmaEquipada;
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


    private bool ganhandostamina = true, on = true, fomeon = true, sedeon = true;
    private CameraController cameraPlayer;
    Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    // Use this for initialization
    void Start ()
	{
<<<<<<< HEAD
        this.Speed = this.WalkSpeed;
=======
        foreach (Transform arma in packArmas) {
            arma.gameObject.SetActive(true);
            ArmaEquipada = arma.gameObject;
        }
		this.Speed = this.WalkSpeed;
>>>>>>> 591b1421872d37dce921c66762e728477f716c54
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
            playerUIInstance.GetComponent<PlayerUI>().setPlayer(GetComponent<PlayerMovement>());

        }
    }
   
	// Update is called once per frame
	private void Update ()
	{
    
        if (!isLocalPlayer)
        {
            return;
        }
        //pega direcao da camera de frente e de lado e multiplica com inputs horizontal e vertical	
        Vector3 directFinal = Input.GetAxis ("Vertical") * cameraPlayer.direcaoFrente () + Input.GetAxis ("Horizontal") * cameraPlayer.direcaoDireita ();
		directFinal.Normalize ();
		if (controller.isGrounded) {
			//determina a direcao de movimento e multiplica com o moveSpeed

			moveDirection = new Vector3 (directFinal.x, 0, directFinal.z)* this.Speed;
      


			if (Input.GetButtonDown ("Jump")) {
                ganhandostamina = false;
                moveDirection.y = this.JumpForce;
                this.Stamina1 = this.Stamina1 - 30;
            }
            else
            {
                ganhandostamina = true;
            }

			//muda velocidade para correr
			if (Input.GetButton("correr")||Input.GetButtonDown("correr") && this.Stamina1 <= 1000){
                ganhandostamina = false;
                this.Speed = this.RunSpeed;
                this.Stamina1 = this.Stamina1 - 3;
			}
            else
            {
                ganhandostamina = true;
            }

			if(Input.GetButtonUp("correr")) {
                this.Speed = this.WalkSpeed ;
            }
            if (Input.GetKey(KeyCode.W) && this.Stamina1 <= 0)
            {
                ganhandostamina = true;
                on = true;
            }

        //----------------Seta Stamina---------------
            if(ganhandostamina == true && on == true)
            {
                StartCoroutine("loopGanhoEstamina");
            }
            if(this.Stamina1 >= 1000)
            {
                this.Stamina1 = 1000;
            }       
            if (this.Stamina1 <= 0)
            {
                this.Stamina1 = 0;
            }
            if (this.Stamina1 <= 10)
            {
                Speed = WalkSpeed;
                JumpForce = 0;
            }
            else
            {
                JumpForce = 5;
            }
        }

        //----------------seta fome------------

        if(fomeon == true)
        {
             this.Fome1 -= 5;
            StartCoroutine("FicandoComFome");
        }
        if(this.Fome1 >= 1000)
        {
            this.Fome1 = 1000;
        }
        if(this.Fome1 <= 0)
        {
            this.Fome1 = 0;
        }

        //---------------seta sede------------------

        if(sedeon == true)
        {
            this.Sede1 -= 5;
            StartCoroutine("FicandoComSede");
        }
        if(this.Sede1 >=1000)
        {
            this.Sede1 = 1000;
        }
        if(this.Sede1 <=0)
        {
            this.Sede1 = 0;
        }

		//--------------agacha----------------------
		if(Input.GetButton("agachar")||Input.GetButtonDown ("agachar")){

			controller.height = 1.5f;
            this.Speed = 1;
		}
		if(Input.GetButtonUp ("agachar")){
			controller.height = 2f;
            this.Speed = this.WalkSpeed;
        
         }
        //--------------LANTERNA----------------------
        if (Input.GetButtonUp("lanterna"))
        {
            print("ativa lanterna");

        }
        //--------------MAPA----------------------
        if (Input.GetButtonUp("mapa"))
        {

            print("ABRE O MAPA");
        }
        //--------------INTERAGI----------------------
        if (Input.GetButtonUp("interacao"))
        {
            print("INTERAGE COM OBJETOS");

        }
        //--------------INVENTARIO----------------------
        if (Input.GetButtonUp("inventario"))
        {

            print("ABRE O INVENTARIO");
        }
        if (Input.GetButtonUp("visaonoturna"))
        {
            print("ATIVA VISAO NOTURNA");

        }
        if (Input.GetMouseButton(0))
        {
            if (ArmaEquipada != null) {
                ArmaEquipada.GetComponent<Arma_Generica>().Atirar();
            }
           

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
        this.Vida = this.Vida - dano;
    }

    IEnumerator loopGanhoEstamina()
    {
        on = false;
        this.Stamina1 += 7;
        yield return new WaitForSeconds(0.1f);
        on = true;
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator FicandoComFome()
    {
        fomeon = false;
        yield return new WaitForSeconds(90);
        fomeon = true;
    }

    IEnumerator FicandoComSede()
    {
        sedeon = false;
        yield return new WaitForSeconds(60);
        sedeon = true;
    }
}
