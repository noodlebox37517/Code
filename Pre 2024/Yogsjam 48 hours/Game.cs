using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public Canvas CV;
    public bool spawnPres = true;
    public int presentthresold = 100;
    public float deliverytimer =30f;
    private float lastdelivery =0f;
    public float delperwep = 3;
    private float dels = 0;
    public GameObject unlockprefab;
    public Transform unlockspawnpos;
    public GameObject Prez;
    public List<GameObject> houseSpawns;
    public GameObject deliveryHouse;
    public GameObject curdelgo;
    public Transform deliverySpawn;
    private GameObject delpresent;
    public NAvpointer nav;
    public GameObject playertarget;
    public Defenses defs;
    public Building building;
    public Interactables inter;
    public static Game instance;
    public Camera cam;
    public Santa santa;
    public WaveController wave;
    public int Resource = 0;
    public float levelthreshold =500;
    private void Awake()
    {
        defs = this.GetComponent<Defenses>();
        inter = this.GetComponent<Interactables>();
        wave = this.GetComponent<WaveController>();
        santa = FindObjectOfType<Santa>();
        cam = FindObjectOfType<Camera>();
        building = GameObject.FindGameObjectWithTag("Building").GetComponent<Building>();
        if (instance != null && instance != this)
        {
            // Destroy(this.gameObject);
            //temp TODO REPLACE to enable quick restart 
            Destroy(instance.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        wave.LoadNextWave();
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (building.Totalworkdone > levelthreshold)
        {
            //spawn krampus
            if (!this.GetComponent<Endgame>().krampusactive)
                this.GetComponent<Endgame>().spawn();

        }

        CV.GetComponent<Fillcontroller>().sliders[1].value = (building.Totalworkdone / levelthreshold);
       CV.GetComponent<TextController>().texts[0].text = Resource.ToString();

        if (playertarget != null)
        {
            nav.to = playertarget.transform;
        }
        if (Input.GetKeyDown("r"))
        {
            Time.timeScale = 1.0f;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown( KeyCode.Escape))
        {
            Application.Quit();

            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Time.time >= lastdelivery+deliverytimer)
        {
            if (delpresent == null) {
                lastdelivery = Time.time;
                delpresent =  SpawnPresent(Prez, deliveryHouse);

                playertarget = delpresent;
            }
        }
    }

    public void Reset()
    {
        lastdelivery = Time.time;
    }
    public Vector3 GetMouse()
    {
        Vector3 val = new Vector3() ;
        int layerMask = 1 << 8;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), ray.direction, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(cam.ScreenToWorldPoint(Input.mousePosition), ray.direction * hit.distance, Color.green);
            //Debug.Log("Did Hit");
            val = hit.point;
        }
        //draw ray from mousepos on screen 
        // to ground forward from screen
        return val;
    }
    public GameObject SpawnPresent(GameObject prefab,GameObject house)
    {
        curdelgo = Instantiate<GameObject>(house, houseSpawns[Random.Range(0, houseSpawns.Count-1)].transform.position, Quaternion.identity);
        //turn on pointer
       
        //choose house
        return Instantiate<GameObject>(prefab,deliverySpawn.position,Quaternion.identity);
    }
    public void WaveEnded()
    {

    }
    public void GameWin()
    {
        Debug.Log("you Win");
        Time.timeScale = 0.0f;
    }
    public void GameLost()
    {
        Debug.Log("you loose");
        Time.timeScale = 0.0f;
    }
    public void delcomplete()
    {
        //turn off pointer
        dels++;
        if(dels/delperwep == 1)
        {
            dels = 0;
            playertarget = Instantiate<GameObject>(unlockprefab,unlockspawnpos);
        }
    }
}
