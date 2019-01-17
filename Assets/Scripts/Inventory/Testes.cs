using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testes : MonoBehaviour
{
    public GameObject[] itens;

    void Start()
    {
        //seta arma da mão
        itens[0].SetActive(true);
        GetComponent<PlayerMovement>().ArmaEquipada = itens[0];
        itens[1].SetActive(false);
    }


    void Update()
    {
        //ativa a pistola e seta ela como a arma
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            itens[0].SetActive(true);
            GetComponent<PlayerMovement>().ArmaEquipada = itens[0];
            itens[1].SetActive(false);
        }
        // ativa o machado e seta ele como a arma
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            itens[0].SetActive(false);
            GetComponent<PlayerMovement>().ArmaEquipada = itens[1];
            itens[1].SetActive(true);
        }
    }
}
