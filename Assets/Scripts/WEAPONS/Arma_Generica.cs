using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Arma_Generica : MonoBehaviour
{
    [SerializeField]
    public string nomeArma;
    [SerializeField]
    protected float DanoArma;

    public  abstract void Atirar();

}
