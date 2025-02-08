using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CellClickable : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
       foreach(GameObject neighbor in GetComponent<FloorInit>().neighbors)
        {
            neighbor.GetComponent<MeshRenderer>().material.color = new Color32(255, 0, 0, 255);
        }
    }

}
