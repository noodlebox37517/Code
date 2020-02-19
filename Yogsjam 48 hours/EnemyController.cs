using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Charecter
{
    public bool isBoss = false;
    public float despawn = 4f;
    private float deadtime = 0;
    public Animator animator;
    public GameObject Target;
    public EnemyModel model;
    public float speedMulti  =1f;
    public Transform spawnpoint;

    private float lastAttack = 0f;
    public bool attack = false;

    private float targettimer = 0f;
    public float checktime = 2;

    // Start is called before the first frame update
    void Start()
    {
        health.CurrentHealth = model.MaxHealth;
        health.maxHealth = model.MaxHealth;
        if (model== null)
        {
            Debug.Log(" WARNING Model unassigned");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health.alive)
        {


            if (Target == null|| Time.time> targettimer+checktime)
            {
                //find new target
                Target = FindNewTarget();
                targettimer = Time.time;
            }
            Debug.DrawLine(transform.position, Target.transform.position, Color.red);
            float tardist = (transform.position - Target.transform.position).magnitude;
            //Debug.Log(tardist);
            Vector3 tardir;
            tardir = Target.transform.position - transform.position;
            tardir.Set(tardir.x, 0f, tardir.z);
            tardir.Normalize();


            transform.rotation = Quaternion.LookRotation(tardir);
            if (tardist <= model.AttackRange)
            {
                this.GetComponent<Rigidbody>().velocity = new Vector3();
                //animator.SetBool("attack", true);
                Attack(Target);
                if(animator!= null)
                {
                    animator.SetTrigger("atack");
                }
                //is target in optimal range, else move closer
                if (tardist > model.StandoffRange)
                {
                    Move();
                }
            }
            else
            {
                //animator.SetBool("attack", false);
                Move();
            }

        }
        else
        {
            if (Time.time> deadtime + despawn)
            {
                base.Death();
            }
        }
    }

    public GameObject FindNewTarget()
    {
        if( (Game.instance.santa.gameObject.transform.position- this.transform.position).magnitude > model.chaserange)
        {
            return Game.instance.building.gameObject;
        }
        return Game.instance.santa.gameObject;
    }
    public void Move()
    {


        //get target direction
        //look in direction


        //TODO smooth out turn speed ETC
        //move step
        //this.GetComponent<Rigidbody>().velocity = dir * ((speed * speedMulti));
        if (animator != null && isBoss)
        {
            animator.SetTrigger("move");
        }
        this.GetComponent<Rigidbody>().velocity = transform.forward * (model.Speed * speedMulti);
       // transform.position += transform.forward*(model.Speed* speedMulti * Time.deltaTime );

    }
    public void Attack(GameObject targ)
    {
        //animator.SetBool("attack", true);
        if (Time.time - lastAttack > model.attackCD)
        {
            lastAttack = Time.time;
            model.Attack(gameObject, spawnpoint, targ);
        }
    }
    public override void Death()
    {
        //nimator.SetBool("attack", false);
        if (isBoss)
        {
            Game.instance.GameWin();
        }
        Game.instance.wave.mobs.Remove(this.gameObject);
        this.GetComponent<Rigidbody>().isKinematic = false;
        this.GetComponent<Rigidbody>().freezeRotation = false;
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        this.GetComponent<Rigidbody>().velocity = new Vector3();
        health.alive = false;
        deadtime = Time.time;
        Waitfordeath();
        
        

    }
    IEnumerator WaitAndPrint()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(5);
        
    }
    IEnumerator Waitfordeath()
    {
        yield return StartCoroutine("WaitAndPrint");
        base.Death();
    }

}
