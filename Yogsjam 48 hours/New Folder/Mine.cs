using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public bool targetinrange = false;
    public bool live = true;
    public GameObject explosion;

    private void Start()
    {
        
        this.GetComponentInParent<Minefield>().Mines.Add(this.gameObject);
        if (!live)
        {
            this.GetComponentInParent<Minefield>().InactiveMines.Add(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        //    if(targetinrange)
        //    {
        //        //increase tic rate
        //    }
        //    if ()
        //    {
        //        explode;
        //    }
        //
    }

    public void Explode()
    {
        GameObject exp = Instantiate<GameObject>(explosion, this.transform.position, Game.instance.santa.transform.rotation);
        exp.GetComponent<Explosion>().dmg = (int)this.GetComponentInParent<Minefield>().Damage;
        this.GetComponentInParent<Minefield>().InactiveMines.Add(this.gameObject);
        this.gameObject.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log("BOOM!");
            targetinrange = true;
            Explode();
        }
            //add to disbled list
            //Destroy(this.gameObject);
    }
}
