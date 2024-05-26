using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public bool test = false;
    public GameSettings GS;
    public Ability DragonAbility;

    public int versionNO = 01;
    public bool mathBufferForSpawn = false;

    public Camera cam;
    public static GameMaster instance;
    public LevelController LC;
    public UIController UI;
    public Effects Effects;
    public Dragon dragon;
    public Stats stats;
    public CameraController CamC;
    public Lives life;
    public ChainManager CM;
    public GameObject gameBounds;
    public Vector2 topLeftBound;
    public Vector2 size;
    public Transform spawnMarker;
    public float spawnOffset = 0f;
    public Level testlevel;
    public List<Level> Levels;
    /// <summary>
    /// needs to be loaded and saved
    /// </summary>
    public List<int> levelcompletes;
    public bool resetSave =false;
    // Start is called before the first frame update


    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            // Destroy(this.gameObject);
            //temp TODO REPLACE to enable quick restart 
            Destroy(instance.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        if (gameBounds != null)
        {
            Bounds tempbounds = gameBounds.GetComponent<Collider>().bounds;
            size = tempbounds.size;
            topLeftBound = new Vector2(tempbounds.min.x, tempbounds.max.y);
        }
        if (cam != null)
            cam = Camera.main;
        if (spawnMarker != null)
            spawnOffset = transform.position.z;

        //load data or reset
        if (resetSave)
            this.GetComponent<Stats>().ResetData();
        this.GetComponent<Stats>().LoadStats();
    }
    void Start()
    {
        Debug.Log("version is "+ versionNO);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LevelExit()
    {
        //save data //fix type issue will loose fraction of food over levels
        stats.UpdateStats((int)LC.foodEaten,LC.distanceTraveled);
        //unload level
        LC.ResetLevel();
        UI.SwitchPanels(1);
    }
   /// <summary>
   /// debug tool, changes colour of dragon object when called
   /// </summary>
    public void touchcheck()
    {
        var block = new MaterialPropertyBlock();
        block.SetColor("_BaseColor", Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));
        dragon.gameObject.GetComponent<Renderer>().SetPropertyBlock(block);
    }
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ResetRun()
    {
       this.GetComponent<TerrainController>().DumpActive(0);
        //set level to 1
    }
}
