using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class personagem : MonoBehaviour {


    public Button[] slot = new Button[12]; //qual slot o item vai entrar
    public Image[] icone = new Image[12]; //o icone do item para mostrar
    public Text[] qt_item = new Text[12]; // quantidade de item para mostrar
    public int[] acumulaitem = new int[12]; //acumula item se for do mesmo tipo
    private bool pegaitem, vermochila = false; //tem de estar true para vc pegar o item.
    public Collider Player; //pegando o corpo do player
    public float tempodeespera = 0.1f; //tempo de espera para pegar o proximo item
    private int numeroitem;
    private bool[] temitem = new bool[12];
    private string[] tipoitem = new string[12];
    public Canvas UI;

    public float speed = 20f; 
    private Vector3 pos;
    private int x;
	
	void Start () {
        for ( x = 0; x <= 11; x++)
        {
            if (acumulaitem[x] >= 0)
            {
                qt_item[x].text = ""; //deixar o texto vazio para que não apareça nada
                acumulaitem[x] = 0; //zerando a variavel
                temitem[x] = false;
            }
        }
        pegaitem = true;
    }


    void Update()
    {
        movimentacao();


        for (int y = 11; y >= 0; y--)
        {
            if (acumulaitem[y] <= 0) //perguntando se esgotou os itens
            {
                qt_item[y].text = ""; //deixando em branco a quantidade de item se não houver mais itens
                icone[y].color = Color.white;
                temitem[y] = false;
                tipoitem[y] = null;
            }

        }
        if (Input.GetKeyDown(KeyCode.I) && !vermochila)
        {
            UI.gameObject.SetActive(true);
            vermochila = true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && vermochila)
        {
            UI.gameObject.SetActive(false);
            vermochila = false;
        }
    }
    private void OnCollisionStay(Collision col)
    {
        if (Input.GetKeyDown(KeyCode.E) && col.gameObject.tag == "itemtipo1"  && pegaitem)
        //verificando se apertou E, se colidio com um objeto de tag "itemtipo(valor do int numeroitem)" e se pode pegar item
        {
                
            for (x = 2; x <= 11; x++)
            {
                if (col.gameObject.tag == tipoitem[x] && pegaitem)
                {
                    acumulaitem[x] += 1;
                    qt_item[x].text = acumulaitem[x].ToString();
                    pegaitem = false;
                    StartCoroutine("Rotina_pegaitem");
                    Destroy(col.gameObject);
                }
            }
            for (x = 2; x <= 11; x++)
            {
                if (acumulaitem[x] <= 0 && temitem[x] == false && pegaitem)
                {

                    acumulaitem[x] += 1;
                    qt_item[x].text = acumulaitem[x].ToString();
                    icone[x].color = Color.red;
                    qt_item[x].color = Color.white;
                    pegaitem = false;
                    temitem[x] = true;
                    tipoitem[x] = "itemtipo1";
                    StartCoroutine("Rotina_pegaitem");
                    Destroy(col.gameObject);
                    break;
                }
            }
               
        }
        else
        if (Input.GetKeyDown(KeyCode.E) && col.gameObject.tag == "itemtipo0" && pegaitem) {
            for (x = 0 ; x <= 1; x++)
            {
                if (acumulaitem[x] <= 0 && temitem[x] == false && pegaitem)
                {

                    acumulaitem[x] += 1;
                    icone[x].color = Color.red;
                    qt_item[x].color = Color.white;
                    pegaitem = false;
                    temitem[x] = true;
                    tipoitem[x] = "itemtipo" + numeroitem.ToString();
                    StartCoroutine("Rotina_pegaitem");
                    Destroy(col.gameObject);
                    break;
                }
            }
            for (x = 7; x <= 11; x++)
            {
                if (acumulaitem[x] <= 0 && temitem[x] == false && pegaitem)
                {

                    acumulaitem[x] += 1;
                    icone[x].color = Color.red;
                    qt_item[x].color = Color.white;
                    pegaitem = false;
                    temitem[x] = true;
                    tipoitem[x] = "itemtipo" + numeroitem.ToString();
                    StartCoroutine("Rotina_pegaitem");
                    Destroy(col.gameObject);
                    break;
                }
            }
        }
        
    }
    IEnumerator Rotina_pegaitem()
    {
        yield return new WaitForSeconds(tempodeespera); //rotina para esperar a coleta de item
        pegaitem = true;
    }
    private void movimentacao()
    {
        pos = transform.position;
        pos.x += speed * Input.GetAxis("Horizontal") * Time.deltaTime;
        pos.z += speed * Input.GetAxis("Vertical") * Time.deltaTime;
        transform.position = pos;
    }
}
