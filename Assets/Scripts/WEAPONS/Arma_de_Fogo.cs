using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma_de_Fogo : Arma_Generica
{
    [SerializeField]
    protected int balas;
    [SerializeField]
    protected int totalBalas;
    [SerializeField]
    protected int regarga;
    public GameObject prefabBala;
    public Transform spawPoint;

    public override void Atirar() {}

    public int getBalas()
    {
        return balas;
    }
}
