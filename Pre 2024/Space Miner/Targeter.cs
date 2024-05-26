using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour {
    public float anglePerSec = 1f;
    public int dir = -1;
    public Transform rightPointForm;
    private Vector2 centerPoint;

    public TargetPointer tarpoint;

    private float radius;
    public float tarAngle =0f;
    public float angledif =0f;
    public Vector3 direction = new Vector3(0f, 0f, 0f);
    public float currentAngle =0f;
    public float angleAccuracy = 1f;
    public float maxAngle =90f;

    public Vector2 currentPos;
    public float collectHookDist = 1f;
    private float hookMaxRange = 5f;

    public List<GameObject> activeHooks;
    public List<float> hookDistance;
    public bool pauseScanner = false;
    public bool grabOnRetract = false;
    public bool drawTarLine = false;
    public GameObject autocollector;
    [Header("Button Control options")]
    public float taranglePerSec = 1f;


    // Use this for initialization
    void Start () {
        radius = Vector2.Distance(transform.position,rightPointForm.position);
        currentPos = (Vector2)transform.position + new Vector2(0, radius);
        centerPoint = (Vector2)transform.position;
        hookMaxRange = Master.instance.GetComponent<Resources>().HookRange;


    }

    //float tarangle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
   // Quaternion angle = transform.rotation = Quaternion.Euler(0f, 0f, tarangle - 90);
    // Update is called once per frame
    void Update () {
        //
        if(pauseScanner&& activeHooks.Count > 0)
        {
            tarpoint.Invis(true);
        }

        else
        {
            if(!tarpoint.spriteVisible)
                tarpoint.Invis(false);


            if (Master.instance.grabbed)
            {
                tarAngle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f) * -1;

            }
            if (tarAngle > maxAngle && tarAngle < 180f) 
            {
            tarAngle = maxAngle;
                //tarAngle = Mathf.Clamp(tarAngle, -maxAngle, maxAngle);
            }
            else if (tarAngle < -maxAngle || tarAngle > 180f)
            {
                tarAngle = -maxAngle;
            }

            angledif = tarAngle - currentAngle;
            if (currentAngle != tarAngle)
            {
                if (angledif > angleAccuracy)
                {
                    dir = 1;
                    currentAngle = currentAngle + (anglePerSec * dir) * Time.deltaTime;
                }
                else if (angledif < -angleAccuracy)
                {
                    dir = -1;
                    currentAngle = currentAngle + (anglePerSec * dir) * Time.deltaTime;
                }
                else
                {
                    currentAngle = tarAngle;
                }


                //max angle check



                float x = centerPoint.x + radius * Mathf.Sin(currentAngle * Mathf.Deg2Rad);
                float y = centerPoint.y + radius * Mathf.Cos(currentAngle * Mathf.Deg2Rad);

                currentPos = new Vector2(x, y);
            }
        }
        if (drawTarLine)
        {
            Vector2 tardir = currentPos - (Vector2)this.transform.position;
            this.GetComponent<LineRenderer>().SetPositions(new Vector3[] { this.GetComponent<Targeter>().currentPos, this.GetComponent<Targeter>().currentPos + (hookMaxRange * tardir.normalized) });
        }
        for (int i = 0; i< activeHooks.Count; i++)
        {
            //check obj not null
            if (activeHooks[i] == null)
            {
                activeHooks.RemoveAt(i);
                hookDistance.RemoveAt(i);
            }
            else
            {
                hookDistance[i] = (activeHooks[i].transform.position - transform.position).magnitude;
                if (hookDistance[i] < collectHookDist)
                {
                    if (activeHooks[i].GetComponent<Hook>().hookedBody != null && activeHooks[i].GetComponent<Hook>().hooked)
                    {
                        //collect ore
                        Master.instance.CollectGrabbable(activeHooks[i].GetComponent<Hook>().hookedBody.GetComponent<Grabbable_Body>());

                        activeHooks[i].GetComponent<Hook>().HookRemove();
                        activeHooks.RemoveAt(i);
                        hookDistance.RemoveAt(i);
                        this.GetComponent<Debug_Tageter>().RemoveShotLine(i);
                    }
                    else
                    {
                        //remove hook /no ore
                        activeHooks[i].GetComponent<Hook>().HookRemove();
                        activeHooks.RemoveAt(i);
                        hookDistance.RemoveAt(i);
                        this.GetComponent<Debug_Tageter>().RemoveShotLine(i);
                    }

                }
                else if (hookDistance[i] >= hookMaxRange) 
                {
                    if (!grabOnRetract)
                    {
                        activeHooks[i].GetComponent<Collider2D>().enabled = !activeHooks[i].GetComponent<Collider2D>().enabled;
                    }
                    activeHooks[i].GetComponent<Hook>().MaxRange();
                }
            }
            //update list
        }
    }
    public void CalcTargetDirection(Vector3 mouspos)
    {
        direction = ( (Vector3)centerPoint - mouspos).normalized;
    }
    public void AttachChain(GameObject hook)
    {
      GameObject tempchain =  Instantiate<GameObject>(Master.instance.sources.HookChain,this.transform.position,Quaternion.identity);
      tempchain.GetComponent<MainChain>().Setup(hook);
        hook.GetComponent<Hook>().Chainmaster = tempchain;
    }
    public void CommenceHookTrack(GameObject hook)
    {

        activeHooks.Add(hook);

        hookDistance.Add((hook.transform.position - transform.position).magnitude);
        this.GetComponent<Debug_Tageter>().ShotLine();
        AttachChain(hook);
        hook.GetComponent<Hook>().backPull = grabOnRetract;

    }
    public void ResetTargeter()
    {

    tarAngle = 0f;
    angledif = 0f;
    direction = new Vector3(0f, 0f, 0f);
    currentAngle = 0f;
        if (tarpoint != null)
        {
        tarpoint.ResetPos();
        }

    }
    public void ChangeTargetAngle(bool right)
    {
        int tempdir = -1;
        if (right)
        {
            tempdir = 1;
        }
        //
        if (activeHooks.Count < 1)
        {
            tarAngle = tarAngle + (taranglePerSec * tempdir) * Time.deltaTime;
        }

    }
    public void ChangeTargetAngle( float val)
    {
        float tempdir = val;

        //
        if (activeHooks.Count < 1)
        {
            tarAngle = tarAngle + (taranglePerSec * tempdir) * Time.deltaTime;
        }
    }
    public void ForceTargetAngle(float newangle)
    {
        tarAngle = newangle;
    }
}
