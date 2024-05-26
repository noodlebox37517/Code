using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSlider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (this.GetComponent<Slider>().value != 0)
		{
			Master.instance.Targeterobj.GetComponent<Targeter>().ChangeTargetAngle(this.GetComponent<Slider>().value);

			if (!Input.GetMouseButton(0))
			{
				this.GetComponent<Slider>().value = 0;
			}
		}

	}

}
