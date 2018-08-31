using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchPAnel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("down!");
        if (!Master.instance.pause)
        {

            Master.instance.grabbed = true;
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("UP!");
        if (Master.instance.grabbed)
        {
            if (!Master.instance.FireButtons)
                Master.instance.Targeted();
            Master.instance.grabbed = false;
        }
    }

}
