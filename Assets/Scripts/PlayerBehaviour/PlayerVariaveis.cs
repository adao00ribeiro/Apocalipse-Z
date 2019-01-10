using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerVariaveis : NetworkBehaviour
{ 
[Header("VARIAVEISPLAYER")]
[SerializeField]
private float vida;
[SerializeField]
private float Stamina;
[SerializeField]
private float Sede;
[SerializeField]
private float Fome;
[SerializeField]
 private float speed;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float jumpForce;

    public float Vida
    {
        get
        {
            return vida;
        }

        set
        {
            vida = value;
        }
    }

    public float Stamina1
    {
        get
        {
            return Stamina;
        }

        set
        {
            Stamina = value;
        }
    }

    public float Sede1
    {
        get
        {
            return Sede;
        }

        set
        {
            Sede = value;
        }
    }

    public float Fome1
    {
        get
        {
            return Fome;
        }

        set
        {
            Fome = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    public float WalkSpeed
    {
        get
        {
            return walkSpeed;
        }

        set
        {
            walkSpeed = value;
        }
    }

    public float RunSpeed
    {
        get
        {
            return runSpeed;
        }

        set
        {
            runSpeed = value;
        }
    }

    public float JumpForce
    {
        get
        {
            return jumpForce;
        }

        set
        {
            jumpForce = value;
        }
    }
}
