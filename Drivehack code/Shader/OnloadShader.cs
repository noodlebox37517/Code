using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnloadShader : MonoBehaviour
{
    public Material mat;

    public float maxVal;
    public float minVal;
    public float inc;

    public string targetstring = "Value";
    // Start is called before the first frame update
    void Start()
    {
        mat = this.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMaster.instance.LC.LevelPlay)
        {
            float curval = mat.GetFloat(targetstring);
            if (curval <= maxVal&& curval>= minVal)
            {
                AlterValue(curval + inc * Time.deltaTime);
            }
        }
    }

    public void AlterValue(float newval)
    {
        mat.SetFloat(targetstring,newval);
    }
}
