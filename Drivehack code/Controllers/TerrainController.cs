using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public float extradistanceoffset = 0f;
    public GameObject[] TerainPrefabs;
    public GameObject Pathprefab;
    public List<GameObject> ActiveTerrain;
    public GameObject[] ScenaryPrefabs;
    public List<GameObject> ActiveScenary;
    public float scenarySpeed = 1f;
    public List<GameObject> Paths;
    public float ScenaryLength;
    public float activeLength;
    public float y = 0f;
    public float x = 0f;

    public float speed = 1f;
    private int prefabno = 0;
    private int scenaryPrefabNo = 0;

    public List<GameObject> levelobjects;
    // Start is called before the first frame update
    void Start()
    {
        speed = GameMaster.instance.LC.speedMod;
        NewTerrain();
    }

    // Update is called once per frame
    void Update()
    {

        //foreach (GameObject g in ActiveTerrain)
        if (GameMaster.instance.LC.LevelPlay)
        {
            //Terrain
            for (int i = 0; i < ActiveTerrain.Count; i++)
            {


                ActiveTerrain[i].transform.position = new Vector3(x, y, (ActiveTerrain[0].transform.position.z + ActiveTerrain[i].GetComponent<Terrain>().Length * i) - GameMaster.instance.LC.speedMod * Time.deltaTime);
                //ActiveTerrain[i].transform.position += new Vector3(0, 0, -GameMaster.instance.LC.speedMod * Time.deltaTime);
                //move terrain
                float glength = ActiveTerrain[i].GetComponent<Terrain>().Length;
                if (ActiveTerrain[i].transform.position.z + glength / 2 < GameMaster.instance.cam.transform.position.z)
                {
                    activeLength -= glength;
                    //why is this trying to destroy prefab
                    Destroy(ActiveTerrain[i]);
                    ActiveTerrain.RemoveAt(i);
                }
            }
            if (activeLength <= GameMaster.instance.spawnOffset + extradistanceoffset)
            {
                LoadToOffeset();
            }

            //Scenary
            for (int i = 0; i < ActiveScenary.Count; i++)
            {
               ActiveScenary[i].transform.position = new Vector3(x, y, (ActiveScenary[0].transform.position.z + ActiveScenary[i].GetComponent<Terrain>().Length * i) -  GameMaster.instance.LC.speedMod * scenarySpeed * Time.deltaTime);
                //ActiveTerrain[i].transform.position += new Vector3(0, 0, -GameMaster.instance.LC.speedMod * Time.deltaTime);
                //move terrain
                float glength = ActiveScenary[i].GetComponent<Terrain>().Length;
                if (ActiveScenary[i].transform.position.z + glength / 2 < GameMaster.instance.cam.transform.position.z)
                {
                    ScenaryLength -= glength;
                    //why is this trying to destroy prefab
                    Destroy(ActiveScenary[i]);
                    ActiveScenary.RemoveAt(i);
                }
                if (ScenaryLength <= GameMaster.instance.spawnOffset + extradistanceoffset)
                {
                    LoadScenaryToOffeset();
                }
            }
        }

        // check if terrain off camera
        //check if need to load more terrain
    }

    public void NewTerrain()
    {
        //GameObject tempath = Instantiate<GameObject>(Pathprefab);
        //tempath.transform.position = new Vector3(x, y, 0);
        //Paths.Add(tempath);
        GameObject tempob = Instantiate<GameObject>(TerainPrefabs[0]);
        Debug.Log("terrain 0");
        tempob.transform.position = new Vector3(x, y, 0);
        activeLength += tempob.GetComponent<Terrain>().Length;
        ActiveTerrain.Add(tempob);
        LoadToOffeset();
        LoadScenaryToOffeset();
    }
    public void LoadScenaryToOffeset()
    {
        //get end pos of current active terrain
        float length = ScenaryLength;
        while (length <= GameMaster.instance.spawnOffset && ScenaryPrefabs.Length > 0)
        {


            GameObject tempob = Instantiate<GameObject>(ScenaryPrefabs[scenaryPrefabNo]);
            if (ActiveScenary.Count == 0)
                tempob.transform.position = new Vector3(x, y, tempob.GetComponent<Terrain>().Length);
            else
                tempob.transform.position = new Vector3(x, y, ActiveScenary[ActiveScenary.Count - 1].transform.position.z + tempob.GetComponent<Terrain>().Length);

            length += tempob.GetComponent<Terrain>().Length;
            ActiveScenary.Add(tempob);

            scenaryPrefabNo++;
            if (scenaryPrefabNo >= ScenaryPrefabs.Length)
            {
                scenaryPrefabNo = 0;

            }
            //Debug.Log("scenary spawn");
        }
        //while terrain length <= offsetpoint
        //randomly pick terrain fron list
        //spawn, ad to active
        //ad length to terrain length
        ScenaryLength = length;
    }
    public void LoadToOffeset()
    {
        //get end pos of current active terrain
        float length = activeLength;
        while (length<= GameMaster.instance.spawnOffset)
        {

            
            GameObject tempob = Instantiate<GameObject>(TerainPrefabs[prefabno]);
            if(ActiveTerrain.Count == 0)
                tempob.transform.position = new Vector3(x, y,tempob.GetComponent<Terrain>().Length);
            else
             tempob.transform.position = new Vector3(x, y, ActiveTerrain[ActiveTerrain.Count-1].transform.position.z+ tempob.GetComponent<Terrain>().Length);

            length += tempob.GetComponent<Terrain>().Length;
            ActiveTerrain.Add(tempob);

            prefabno++;
            if(prefabno >= TerainPrefabs.Length)
            {
                prefabno = 0;
                
            }
           // Debug.Log("terrain spawn");
        }
        //while terrain length <= offsetpoint
        //randomly pick terrain fron list
        //spawn, ad to active
        //ad length to terrain length
        activeLength = length;
    }
    public void DestroyLevelElements()
    {
        //levelobjects
        //empty levelobjects
    }
    public void DumpActive(int gap)
    {
        //while (ActiveTerrain.Count > 0+gap)
        //{
        //    Destroy( ActiveTerrain[ActiveTerrain.Count - 1]);
        //    ActiveTerrain.RemoveAt(ActiveTerrain.Count - 1);

        //}
        Debug.Log(ActiveTerrain.Count);
        //activeLength = 0;
        while (levelobjects.Count > 0 + gap)
        {
            Destroy(levelobjects[levelobjects.Count - 1]);
            levelobjects.RemoveAt(levelobjects.Count - 1);

        }
        Debug.Log(levelobjects.Count);
       
    }


}
