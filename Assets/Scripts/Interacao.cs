using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacao : MonoBehaviour
{
    public Sprite imagem;
    public bool isobjeto;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interagir(string name) {

       
    }
    public GameObject Interagir() {
        gameObject.SetActive(false);
        return gameObject;
    }
    public GameObject retornaObjeto() {

      return gameObject;
    }
}
