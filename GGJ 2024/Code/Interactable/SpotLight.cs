using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLight : Interactable
{

    
    private void OnEnable()
    {
        Interacted += SpotLightOn;
    }

    public void SpotLightOn()
    {
        IllusionManager.instance.ProgressState();
    }
}
