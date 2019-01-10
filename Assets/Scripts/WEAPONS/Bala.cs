using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float danoBala;
    private Rigidbody rd;
    public float speed;
    private Vector3 direcaobala;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyOBJETO",5);
        rd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
         rd.velocity = direcaobala * speed * Time.deltaTime;
    }
    public void direcao(Vector3 direc)
    {

        direcaobala = direc;

    }
   

    public void DestroyOBJETO()
    {
        Destroy(gameObject);
    }
    
}
