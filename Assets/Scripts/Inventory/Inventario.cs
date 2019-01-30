
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario:MonoBehaviour
{
    
    [SerializeField]
    private int quantidadeTotal;
    [SerializeField]
    private int itens;
    public GameObject mochila;
    public void adicionarItem(GameObject item) {

        if (itens< quantidadeTotal) {
            GameObject objeto = Instantiate(item, transform.position, transform.rotation, transform);
            objeto.SetActive(false);
            objeto.AddComponent<infoSlot>().setSlot(itens);
            objeto.GetComponent<infoSlot>().objeto = objeto;
            objeto.GetComponent<Rigidbody>().useGravity = false;
            objeto.GetComponent<Interacao>().enabled = false;
            objeto.GetComponent<BoxCollider>().enabled = false;
            objeto = null;
            itens++;
        }
        

    }
    public void removerItem() {



    }
    public int getquantidadeItens() {

        return transform.childCount;
    }
    public int getQuantidadeTotal()
    {

        return quantidadeTotal;
    }
    public void setQuantidadeTotal(int total) {
        this.quantidadeTotal = total;
    }
}
