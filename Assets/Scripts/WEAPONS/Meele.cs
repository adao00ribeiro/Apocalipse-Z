using Snake.ApocalipseZ.Enemys;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meele : ArmaBranca_Generica
{
    [SerializeField] private float Atacando;

    private PlayerMovement player;
    private Camera cam;


    void Start()
    {
        transform.tag = "ArmaBranca";
        // procura a camera do player!
        cam = FindObjectOfType<Camera>();

        Atacando = this.TempoDeAtaque;
    }

    private void Update()
    {
        Cronometro();
    }

    override
    public void Atirar()
    {     
        if(Atacando == TempoDeAtaque)
        { 
            Atacando = 0;
            //pega a posição do centro da tela e acerta o alvo se estiver no alcance
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, this.Distancia))
            {
                Debug.Log("Acertou");
                //se o alvo for um player ele recebera o dano da arma


                //se o alvo for um zumbi ele recebera o dano da arma
                if (tag == "Zumbi")
                {
                    GetComponent<EnemyBase>().ApplyDamage(DanoArma);
                }
                
            }
            else
            {
                Debug.Log("Não alcançou");
            }
            }
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
