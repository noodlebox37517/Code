using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class Textupdate : MonoBehaviour
{
    public string pretext= "";
    public string postext = "";

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = pretext+ Game.instance.Resource.ToString(); 
    }
}
