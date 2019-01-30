using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snake.ApocalipseZ.Enemys;

public class Bala : MonoBehaviour
{
    public float speed;
    public float danoBala;
    public float timeToLive;
    Rigidbody2D rd;

    Vector2 vetor;
    void Start()
    {
        Destroy(gameObject,timeToLive);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zumbie")) {

            other.gameObject.GetComponent<EnemyBase>().ApplyDamage(100);
            print("acerto um zumbi");
            Destroy(gameObject);
        }
    }

   
    
}
