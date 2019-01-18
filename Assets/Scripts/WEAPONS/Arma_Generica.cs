using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Arma_Generica : MonoBehaviour
{
 

    [SerializeField]
    public string nomeArma;
    [SerializeField]
    protected float DanoArma;
    [SerializeField]
    protected int balas;
    [SerializeField]
    protected int totalBalas;
    [SerializeField]
    protected int regarga;
    public GameObject prefabBala;
    public Transform spawPoint;



    public  abstract void Atirar();

    public int getBalas() {
        return balas;
    }
}
