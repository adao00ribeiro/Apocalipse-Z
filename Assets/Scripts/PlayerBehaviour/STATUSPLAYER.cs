using Snake.ApocalipseZ.Enemys;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//Ser adicionado ao Player!
public class STATUSPLAYER : MonoBehaviour
{

    //Itens Consumiveis
    //biscoito + 15, lasanha + 30, agua + 15, suco + 17, coca + 20, bandagem + 10, antibiotics + 20, morphine + 15.
    public static int biscoito, lasanha, agua, suco, coca, bandage, antibiotics, morphine;
    //tempo de usar remedio
    private float tempo;
    private bool podeUsar;
    [Space(3)]
    [Header("Status")]
    public Text vidatxt, sedetext, fometext,Staminatext;
    [Space(3)]
    [Header("Status")]
    public Slider vidasld, fomesld, sedesld, Staminasld;
    public static float currentLife, currentFome, currentSede, currentStamina;
    private int vida = 100, fome = 100, sede = 100, Stamina = 100;
    private float loseLife, loseFome, loseSede, lossStaminaInRun, lossStaminaJump, winStamina;
    private bool run, stop, jump;
    [Space(3)]
    [Header("Cena De Morte")]
    public string cenaDeMorte;

    void Start()
    {
        jump = true;
        podeUsar = true;
        currentStamina = Stamina;
        currentSede = sede;
        currentLife = vida;
        currentFome = fome;
        //estamina
        Staminasld.value = currentStamina;
        Staminatext.text = "" + currentStamina;
        //sede
        sedetext.text = "" + (int)currentSede;
        sedesld.value = (int)currentSede;
        //vida
        vidatxt.text = "" + (int)currentLife;
        vidasld.value = (int)currentLife;
        //fome
        fometext.text = "" + (int)currentFome;
        fomesld.value = (int)currentFome;
    }

    void Update()
    {
        Hunger();
        Life();
        Stamin();
        Thirst();
        Medicamentos();
        Alimentos();
        Bebidas();
        if(currentLife <= 0)
        {
            Dead();
        }
    }

    void Hunger()
    {
        loseFome = 0.004f;
        fometext.text = "" + (int)currentFome;
        fomesld.value = (int)currentFome;
        //ficar com fome
        if (currentFome >= 0)
        {
            currentFome -= loseFome * Time.deltaTime;
        }
        //perder vida ao ficar com muita fome
        if (currentFome <= 0)
        {
            currentLife -= loseLife * Time.deltaTime;
        }

    }

    void Life()
    {
        loseLife = 0.008f;
        vidatxt.text = "" + (int)currentLife;
        vidasld.value = (int)currentLife;
        //vida do player
        if (currentLife >= 100)
        {
            currentLife = 100;
        }

        if (currentLife <= 0)
        {
            currentLife = 0;
        }
        //vida ao morrer
        if (vida <= 0)
        {
            vidatxt.text = "" + currentLife;
            currentLife = 0;
            Dead();
        }
        //perde vida mais rapido qnd fome e sede estão em 0
        if(currentFome <=0 && currentSede <= 0)
        {
            currentLife -= 0.09f * Time.deltaTime;
        }
    }

    void Thirst()
    {
        loseSede = 0.006f;
        sedetext.text = "" + (int)currentSede;
        sedesld.value = (int)currentSede;
        //fica com sede
        if (currentSede >= 0)
        {
            currentSede -= loseSede * Time.deltaTime;
        }
        //perder vida ao ficar com muita sede
        if (currentSede <= 0)
        {
            currentLife -= loseLife * Time.deltaTime;
        }

    }

    void Dead()
    {
        SceneManager.LoadScene(cenaDeMorte);
    }

    void Stamin()
    {
        Staminasld.value = currentStamina;
        Staminatext.text = "" + (int)currentStamina;

        lossStaminaInRun = 0.9f;
        lossStaminaJump = 5;
        winStamina = 1.3f;
        //seta a estamina correta e nao correr/pular ao ficar cansado
        if(currentStamina <= 0)
        {
            winStamina = winStamina * Time.deltaTime;
            GetComponent<MovimentPlayer>().runSpeed = GetComponent<MovimentPlayer>().walkSpeed;
            GetComponent<MovimentPlayer>().jumpForce = 0;
            currentStamina = 0;

        }
        if (currentStamina >= 10)
        {
            GetComponent<MovimentPlayer>().runSpeed = GetComponent<MovimentPlayer>().runSpeed;
            GetComponent<MovimentPlayer>().jumpForce = 5;

        }

        if (currentStamina >= 100)
        {
            currentStamina = 100;
        }
        //perca de estamina ao correr sem fome ou sede
        if (run == true && currentFome >= 10 && currentSede >= 10)
        {
            currentStamina -= lossStaminaInRun * Time.deltaTime;
        }
        //perca de estamina ao correr com fome ou sede
        if (run == true && currentSede <= 10)
        {
            currentStamina -= 4 * Time.deltaTime;
        }
        if (run == true && currentFome <= 10)
        {
            currentStamina -= 4 * Time.deltaTime;
        }
        //perca de estamina ao pular
        if (Input.GetKeyDown(KeyCode.Space) && jump == true)
        {
            StartCoroutine("Jump");
            currentStamina -= lossStaminaJump;
        }
        //ganho de estamina sem correr e sem fome ou sede
        if (stop == true && currentFome >= 10 && currentSede >= 10)
        {
            currentStamina += winStamina * Time.deltaTime;
        }
        //ganho de estamina sem correr e com fome ou sede
        if (stop == true && currentFome <= 10)
        {
            currentStamina += 0.4f * Time.deltaTime;
        }
        if (stop == true &&currentSede <= 10)
        {
            currentStamina += 0.4f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            run = true;
            stop = false;
        }
        else
        {
            run = false;
            stop = true;
        }

    }

    void Medicamentos()
    {
        if(bandage >= 1 && Input.GetKeyDown(KeyCode.Q) && currentLife <= 90)
        {
            bandage -= 1;
            currentLife += 10;
        }
    }

    void Alimentos()
    {
        
    }

    void Bebidas()
    {

    }

    public void Damage()
    {
        currentLife = currentLife - 20;
    }

    //usando remedio
    IEnumerator UsandoRemedio()
    {
        podeUsar = false;
        currentLife += 10;
        yield return new WaitForSeconds(tempo);
        podeUsar = true;
    }

    IEnumerator Jump()
    {
        jump = false;
        yield return new WaitForSeconds(1.2f);
        jump = true;
    }
}