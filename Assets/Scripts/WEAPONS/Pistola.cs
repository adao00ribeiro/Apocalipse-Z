    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistola : Arma_de_Fogo
{
   public ParticleSystem muzzleFire;
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
    
    public override void Atirar() {
        if (isCinematic) {
            muzzlePlay();
            return;
        }

            GameObject tempBala = Instantiate(prefabBala, spawPoint.position, spawPoint.rotation);
            tempBala.GetComponent<Bala>().danoBala = this.DanoArma;
            this.balas--;
             
            print(gameObject.name + "atirou");

    }
    
    public void muzzlePlay() {
        if (muzzleFire==null) {
            return;
        }
        muzzleFire.Play();
    }

}
