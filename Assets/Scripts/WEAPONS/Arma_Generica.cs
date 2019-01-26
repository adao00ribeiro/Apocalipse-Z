using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Arma_Generica : MonoBehaviour
{
    [SerializeField]
    public string nomeArma;
    [SerializeField]
    protected float DanoArma;

    [HideInInspector] public PlayerMovement player;

    public  abstract void Atirar();

    public void Awake()
    {
        //reconhece o player
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
}
