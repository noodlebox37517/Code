using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public WaveModel Wave;
    public List<WaveModel> waves;
    private int waveid = -1;
    public List<GameObject> spawns;

    public float maxspawnrange =20;
    public List<GameObject> mobs;
    public int mobsspawned =0;
    public bool wavelive = false;
    public float minspawngap = .5f;
    private float lastspawn;
    public float spawnschance = .1f;
    private int totalspawned;
    public int maxwaveinc=5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Game.instance.CV.GetComponent<TextController>().texts[1].text = "Wave: " + (waveid+1);
        Game.instance.CV.GetComponent<TextController>().texts[2].text = "Enemies:  " + mobsspawned;
        if (wavelive)
        {
            if (mobsspawned<Wave.Mobsinwave) {
                if (mobs.Count < Wave.Maxmobs+ totalspawned/ maxwaveinc)
                {
                    if (Time.time > lastspawn + minspawngap) {
                        if (Random.value < spawnschance * Time.deltaTime)
                        {
                            mobs.Add(Spawn());
                            mobsspawned++;
                            minspawngap *= .99f;
                            lastspawn = Time.time;
                            totalspawned++;
                        }
                        }

                }
            }
            else
            {
                wavelive = false;
                Game.instance.WaveEnded();

            }
        }


    }
    public GameObject Spawn()
    {
        //random spawn
        //random mob
        int spawnindex = Random.Range(0, spawns.Count );

        GameObject go = Wave.mobs[Random.Range(0, Wave.mobs.Count)];
        Vector3 spawnoffset = new Vector3((Random.value-.5f)*2, 0f, (Random.value - .5f) * 2).normalized * (Random.value * maxspawnrange);
        GameObject mob = Instantiate<GameObject>(go, spawns[spawnindex].transform.position+ spawnoffset, Quaternion.identity);
        return mob;
    }
    public bool LoadNextWave()
    {
        if (waveid < waves.Count)
        {
            waveid++;
            Wave = waves[waveid];
            mobsspawned = 0;
            wavelive = true;
            return true;

        }
        return false;

    }
}
