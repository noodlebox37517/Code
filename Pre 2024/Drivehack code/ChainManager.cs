using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainManager : MonoBehaviour
{
    public Material chainMat;
    public Material normMAt;
    public int maxchains = 4 ;
    public List<GameObject>[] Chains;
    public List<List<GameObject>> chainList;
    public int[] chainids = { -1, -1, -1, -1 };


    public bool active = false;
    public int chainbonus =5;
    public int chainlength = 0;
    public int activeLength= 0;
    public int chainstreak = 0;
    public List<GameObject> Chain;

    // Start is called before the first frame update
    void Start()
    {
        Chains = new List<GameObject>[maxchains];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChainStart(int length,int bonus, GameObject GO)
    {
        if (!active) {
            active = true;
            
            chainlength = length;
            chainbonus = bonus;
            Chain.Add(GO);
            activeLength++;
            Debug.Log("chained start");
            //start listening to misses and streaks
        }
        else
        {
            GO.transform.GetComponent<MeshRenderer>().materials[0] = GameMaster.instance.CM.normMAt;
            // chain object to regular mat
        }
    }
    public void Chained(bool hit,GameObject GO)
    {
        Debug.Log("Hit chained");
        if (active)
        {
            if (hit)
            {
                GameMaster.instance.UI.toggle(2, true);
                chainstreak++;
                Chain.Remove(GO);
                if (chainstreak >= chainlength||Chain.Count==0)
                {
                    //addbonus
                    //reset chain
                    GameMaster.instance.LC.FoodEaten(chainbonus);
                    active = false;
                    ChainFinished();
                }
            }
            else
            {
                active = false;
                //end chain
                ChainFinished();
            }
        }
    }
    public void ChainFinished()
    {
        GameMaster.instance.UI.toggle(2, false);
        chainstreak = 0;
        activeLength = 0;
        Chain.Clear();
        Debug.Log("chained ended");
    }
    public bool ChainRequest(GameObject GO)
    {
        if (active && activeLength < chainlength)
        {
            Chain.Add(GO);
            GO.GetComponentInChildren<Renderer>().material = chainMat;
            //GO.GetComponent<MeshRenderer>().materials[0] = GameMaster.instance.CM.chainMat;
            GO.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0] = GameMaster.instance.CM.chainMat;
            GO.gameObject.name = "chained";
            Debug.Log(GO.name + " ADD TO CHAIN");

            activeLength++;
            return true;
        }

        else
            return false;
    }
}
