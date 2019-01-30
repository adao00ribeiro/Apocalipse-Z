using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    public Camera camera;
    [SerializeField] Texture2D image;
    [SerializeField] int size;

    public float vertical,horizontal;
   
    private void OnGUI()
    {
        Vector3 screenPosition = camera.WorldToScreenPoint(transform.position);
        screenPosition.y = Screen.height - screenPosition.y;
        GUI.DrawTexture(new Rect(screenPosition.x - vertical, screenPosition.y - horizontal, size,size),image);
    }
}
