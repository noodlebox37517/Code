using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santa : Charecter
{
    private bool alternate = false;
    public GameObject santaRig;
    public Transform wepspawn;
    public GameObject SantaObject;
    public int maxHealth = 100;
    public SanatModel model;

    [Header("Movement")]
    public float speed = 0.1f;
    public float speedMulti = 1f;
    private Vector3 heightoffset;

    public GameObject carrying;
    public Transform carrypos;

    private float lastAttack = 0f;
    // Start is called before the first frame update
    void Start()
    {
        heightoffset = new Vector3(0f,0f, 0f);
        SantaObject = this.gameObject;
        health.CurrentHealth = maxHealth;
        health.maxHealth = maxHealth;

        //spawn rig
        santaRig = LoadSantaModel();

    }

    private void FixedUpdate()
    {
        Vector3 dir = new Vector3();
        if (Input.GetKey("w"))
        {
            dir += Vector3.forward;
        }
        if (Input.GetKey("s"))
        {
            dir += Vector3.back;
        }
        if (Input.GetKey("a"))
        {
            dir += Vector3.left;
        }
        if (Input.GetKey("d"))
        {
            dir += Vector3.right;
        }

        dir.Normalize();

        this.GetComponent<Rigidbody>().velocity = dir * ((speed * speedMulti));
    }
    // Update is called once per frame
    void Update()
    {
        Game.instance.CV.GetComponent<Fillcontroller>().sliders[0].value = health.CurrentHealth / maxHealth;




        //LOOK direction

        Vector3 tardir = new Vector3();
        Vector3 mousepos = Game.instance.GetMouse();
        
       // mousepos.Set(mousepos.x, 0f, mousepos.z);
        if (mousepos != tardir)
        {
            tardir =  mousepos - transform.position;
            tardir.Set(tardir.x, 0f, tardir.z);
            tardir.Normalize();


            transform.rotation = Quaternion.LookRotation(tardir);
            //transform.LookAt(mousepos + heightoffset);
            Debug.DrawLine(transform.position, mousepos , Color.green);
        }

        // MOVEMENT
        Vector3 dir = new Vector3();
        if (Input.GetKey("w"))
        {
            dir += Vector3.forward;
        }
        if (Input.GetKey("s"))
        {
            dir += Vector3.back;
        }
        if (Input.GetKey("a"))
        {
            dir += Vector3.left;
        }
        if (Input.GetKey("d"))
        {
            dir += Vector3.right;
        }

        dir.Normalize();

        //this.GetComponent<Rigidbody>().velocity = dir * ((speed * speedMulti));
        transform.position += (dir * ((speed * speedMulti * Time.deltaTime)));
        // movement
        // actions
        if (Input.GetMouseButton(0))
        {
            if (Time.time - lastAttack > model.Attack.CD)
            {
                if (santaRig.GetComponent<SantaLocalmodel>().animator != null)
                {
                    santaRig.GetComponent<SantaLocalmodel>().animator.SetTrigger("atack");
                }
                Vector3 forward = tardir = mousepos - wepspawn.position;
                forward.Set(tardir.x, 0f, tardir.z);
                forward.Normalize();
                lastAttack = Time.time;
                //last minute code for dualk guns
                Transform wepform = wepspawn;
                if (model.Attack.duals)
                {
                    if (alternate)
                    {
                        wepform = santaRig.GetComponent<SantaLocalmodel>().Weaponspawntwo;
                        
                    }
                    alternate = !alternate;
                }
                
                model.Attack.Attack(this.gameObject, wepform, wepform.transform.forward);
            }
            
 
        }
        if (Input.GetKeyDown("e"))
        {
            if (carrying == null)
            {
                Game.instance.inter.Interact();
            }
            else
            {
                if (carrying.GetComponent<Placable>())
                {
                    //carrying.GetComponent<Interactable>().Remove();
                   if (carrying.GetComponent<Placable>().Place())
                    {
                        carrying = null;
                    }
                }
                //attempt place
            }
        }
    }

    public override void Death()
    {
        Debug.Log("santa death");
        Game.instance.GameLost();
       
    }
    public GameObject AcceptGift()
    { GameObject val = carrying;
        carrying.transform.parent = null;
        carrying = null;
            return val;
    }

    public bool Pickup(GameObject obj)
    {
        if (carrying == null)
        {
            carrying = obj;
            carrying.transform.position = carrypos.position;
            carrying.transform.parent = this.gameObject.transform;
            return true;
        }
        return false;

    }
    public GameObject LoadSantaModel()
    {
        if (santaRig != null)
        {
            Destroy(santaRig);
            wepspawn = null;
        }

        GameObject santamod = Instantiate<GameObject>(model.Santaprefab,transform.position,transform.rotation,this.transform);
        wepspawn = santamod.GetComponent<SantaLocalmodel>().Weaponspawn;


        return santamod;
    }

    public void SwapModel(SanatModel newmodel)
    {
        model = newmodel;
        santaRig = LoadSantaModel();

    }
}
