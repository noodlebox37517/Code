using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KnobTargeter : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{

    public Transform midpoint;
    public float radius;
    public float lastangle = 0;
    private float firstangle;
    public bool trackpointer= false;
    public float maxangles = 90f;
    public int sign =1;
    // Use this for initialization
    void Start () {
        if (midpoint != null)
        {
            radius = radius = Vector2.Distance(transform.position, midpoint.position);
        }
        firstangle = lastangle;

    }
	
	// Update is called once per frame
	void Update () {


            if (trackpointer)
        {

            Vector3 mouspos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            //calc angle
            Vector3 direction = (  mouspos -midpoint.position).normalized;
            lastangle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg * -1 +90f) ;
            sign = 1;
            if(lastangle <= 0f || lastangle >180f)
            {
                sign = -1;
            }

            if(lastangle >= maxangles)
            {
                lastangle = maxangles * sign;
            }

            //move knob to angle pos

            float x = midpoint.position.x + radius * Mathf.Sin(lastangle * Mathf.Deg2Rad);
            float y = midpoint.position.y + radius * Mathf.Cos(lastangle * Mathf.Deg2Rad);

            this.transform.position = new Vector3(x, y, 0f);
            //update pointer
            Master.instance.Targeterobj.GetComponent<Targeter>().ForceTargetAngle(lastangle);
        }
	}
    public void OnPointerExit(PointerEventData eventData)
    {
        trackpointer = false;
        //Master.instance.Targeted();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        trackpointer = true;

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //get pointer pos
        //trackpointer = true;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // trackpointer = false;
        Master.instance.Targeted();
    }

    public void Reset()
    {
        if (radius == 0f)
        {
            radius = radius = Vector2.Distance(transform.position, midpoint.position);
        }

        lastangle = firstangle;

        float x = midpoint.position.x + radius * Mathf.Sin(lastangle * Mathf.Deg2Rad);
        float y = midpoint.position.y + radius * Mathf.Cos(lastangle * Mathf.Deg2Rad);

        this.transform.position = new Vector3(x, y, 0f);
    }


}
