using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class btn_slot1 : MonoBehaviour {

    public personagem usaitem; //pego a referencia do script personagem que está no meu player
    private bool CD_item = true;
    private float tempoderecarga = 10f;

    

    public void Clickslot1() //função que vou chamar no botão do primeiro slot
    {
        if (usaitem.acumulaitem[0] >= 1 && CD_item) //pergunto se há item nesse slot
        {
            usaitem.acumulaitem[0] -= 1; //se houver eu retiro 1
            usaitem.qt_item[0].text = usaitem.acumulaitem[0].ToString(); //atualizo o texto
            CD_item = false;
            StartCoroutine("Rotina_Recarga");
        }
    }
    
    
    IEnumerator Rotina_Recarga() {
        yield return new WaitForSeconds(tempoderecarga);
        CD_item = true;

    }
}
