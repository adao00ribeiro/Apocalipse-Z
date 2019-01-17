using Snake.ApocalipseZ.Enemys;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meele : ArmaBranca_Generica
{
    [SerializeField] private float Atacando;

    private Camera cam;


    void Start()
    {
        // procura a camera do player!
        cam = FindObjectOfType<Camera>();

        Atacando = this.TempoDeAtaque;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Atacando == this.TempoDeAtaque)
        {
            Atacando = 0;
            //pega a posição do centro da tela e acerta o alvo se estiver no alcance
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, this.Distancia))
            {
                Debug.Log("Bateu");
                //se o alvo for um player ele recebera o dano da arma
                 if(tag == "Player")
                {
                    GetComponent<PlayerMovement>().recebeDano(Dano);
                }
                //se o alvo for um zumbi ele recebera o dano da arma
                if (tag == "Zumbi")
                {
                    GetComponent<EnemyBase>().ApplyDamage(Dano);
                }
            }
            else
            {
                Debug.Log("Não alcançou");
            }            
        }
        Cronometro();
    }

    void Cronometro()
    {
        if(Atacando != this.TempoDeAtaque)
        {
            Atacando += TempoDeAtaque * Time.deltaTime;
        }

        if (Atacando > this.TempoDeAtaque)
        {
            Atacando = this.TempoDeAtaque;
        }
    }
}
