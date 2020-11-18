using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMaster : MonoBehaviour
{
    public Camera cam;

    private void Start()
    {
        cam = Camera.main;
        //Toggle("LevelElement");
    }
    private void Show()
    {
        cam.cullingMask |= 1 << LayerMask.NameToLayer("SomeLayer");
    }

    // Turn off the bit using an AND operation with the complement of the shifted int:
    private void Hide()
    {
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("SomeLayer"));
    }

    // Toggle the bit using a XOR operation:
    public void Toggle(string layername)
    {
        cam.cullingMask ^= 1 << LayerMask.NameToLayer(layername);
    }
}
