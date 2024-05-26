using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Charecter
{
    public float Totalworkdone;
    public float TickPerElf = 1;
    public int ElvesWorking
    {
        get
        {
            return workingelves.Count;
        }
        set
        {
            // do nothing maybe add elves?
        }
    }
    private float progress=0;
    /// <summary>
    /// chance for one elf to wander every second
    /// </summary>
    public float wanderchance = .1f;
    private float wandertimer = 0f;
    public List<GameObject> elves;
    public List<GameObject> workingelves;
    // Start is called before the first frame update
    void Start()
    {
        wandertimer = Time.time;
        workingelves = elves;
    }

    // Update is called once per frame
    void Update()
    {
        progress += (TickPerElf * ElvesWorking * Time.deltaTime);
        Game.instance.Resource += Mathf.FloorToInt(progress) ;
        Totalworkdone += Mathf.FloorToInt(progress);
        progress -= Mathf.FloorToInt(progress);

        if(Time.time> wandertimer+1)
        {
            wandertimer = Time.time;
            if (ElvesWorking > 0)
            {
                if (Random.value <= wanderchance)
                {
                    int index = Random.Range(0, workingelves.Count);
                    workingelves[index].GetComponent<ElfController>().Wander();
                    workingelves.RemoveAt(index);
                }
            }
        }
    }
    public override void Death()
    {
        Totalworkdone -= 2;
        if (Totalworkdone < 0)
        {
            Game.instance.GameLost();
        }
    }
}
