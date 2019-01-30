using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public List<GameObject> painel;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Transform inventarioPlayer;
    public Slider slideVida;
    public Slider slideFome;
    public Slider slideSede;
    public Slider slideStamina;
    public GameObject inventarioHUD;
    public static bool openInvent;
    [SerializeField]
    private Arma_de_Fogo Arma;
    public Text nomearma;
    public Text numerodebalas;
    Inventario scriptInventario;
    public GameObject prefabSlot;
    public Transform inventarioSlots;

    public Mochila painelMochila;
    private void Start()
    {
        scriptInventario = inventarioPlayer.GetComponent<Inventario>();
        GeraSlot();
    }
    private void Update()
        {
        atualizaInventario();
        //-----------ATUALIZA HUD PLAYER-----------
            slideVida.value = player.Vida / 1000;
            slideFome.value = player.Fome1 / 1000;
            slideSede.value = player.Sede1 / 1000;
            slideStamina.value = player.Stamina1 / 1000;
        //-----------ATUALIZA HUD PLAYER - Armas -----------
       // nomearma.text = Arma.nomeArma;
       // numerodebalas.text = Arma.getBalas().ToString();

        if (Input.GetKey(KeyCode.Alpha9))
        {
            atualizaInventario();
        }
        //--------------INVENTARIO----------------------
        if (Input.GetButtonUp("inventario"))
        {
            openInvent = !openInvent;
            inventarioHUD.SetActive(openInvent);
            atualizaInventario();
        }
     
    }

    public void setPlayer(PlayerMovement player) {

        this.player = player;
        Arma = player.ArmaEquipada.GetComponent<Arma_de_Fogo>();
       
    }
    public void setInventario(Transform inventario) {
        this.inventarioPlayer = inventario;
    }
    public void atualizaInventario() {
        foreach (Transform slot in inventarioSlots) {
            foreach (Transform item in inventarioPlayer) {

                if (slot.GetComponent<infoSlot>().getSlot()== item.GetComponent<infoSlot>().getSlot()) {

                    slot.GetChild(0).GetComponent<Image>().sprite = item.GetComponent<Interacao>().imagem;
                    slot.GetComponent<infoSlot>().objeto = item.GetComponent<infoSlot>().objeto;
                }
            }

        }

    }
    public void GeraSlot() {
        mochila();
        for (int i = 0; i < scriptInventario.getQuantidadeTotal();i++) {
         GameObject slot = Instantiate(prefabSlot);
            slot.transform.SetParent(inventarioSlots);
            slot.GetComponent<infoSlot>().setSlot(i);
            slot.transform.GetComponentInChildren<Text>().text = (i + 8).ToString();
        }


    }

    public void mochila() {

        if (scriptInventario.mochila == null)
        {
            scriptInventario.setQuantidadeTotal(5);
        }
        else {
            scriptInventario.setQuantidadeTotal(scriptInventario.mochila.GetComponent<Mochila>().tamanhoMochila);

        }
    }
}
