using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Comida : MonoBehaviour
{
    public enum SetarComidas
    {
        Lasanha,
        Chocolate,
        Biscoito,
        None
    };
    public SetarComidas Comidas = SetarComidas.None;

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
        if (Input.GetKeyDown(KeyCode.E) && coll == true && CompareTag("Foods"))
        {
            switch(Comidas)
            {
                case SetarComidas.Chocolate:

                    Mensagem.text = "";
                    break;
                case SetarComidas.Biscoito:

                    Mensagem.text = "";
                    break;
                case SetarComidas.Lasanha:

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
