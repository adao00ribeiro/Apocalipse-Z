using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bebida : MonoBehaviour
{
    public enum SetarBebidas
    {
        None,
        Water,
        Juice,
        Coca
    };
    public SetarBebidas Bebidas = SetarBebidas.None;


    private bool coll;
    public Text Mensagem;
    [SerializeField] private GameObject item;

    void Start()
    {
        Mensagem.text = "";
        coll = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && coll == true && CompareTag("Drinks"))
        {
            switch (Bebidas)
            {
                case SetarBebidas.Water:

                    Mensagem.text = "";
                    break;
                case SetarBebidas.Juice:

                    Mensagem.text = "";
                    break;
                case SetarBebidas.Coca:

                    Mensagem.text = "";
                    break;
            }

            Destroy(item);
        }
    }

    private void OnTriggerEnter()
    {
        coll = true;
        Mensagem.text = "Press [E]";
    }

    private void OnTriggerExit()
    {
        coll = false;
        Mensagem.text = "";
    }
}
