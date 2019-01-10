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


        private void Update()
        {
            //-----------ATUALIZA HUD PLAYER-----------
            slideVida.value = player.Vida / 1000;
            slideFome.value = player.Fome1 / 1000;
            slideSede.value = player.Sede1 / 1000;
            slideStamina.value = player.Stamina1 / 1000;
            //-----------ATUALIZA HUD PLAYER - Armas -----------

        }

    public void setPlayer(PlayerMovement player) {

        this.player = player;
    }
}
