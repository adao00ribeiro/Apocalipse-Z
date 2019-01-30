using Snake.ApocalipseZ.Enemys;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement  :PlayerVariaveis          
{
    public Transform inventario;
    public float sphereRadius;
    public float maxDistance;
    public LayerMask layerMask;

    private Vector3 origin;
    private Vector3 direction;

    private float currenthitdistance;
    
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

    public  Animator animator;
    public GameObject objetoInteracao;
    public Transform pontoHand; 
    public CameraController cameraPlayer;
    Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    // Use this for initialization
    void Start ()
	{
      
        InvokeRepeating("FicandoComFome", 1, 1);
        InvokeRepeating("FicandoComSede", 1, 1);
        InvokeRepeating("RestaurandoStamina", 0.3f, 0.3f);


        this.Speed = this.WalkSpeed;

       
        transform.tag = "Player";

		controller = GetComponent<CharacterController> ();

        //seta a rotacao da camera
		cameraPlayer.transform.localRotation = Quaternion.identity;
        //posiciona camera na posicao 
        // cameraPlayer.posicaoCamera ( 0, 0.6479495f, 0.005414106f);

        //verifica  se nao for o playerLocal desativa componentes e troca a layer
        if (!isLocalPlayer)
        {
            DesativaComponents();
            AtribuiRemoteLayer();
            return;
        }
        //se for o player esconde a mesh do player e cria a ui do play//       else
        {
            // Disable player graphics for local player
            SetLayerRecursively(playerGraphics, LayerMask.NameToLayer(dontDrawLayerName));
            // Create PlayerUI
            playerUIInstance = Instantiate(playerUIPrefab);
            playerUIInstance.name = playerUIPrefab.name;
            playerUIInstance.GetComponent<PlayerUI>().setInventario(inventario);
            playerUIInstance.GetComponent<PlayerUI>().setPlayer(GetComponent<PlayerMovement>());
          

        }
    }
   
	// Update is called once per frame
	private void Update ()
	{
        if (!PlayerUI.openInvent)
        {
            equipar();
            if (!isLocalPlayer)
            {
                return;
            }
            //pega direcao da camera de frente e de lado e multiplica com inputs horizontal e vertical	
            animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
            animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
            Vector3 directFinal = Input.GetAxis("Vertical") * cameraPlayer.direcaoFrente() + Input.GetAxis("Horizontal") * cameraPlayer.direcaoDireita();
            directFinal.Normalize();
            if (controller.isGrounded)
            {
                //determina a direcao de movimento e multiplica com o moveSpeed

                moveDirection = new Vector3(directFinal.x, 0, directFinal.z) * this.Speed;



                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = this.JumpForce;
                    this.Stamina1 = this.Stamina1 - 30;
                }


                //muda velocidade para correr
                if (Input.GetButton("correr") || Input.GetButtonDown("correr") && this.Stamina1 <= 1000)
                {
                    this.Speed = this.RunSpeed;
                    this.Stamina1 = this.Stamina1 - 3;
                }

                if (Input.GetButtonUp("correr"))
                {
                    this.Speed = this.WalkSpeed;
                }


            }

            //--------------agacha----------------------
            if (Input.GetButton("agachar") || Input.GetButtonDown("agachar"))
            {

                controller.height = 1.5f;
                this.Speed = 1;
            }
            if (Input.GetButtonUp("agachar"))
            {
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
                Interacao temp = objetoInteracao.GetComponent<Interacao>();
                if (temp != null)
                {
                    if (temp.isobjeto)
                    {
                        //adiciona no inventario
                        inventario.GetComponent<Inventario>().adicionarItem(temp.Interagir());
                    }
                    else
                    {

                        temp.Interagir();
                    }



                }

            }

            if (Input.GetButtonUp("visaonoturna"))
            {
                print("ATIVA VISAO NOTURNA");

            }
            if (Input.GetButtonUp("jogariten"))
            {
                if (ArmaEquipada != null)
                {

                    print("joga item fora");


                }

            }

            if (Input.GetMouseButton(0))
            {
                if (ArmaEquipada != null)
                {
                    print("armaequipada");
                    ArmaEquipada.GetComponent<Arma_Generica>().Atirar();
                }


            }
            raycast();
            //aplica gravidade
            moveDirection.y += Physics.gravity.y * Time.deltaTime;
            //move o personagem 
            controller.Move(moveDirection * Time.deltaTime);
            //controle da camera 	
            cameraPlayer.camController();
        }
        else {
            cameraPlayer.cursor(true);

        }
	}
  
 
   
    public void raycast() {
        origin = cameraPlayer.camera.transform.position;
        direction = cameraPlayer.camera.transform.forward;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.SphereCast(origin, sphereRadius,direction, out hit,maxDistance,layerMask,QueryTriggerInteraction.UseGlobal))
        {
            objetoInteracao = hit.collider.gameObject;
            currenthitdistance = hit.distance;
          //  Debug.DrawRay(cameraPlayer.camera.transform.position, cameraPlayer.camera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            currenthitdistance = maxDistance;
            objetoInteracao = null;
         
         //   Debug.DrawRay(cameraPlayer.camera.transform.position, cameraPlayer.camera.transform.TransformDirection(Vector3.forward) * distanciaInteracao, Color.white);
            Debug.Log("Did not Hit");
        }


    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * currenthitdistance);
        Gizmos.DrawWireSphere(origin + direction * currenthitdistance, sphereRadius);
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
    public void recebeDano(float dano)
    {
        this.Vida -= dano;
    }

    void RestaurandoStamina()
    {
        if (this.Stamina1 > 1000)
        {
            Stamina1 = 1000;
        }
        this.Stamina1 += 5;
    }

    void FicandoComFome()
    {
        this.Fome1 -= 5;
    }

    void FicandoComSede()
    {
        this.Sede1 -= 5;
    }

    public void equipar() {
        if (Input.GetKey(KeyCode.Alpha1)) {

            if (ArmaEquipada == null)
            {
                foreach (Transform arma in inventario.transform)
                {

                    if (arma.GetComponent<infoSlot>().getSlot() == 1)
                    {
                        ArmaEquipada = arma.gameObject;
                        ArmaEquipada.transform.SetParent(pontoHand, false);
                        ArmaEquipada.SetActive(true);
                        break;
                    }
                }
            }
            else {
                ArmaEquipada.transform.SetParent(inventario.transform, false);
                ArmaEquipada.SetActive(false);
                foreach (Transform arma in inventario.transform)
                {

                    if (arma.GetComponent<infoSlot>().getSlot() == 1)
                    {
                        ArmaEquipada = arma.gameObject;
                        ArmaEquipada.transform.SetParent(pontoHand, false);
                        ArmaEquipada.SetActive(true);
                        break;
                    }
                }


            }
            

        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            
            foreach (Transform arma in inventario.transform)
            {

                if (arma.GetComponent<infoSlot>().getSlot() == 2)
                {
                    print("usa item");
                    break;
                }
            }


        }
        if (Input.GetKey(KeyCode.Alpha3))
        {

            foreach (Transform arma in inventario.transform)
            {

                if (arma.GetComponent<infoSlot>().getSlot() == 3)
                {
                    print("usa item");
                    break;
                }
            }

        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            foreach (Transform arma in inventario.transform)
            {

                if (arma.GetComponent<infoSlot>().getSlot() == 4)
                {
                    print("usa item");
                    break;
                }
            }


        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            foreach (Transform arma in inventario.transform)
            {

                if (arma.GetComponent<infoSlot>().getSlot() == 5)
                {
                    print("usa item");
                    break;
                }
            }


        }
        if (Input.GetKey(KeyCode.Alpha6))
        {

            foreach (Transform arma in inventario.transform)
            {

                if (arma.GetComponent<infoSlot>().getSlot() == 6)
                {
                    print("usa item");
                    break;
                }
            }

        }
        if (Input.GetKey(KeyCode.Alpha7))
        {

            foreach (Transform arma in inventario.transform)
            {

                if (arma.GetComponent<infoSlot>().getSlot() == 7)
                {
                    print("usa item");
                    break;
                }
            }

        }
       


    }
}
