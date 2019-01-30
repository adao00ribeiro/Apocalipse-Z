using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infoSlot : MonoBehaviour
{
    [SerializeField]
    private int slot;
    public GameObject objeto;

    public int getSlot() {
        return slot;
    }
    public void setSlot(int slot) {
        this.slot = slot;
    }


}
