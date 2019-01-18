using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour
{
    private PlayerMovement player;
    public Slider slideVida;
    public Slider slideFome;
    public Slider slideSede;
    public Slider slideStamina;


    [SerializeField]
    private Arma_de_Fogo Arma;
    public Text nomearma;
    public Text numerodebalas;

    private void Update()
        {
        //-----------ATUALIZA HUD PLAYER-----------
            slideVida.value = player.Vida / 1000;
            slideFome.value = player.Fome1 / 1000;
            slideSede.value = player.Sede1 / 1000;
            slideStamina.value = player.Stamina1 / 1000;
        //-----------ATUALIZA HUD PLAYER - Armas -----------
        nomearma.text = Arma.nomeArma;
        numerodebalas.text = Arma.getBalas().ToString();
        }

    public void setPlayer(PlayerMovement player) {

        this.player = player;
        Arma = player.ArmaEquipada.GetComponent<Arma_de_Fogo>();
    }
}
