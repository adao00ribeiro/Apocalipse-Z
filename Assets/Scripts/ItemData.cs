using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private PlayerUI ui;
    private Vector2 offset;
    public  int slotId;

    public Image item;


    public void Update()
    {
        print(slotId);
    }
    void Start()
    {
        ui = GameObject.Find("PlayerUI").GetComponent<PlayerUI>();
   //     inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    //    tooltip = inv.GetComponent<Tooltip>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            this.transform.SetParent(this.transform.parent.parent);
            this.transform.position = eventData.position - offset;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            this.transform.position = eventData.position - offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(ui.painel[slotId].transform);
        this.transform.position = ui.painel[slotId].transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
   //     tooltip.Activate(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
     //   tooltip.Deactivate();
    }
}