using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Remedios : MonoBehaviour
{
    public enum SelecionarRemedio
    {
        Bandage,
        Antibiotics,
        Morphine,
        None
    };
    public SelecionarRemedio Remedio = SelecionarRemedio.None;

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
        if (Input.GetKeyDown(KeyCode.E) && coll == true && CompareTag("Medicaments"))
        {
            switch(Remedio)
            {
                case SelecionarRemedio.Bandage:
                    STATUSPLAYER.bandage += 1;
                    Mensagem.text = "";
                    break;
                case SelecionarRemedio.Morphine:
                    STATUSPLAYER.morphine += 1;
                    Mensagem.text = "";
                    break;
                case SelecionarRemedio.Antibiotics:
                    STATUSPLAYER.antibiotics += 1;
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
