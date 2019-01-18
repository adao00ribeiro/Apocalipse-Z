using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistola : Arma_de_Fogo
{
   
    // Start is called before the first frame update
    public void Start()
    {
        balas = totalBalas;
        regarga = 24;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    override
    public void Atirar() {
        
            GameObject tempBala = Instantiate(prefabBala, spawPoint.position, prefabBala.transform.rotation);
            tempBala.GetComponent<Bala>().danoBala = this.DanoArma;
            tempBala.GetComponent<Bala>().direcao(spawPoint.right);
            this.balas--;
            print(gameObject.name + "atirou");
        


        
    }

}
