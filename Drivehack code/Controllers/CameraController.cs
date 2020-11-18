using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    public Vector3 mainPos;
    public float translateSpeed = 15f;
    public float offset = 0;
    private void Start()
    {
        mainPos = cam.transform.position;
    }
    private void Update()
    {
        //cam.transform.LookAt(GameMaster.instance.dragon.transform);
    }
    public void MoveCam(int pos)
    {
        //cam.transform.position = mainPos - new Vector3(((pos-1)*offset), 0f, 0f);
        StartCoroutine(TranslateMove(mainPos - new Vector3(((pos - 1) * offset), 0f, 0f), translateSpeed, cam.transform));
    }
    IEnumerator TranslateMove(Vector3 end, float speed, Transform form)
    {
        while (Vector3.Distance(form.position, end) > speed * Time.deltaTime)
        {
            form.position = Vector3.MoveTowards(form.position, end, speed * Time.deltaTime);
            yield return 0;
        }
        form.position = end;
        //sliding = false;

    }
    private void Show(string layername)
    {
        cam.cullingMask |= 1 << LayerMask.NameToLayer(layername);
    }

    // Turn off the bit using an AND operation with the complement of the shifted int:
    private void Hide(string layername)
    {
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer(layername));
    }

    // Toggle the bit using a XOR operation:
    public void Toggle(string layername)
    {
        cam.cullingMask ^= 1 << LayerMask.NameToLayer(layername);
    }
}
