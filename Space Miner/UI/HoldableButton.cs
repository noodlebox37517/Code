using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldableButton : Button
{

    public bool right = true;

    public void Update()
    {
        //A public function in the selectable class which button inherits from.
        if (IsPressed())
        {
            WhilePressed();
        }
    }

    public void WhilePressed()
    {
        Master.instance.Targeterobj.GetComponent<Targeter>().ChangeTargetAngle(right);
    }
}
