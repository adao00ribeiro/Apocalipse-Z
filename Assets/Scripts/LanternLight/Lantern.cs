using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lantern : MonoBehaviour
{
    public Texture[] TextureBatery;
    public Text power;
    public AudioClip click;
    public int number;
    public int numberTexture,bateryTexturing;
    public static float timeTotal = 120;
    public static float TimeTotalInitial;
    public float timeMinimo = 20;
    public float angleLight = 25;
    public float distanceLight = 50;
    public float intencity = 5;
    public float reductionLight = 10;
    public float angleMax, angleMin,intencityMax,intencityMin,distanceMax,distanceMin;

    public bool CanCall;

    private void Start()
    {
        GetComponent<Light>().enabled = true;
        TimeTotalInitial = timeTotal;
        CanCall = false;
        numberTexture = TextureBatery.Length - 1;
        number = Mathf.RoundToInt(timeTotal) / TextureBatery.Length;
    }

    private void Update()
    {
       if(GetComponent<Light>().enabled == true && CanCall == true)
        {
            power.text = "Flashlight is on!";
            timeTotal -= Time.deltaTime;
        }
       if(Input.GetKeyDown(KeyCode.F))
        {
            if(GetComponent<Light>().enabled == true)
            {
                power.text = "Flashlight is off!";
                GetComponent<Light>().enabled = false;
                GetComponent<AudioSource>().PlayOneShot(click);
            }
            else
            {
                power.text = "Flashlight is on!";
                GetComponent<Light>().enabled = true;
                GetComponent<AudioSource>().PlayOneShot(click);
            }
        }
       if(timeTotal <= timeMinimo)
        {
            power.text = "Flashlight without Battery!";
            GetComponent<Light>().enabled = false;

            CanCall = false;
        }
        if (timeTotal >= timeMinimo)
        {
            CanCall = true;
        }
        if (timeTotal >= TimeTotalInitial)
        {
            timeTotal = TimeTotalInitial;
        }
        GetComponent<Light>().spotAngle = angleLight * timeTotal / reductionLight;
        GetComponent<Light>().range = distanceLight * timeTotal / reductionLight;
        GetComponent<Light>().intensity = intencity * timeTotal / reductionLight/2;
        bateryTexturing = Mathf.FloorToInt(timeTotal / number);

        if(GetComponent<Light>().spotAngle >= angleMax)
        {
            GetComponent<Light>().spotAngle = angleMax;
        }
        if(GetComponent<Light>().spotAngle <= angleMin)
        {
            GetComponent<Light>().spotAngle = angleMin;
        }

        if (GetComponent<Light>().range >= distanceMax)
        {
            GetComponent<Light>().range = distanceMax;
        }
        if (GetComponent<Light>().range <= distanceMin)
        {
            GetComponent<Light>().range = distanceMin;
        }

        if (GetComponent<Light>().intensity >= intencityMax)
        {
            GetComponent<Light>().intensity = intencityMax;
        }
        if (GetComponent<Light>().intensity <= intencityMin)
        {
            GetComponent<Light>().intensity = intencityMin;
        }
    }

    private void OnGUI()
    {
        GUI.DrawTexture (new Rect(Screen.width / 1.92f + Screen.width / 2.3f, Screen.height / 2 - Screen.height / 2, Screen.width / 25, Screen.height / 10), TextureBatery[bateryTexturing]);

    }
}
