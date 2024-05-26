using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_HoverPop : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerClickHandler
{

    public string Text;
    public bool defaultpos = true;
    public Transform popPos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnPointerExit(PointerEventData eventData)
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    { 
        Vector3 pos = new Vector3(0f, 0f, 0f);
        if (!defaultpos)
            pos = popPos.position/ this.GetComponent<RectTransform>().localScale.x;
       // Debug.Log(this.GetComponent<RectTransform>().localScale);
        UI_Control.instance.SpawnPopUp(UI_Control.instance.poppedFab, pos, Text);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 pos = new Vector3(0f, 0f, 0f);
        if (!defaultpos)
            pos = popPos.position / this.GetComponent<RectTransform>().localScale.x;
        // Debug.Log(this.GetComponent<RectTransform>().localScale);
        UI_Control.instance.SpawnPopUp(UI_Control.instance.poppedFab, pos, Text);
    }
}
