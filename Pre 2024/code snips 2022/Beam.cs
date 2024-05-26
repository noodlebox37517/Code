using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    // TODO auto layer detection
    public LineRenderer LR;
    public bool playerowned = true;
    public float lifetime = 1f;
    private float starttime = 0f;
    public int arcLength = 45;

    public float damagePerSecond = 1;

    public float beamrange =5f;
    public float traverseSpeed = 1f;
    public float curAngle = 0f;
    public Vector3 targdir;
    public LayerMask lm;

    public enum Facing {Forward,Left,Right, Back }
    public Facing facing = Facing.Forward;


    public Vector3 spawnoffset = new Vector3();
    private Vector3 endpoint;
    private Vector3 offset;

    void Start()
    {
        StartBeam();
    }
    private void Update()
    {
        offset = transform.localToWorldMatrix * spawnoffset;
        if (Time.time> starttime + lifetime)
        {
            EndBeam();
        }
        UpdateLine();
    }
    void FixedUpdate()
    {

        targdir = GetDirection(facing);
        curAngle = -(arcLength / 2) +  arcLength * (Time.time - starttime)/lifetime;
        targdir = Quaternion.AngleAxis(-curAngle , Vector3.forward) * targdir;
        //ray on point at time t
        RaycastHit2D hit = Physics2D.Raycast(transform.position + offset, targdir, beamrange,lm);

        if (hit.collider != null)
        {
         
            Debug.DrawLine(transform.position + offset, hit.point, Color.red);
            endpoint = hit.point;
            BeamHit(hit);
        }
        else
        {
            Debug.DrawLine(transform.position + offset, transform.position + offset + (Vector3)(targdir * beamrange) , Color.blue);
            endpoint = transform.position + offset + (Vector3)(targdir * beamrange);
        }
    }

    public void BeamHit(RaycastHit2D h)
    {
        if (!playerowned && h.transform.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit player");
            h.transform.gameObject.GetComponent<GameChar>().Damage(damagePerSecond * Time.fixedDeltaTime);

        }
        else if (playerowned && h.transform.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("hit Enemy");
            h.transform.gameObject.GetComponent<GameChar>().Damage(damagePerSecond * Time.fixedDeltaTime);
        }
        else if (playerowned && h.transform.gameObject.CompareTag("Enviroment"))
        {
            Debug.Log("hit enviroment");
            h.transform.gameObject.GetComponent<GameChar>().Damage(damagePerSecond * Time.fixedDeltaTime);
        }
    }
    public Vector3 GetDirection(Facing face)
    {
        Vector3 dir = transform.up;
        switch (face)
        {
            case Facing.Forward:
                dir = transform.up;
                break;
            case Facing.Left:
                dir = -transform.right;
                break;
            case Facing.Right:
                dir = transform.right;
                break;
            case Facing.Back:
                dir = -transform.up;
                break;
        }

        return dir;
    }
    public void StartBeam()
    {
        starttime = Time.time;
        if (this.GetComponent<LineRenderer>() == null)
        {
            this.gameObject.AddComponent<LineRenderer>();
        }
        LR = this.GetComponent<LineRenderer>();
        

        endpoint = transform.position ;
    }
    public void EndBeam()
    {
        Destroy(LR);
        Destroy(this);
    }
    public void UpdateLine()
    {
        
        LR.SetPositions(new Vector3[] {transform.position + offset, endpoint});
    }
}
