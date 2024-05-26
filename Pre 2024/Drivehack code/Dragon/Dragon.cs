using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    public Animator anim;
    public Transform anchor;
    public bool Immortal = false;
    private bool _invulnerable = false;
        public bool invulnerable
    {
        get
        {
            if (_invulnerable)
            {
                return _invulnerable;
            }
            else if (shield > 0)
            {
                shield--;
                return true;

            }
            return _invulnerable;

        }
        set
        {
            _invulnerable = value;
        }
    }
    public int maxShield = 1;
    private int _shield = 0;
    public int shield
    {
        get
        {
            return _shield;

        }
        set
        {
            if (value <= 0)
            {
                GameMaster.instance.UI.Shield(false);
                //TODO end effect
            }
            else
            {
                GameMaster.instance.UI.Shield(true);
            }
            _shield = Mathf.Min(value,maxShield);
        }
    }
    public int maxEnergy = 100;
    public int Energy = 100;
    public float ticktimer = 1f;
    public int ePerTick = 1;
    private IEnumerator coroutine;

    public void Awake()
    {
        coroutine = WaitAndPrint(ticktimer);
        StartCoroutine(coroutine);
        //anim = GetComponent<Animator>();
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Tick();
        }
    }
    public void Tick()
    {
        if (GameMaster.instance.LC.LevelPlay)
        {
            if(Energy< maxEnergy)
            {
                Energy += ePerTick;
                Energy = Mathf.Min(Energy,maxEnergy);
            }
        }
    }

    public void Slide(int dir)
    {
        Debug.Log("dir is "+ dir);
        switch (dir)
        {
            case -1:
                //left
                anim.SetTrigger("left");
                break;
            case 1:
                //right
                anim.SetTrigger("right");
                break;

        }
    }
    public void TakeEnergy(int cost)
    {
        Energy -= cost;
        if (Energy < 0)
        {
            Energy = 0;
        }
        else if(Energy> maxEnergy)
        {
            Energy = maxEnergy;
        }
    }

    public bool UseEnergy(int cost)
    {
        if (Energy - cost >= 0)
        {
            Energy -= cost;
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //check if it is terrain
        if (other.tag == "Terrain")
        {
            //hit terrain call
        }
        else if (other.GetComponent<FoodData>()!= null )
        {
            GameMaster.instance.LC.StreakAdd();
            other.GetComponent<CollidableObject>().Contact();
            other.GetComponent<FoodData>().CheckLocal(true);
            
            other.gameObject.GetComponent<CollidableObject>().DestroySelf();
            //Destroy(other.gameObject);
            //Debug.Log(food + " eaten");
        }
        else {
            other.GetComponent<CollidableObject>().Contact();
            other.gameObject.GetComponent<CollidableObject>().DestroySelf();
        }
    }
    public void Damage()
    {
        GameMaster.instance.LC.StreakReset();
        if (!Immortal)
        {
            
            GameMaster.instance.life.LifeTake();
        }
        anim.SetTrigger("crash");
    }
    public void Invulnerable(float bt)
    {
        StartCoroutine(Invul(1f));
    }
    
    IEnumerator Invul(float bt)
    {
        GameMaster.instance.dragon.invulnerable = true;
        GameMaster.instance.UI.notifactionText.text = "Block";
        yield return new WaitForSeconds(bt);
        GameMaster.instance.dragon.invulnerable = false;
        GameMaster.instance.UI.notifactionText.text = "Block off";
    }
}
