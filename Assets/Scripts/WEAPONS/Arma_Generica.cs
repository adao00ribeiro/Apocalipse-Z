using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Tipo { ARMA_DE_FOGO, ARMA_BRANCA }
public abstract class Arma_Generica : MonoBehaviour
{
    [SerializeField]
    public string nomeArma;
    [SerializeField]
    protected float DanoArma;
    public Tipo tipo;

 

    public  abstract void Atirar();

   
}
