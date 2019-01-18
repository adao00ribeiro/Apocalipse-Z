using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArmaBranca_Generica : MonoBehaviour
{
    public string Nome;

    public float Dano;

    public float Distancia;

    public float TempoDeAtaque;

    public abstract void Atirar();
}
