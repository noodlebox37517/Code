using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    private MapTimeLines timelines;
    [Header("map options")]
    public List<Transform> bodyPHs = new List<Transform>();
    public List<Transform> rightSidebodyPHs = new List<Transform>();
    public List<Transform> leftSidebodyPHs = new List<Transform>();
    public List<GameObject> LiveBodies = new List<GameObject>();

    public GameObject reader;
    public GameObject leftReader;
    public GameObject rightReader;
    public Transform EndofMap;
	public float readerSpeed = 1f;
	public float curReadPos = 0f;
    public float cursideReadPos = 0f;
    public Vector3 topright;
    public int maxValue = 0;
    public int bodyCount = 0;

    //scanner readers




    // Use this for initialization
    void Awake()
	{
        bodyCount = 0;
        if (this.GetComponent<RoidCounter>() == null)
            gameObject.AddComponent<RoidCounter>();
    }
	void Start () {
        topright = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        curReadPos = reader.transform.position.y;
        cursideReadPos = rightReader.transform.position.x;
        int tempcount = bodyPHs.Count;

        //invis greenlines
        if (!Master.instance.Testmode)
        {
            reader.GetComponent<SpriteRenderer>().enabled = false;
            leftReader.GetComponent<SpriteRenderer>().enabled = false;
            rightReader.GetComponent<SpriteRenderer>().enabled = false;
        }

            foreach (Transform t in bodyPHs)
        {
            if (t.GetComponent<BodyPH>() != null)
            {
                maxValue += t.GetComponent<BodyPH>().bodyActual.GetComponent<Grabbable_Body>().grabValue;
                bodyCount++;
            }
            //Debug.Log("count = " + bodyPHs.Count);
            if (t.transform.position.y < curReadPos)
            {
                
                Transform temptransform = t;
                //bodyPHs.Remove(t);
                if (temptransform.position.x < -cursideReadPos)
                {
                    leftSidebodyPHs.Add(temptransform);
                }
                else
                {
                    rightSidebodyPHs.Add(temptransform);

                }
            }
        }
        foreach (Transform t in rightSidebodyPHs)
        {
            bodyPHs.Remove(t);
        }
        foreach (Transform t in leftSidebodyPHs)
        {
            bodyPHs.Remove(t);
        }

        rightSidebodyPHs.Sort((a, b) => a.position.x.CompareTo(b.position.x));
        leftSidebodyPHs.Sort((b, a) => a.position.x.CompareTo(b.position.x));
        bodyPHs.Sort((a, b) => a.position.y.CompareTo(b.position.y));
        EndofMap.position = new Vector3(0f, bodyPHs[bodyPHs.Count - 1].position.y, 0f);

        if (this.GetComponent<Scanner>() != null)
        {
            this.GetComponent<Scanner>().bodyPHs = new List<Transform>(bodyPHs);
            this.GetComponent<Scanner>().rightSidebodyPHs = new List<Transform>(rightSidebodyPHs);
            this.GetComponent<Scanner>().leftSidebodyPHs = new List<Transform>(leftSidebodyPHs);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Master.instance.pause)
        {

            if (bodyPHs.Count <= 0)
            {
               // Debug.Log("Spawner Finished");
                if (LiveBodies.Count == 0)
                {
                   
                    bool passed = GetComponent<Level>().EvaluteLVL(Master.instance.levelMoney);
                    Master.instance.LevelEnded(passed, Master.instance.levelMoney - GetComponent<Level>().cashTarget);
                }
            }
            else {
                //top
                curReadPos += readerSpeed * Time.deltaTime;
               
                reader.transform.position = new Vector3(0f, curReadPos, 0f);
                //sides
                cursideReadPos += readerSpeed * Time.deltaTime;
                leftReader.transform.position = new Vector3(-cursideReadPos, 0f, 0f);
                rightReader.transform.position = new Vector3(cursideReadPos, 0f, 0f);


            }

            
            //top
            while (bodyPHs.Count > 0 && bodyPHs[0].position.y < curReadPos)
            {
                float posy = transform.position.y;
                GameObject tempGO = Instantiate<GameObject>(bodyPHs[0].GetComponent<BodyPH>().bodyActual, new Vector3(bodyPHs[0].position.x, posy, bodyPHs[0].position.z), Quaternion.identity);
                tempGO.GetComponent<Grabbable_Body>().Launch(-bodyPHs[0].transform.up, bodyPHs[0].GetComponent<BodyPH>().speed);
                LiveBodies.Add(tempGO);
                Destroy(bodyPHs[0].gameObject);
                bodyPHs.RemoveAt(0);
            }
            //left
            while (leftSidebodyPHs.Count > 0 && leftSidebodyPHs[0].position.x > -cursideReadPos)
            {
                float posx = topright.x;
                GameObject tempGO = Instantiate<GameObject>(leftSidebodyPHs[0].GetComponent<BodyPH>().bodyActual, new Vector3(-posx, leftSidebodyPHs[0].position.y, leftSidebodyPHs[0].position.z), Quaternion.identity);
                tempGO.GetComponent<Grabbable_Body>().Launch(-leftSidebodyPHs[0].transform.up, leftSidebodyPHs[0].GetComponent<BodyPH>().speed);
                LiveBodies.Add(tempGO);
                Destroy(leftSidebodyPHs[0].gameObject);
                leftSidebodyPHs.RemoveAt(0);
            }
            //right
            while (rightSidebodyPHs.Count > 0 && rightSidebodyPHs[0].position.x < cursideReadPos)
            {
                float posx = topright.x;
                GameObject tempGO = Instantiate<GameObject>(rightSidebodyPHs[0].GetComponent<BodyPH>().bodyActual, new Vector3( posx, rightSidebodyPHs[0].position.y, rightSidebodyPHs[0].position.z), Quaternion.identity);
                tempGO.GetComponent<Grabbable_Body>().Launch(-rightSidebodyPHs[0].transform.up, rightSidebodyPHs[0].GetComponent<BodyPH>().speed);
                LiveBodies.Add(tempGO);
                Destroy(rightSidebodyPHs[0].gameObject);
                rightSidebodyPHs.RemoveAt(0);
            }
            if (this.GetComponent<Scanner>() != null)
            {
                this.GetComponent<Scanner>().cursideReadPos = cursideReadPos;
                this.GetComponent<Scanner>().curReadPos = curReadPos;
            }
        }
    }
    public void RemovePreBodies(float dist)
    {
        while (bodyPHs.Count > 0 && bodyPHs[0].position.y < dist)
        {

            Destroy(bodyPHs[0].gameObject);
            bodyPHs.RemoveAt(0);
        }
        while (leftSidebodyPHs.Count > 0 && leftSidebodyPHs[0].position.x > -cursideReadPos)
        {
            float posx = topright.x;   
            Destroy(leftSidebodyPHs[0].gameObject);
            leftSidebodyPHs.RemoveAt(0);
        }
        //right
        while (rightSidebodyPHs.Count > 0 && rightSidebodyPHs[0].position.x < cursideReadPos)
        {
            float posx = topright.x;
            Destroy(rightSidebodyPHs[0].gameObject);
            rightSidebodyPHs.RemoveAt(0);
        }
    }
}
