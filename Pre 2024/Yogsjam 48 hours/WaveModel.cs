using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "wave", menuName = "Waves/wave")]
public class WaveModel : ScriptableObject
{
    public int Mobsinwave = 20;
    public int Maxmobs = 1;
    public List<GameObject> mobs;
}
