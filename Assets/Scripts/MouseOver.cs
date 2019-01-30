using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MouseOver : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private PlayerUI ui;
    private Vector2 offset;
    public void Start() {
        ui = GameObject.Find("PlayerUI").GetComponent<PlayerUI>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        
            this.transform.SetParent(this.transform.parent.parent);
            this.transform.position = eventData.position - offset;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        
            this.transform.position = eventData.position - offset;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
         this.transform.SetParent(ui.painel[1].transform);
        this.transform.position = ui.painel[1].transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      //  tooltip.Activate(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    //    tooltip.Deactivate();
    }
}
