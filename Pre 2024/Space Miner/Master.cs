using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : MonoBehaviour {

    public bool Testmode;
    public levelTesting testcript;
    public bool load = true;

    public class HookValue
    {
        public HookType type;
        public bool active = false;
    }
    public static Master instance;
    public bool pause;
    public GameObject Targeterobj;
    public enum HookType { Grapple = 0, TractorBeam = 1, Teleport = 2 }
    public HookType hook = HookType.Grapple;
    public List<HookValue> hooks;
    public int hooknum = 0;
    public Vector3 collectorpoint;
    public int currentLevel = 0;
    public GameObject LoadedLevel;

    public Resources sources;
    public EndGameStats statistics;
    public SoundMaster SM;
    public bool grabbed = false;

    private int _lvlmoney = 0;
    public int levelMoney
    {
        get
        {
            return _lvlmoney;


        }
        set
        {
            _lvlmoney = value;
            TextUpdate(2, _lvlmoney.ToString());
        }
    }
    public int masterMoney = 0;
    

    [Header("Options")]
    public bool controlbuttons = false;
    public bool FireButtons = false;
    public bool sliderMove = false;

    public bool destoryOnTouch = false;
    // Use this for initialization
    void Awake()
    {
        if (instance)
        {

        }

        else
        {

            instance = this;
        }

        collectorpoint = Targeterobj.transform.position;
        if (pause)
        {
            Time.timeScale = 0f;
        }

        sources = this.GetComponent<Resources>();


    }
    void Start() {
        statistics = this.GetComponent<EndGameStats>();
        SM = this.GetComponent<SoundMaster>();

    HookValue temphook = new HookValue();
        temphook.type = HookType.Grapple;
        hooks = new List<HookValue>();
        hooks.Add(temphook);

        //set start cash

        levelMoney = 0;
        //temp level start
        if (Testmode)
        {
            //StartLVL(false);
            testcript = this.GetComponent<levelTesting>();
            LoadedLevel = testcript.testlevel;
            if (LoadedLevel.GetComponent<Map>() != null)
            {
                LoadedLevel.GetComponent<Level>().UnPackLVL();
            }
            //Load UI
            UI_Control.instance.PanelSwitch(UI_Control.ActiveUI.LvlPanel);
            //unpause
            Targeterobj.GetComponent<Targeter>().currentAngle = 0f;
            Targeterobj.GetComponent<Targeter>().dir = -1;
            Targeterobj.GetComponent<Targeter>().ResetTargeter();
            PauseLVL(!pause);
            ApplyAttributes();

        }
        if (load)
            LoadGame();

        if (currentLevel > 0)
            //set main menu to continue show
            UI_Control.instance.menuMain.GetComponent<MainMenu>().contBut.SetActive(true);


    }

    // Update is called once per frame
    void Update() {

    }

    public void TextUpdate(int id, string newtext)
    {
        if (UI_Control.instance != null)
            UI_Control.instance.textchange(id, newtext);
    }
    public void CollectGrabbable(Grabbable_Body gb)
    {
        //check effect
        //add value to stock pile
        levelMoney += gb.grabValue;
        if (LoadedLevel.GetComponent<RoidCounter>()!= null)
        LoadedLevel.GetComponent<RoidCounter>().AddCollected(gb.gameObject);
        gb.GetComponent<Grabbable_Body>().RemoveSelf();
        UI_Control.instance.SpawnGainUIElement(UI_Control.instance.Gainprefab, gb.grabValue);
        Master.instance.SM.PlaySoundMenu(4);
        //delete object/crumble effect
    }

    public void HookReturn(int hooknum)
    {
        hooks[hooknum].active = false;
    }

    public void Release(GameObject grabbed)
    {
        if (destoryOnTouch) {
            if (!pause)
            {


                if (grabbed.GetComponent<Grabbable_Body>().releaseEffect != null)
                {
                    Instantiate<GameObject>(grabbed.GetComponent<Grabbable_Body>().releaseEffect, grabbed.transform.position, Quaternion.identity);
                }
                grabbed.GetComponent<Grabbable_Body>().hook.QuickRelease();
            }
        }
    }
    public void Targeted()
    {
        //luanch hook obj
        //check hook available
        if (!pause)
            if (hooks.Count > 0) {

                bool available = false;
                int i = 0;

                while (available == false && i < hooks.Count)
                {
                    if (!hooks[i].active)
                    {
                        available = true;
                    }
                    else
                    {
                        i++;
                    }
                }

                if (available)
                {
                    // give launch number
                    hooks[i].active = true;

                    Targeter tempscript = Targeterobj.GetComponent<Targeter>();
                    switch (hooks[i].type)
                    {
                        case HookType.Grapple:
                            Vector3 launchpos = tempscript.currentPos;
                            float launchrot = tempscript.currentAngle;
                            GameObject hookobj = Instantiate(this.GetComponent<Resources>().Hook, launchpos, Quaternion.identity);
                            hookobj.transform.eulerAngles = new Vector3(0f, 0f, -launchrot);
                            hookobj.GetComponent<Rigidbody2D>().velocity = hookobj.transform.up * hookobj.GetComponent<Hook>().Hookspeed;
                            hookobj.GetComponent<Hook>().hookNum = i;
                            Targeterobj.GetComponent<Targeter>().CommenceHookTrack(hookobj);
                            //platluanch sound
                            SM.PlaySoundMenu(2);


                            break;

                        default:

                            break;
                    }
                }
            }

    }
    public void StartLVL(bool endless)
    {
        ApplyAttributes();
        if (!endless)
        {
            //check there is a next level
            if (currentLevel < this.GetComponent<Levels>().Levelslist.Count)
            {
                LoadedLevel = this.GetComponent<Levels>().loadlvl(currentLevel);
               
               
                Targeterobj.GetComponent<Targeter>().currentAngle = 0f;
                Targeterobj.GetComponent<Targeter>().dir = -1;
                Targeterobj.GetComponent<Targeter>().ResetTargeter();
                UI_Control.instance.knob.GetComponent<KnobTargeter>().Reset();


                //Load UI
                UI_Control.instance.startPanel.GetComponent<UI_StartPanel>().LoadLevelData(LoadedLevel.GetComponent<Level>()); 
                UI_Control.instance.PanelSwitch(UI_Control.ActiveUI.startPanel);
                //load panel
                //panel switch start level

                //PauseLVL(!pause);
                
            }
            else
            {
                Debug.LogError(" level " + currentLevel + " does not exist");
            }
        }
        else
        {

        }
    }
    public void PauseLVL(bool tarstate)
    {
        pause = tarstate;

        if (tarstate)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    public void LevelEnded(bool lvlpassed, int cashgained)
    {
        PauseLVL(true);
        for (int i = 0; i < LoadedLevel.GetComponent<Map>().LiveBodies.Count; i++)
        {
            Destroy(LoadedLevel.GetComponent<Map>().LiveBodies[i]);
        }

        for (int i = 0; i < Targeterobj.GetComponent<Targeter>().activeHooks.Count; i++)
        {
            Targeterobj.GetComponent<Targeter>().activeHooks[0].GetComponent<Hook>().HookRemove();
            Targeterobj.GetComponent<Targeter>().activeHooks.RemoveAt(0);
            Targeterobj.GetComponent<Debug_Tageter>().RemoveShotLine(0);
        }

        if (lvlpassed)
        {
            Debug.Log("YOU WIN");
            masterMoney += cashgained;
            //count cash gained
            statistics.totalCash += cashgained;
            RoidCounter rc = LoadedLevel.GetComponent<RoidCounter>();
            statistics.orescollected += rc.roidCount;
            statistics.bombesExploded += rc.bombCount;
            statistics.oresenriched += rc.oresEnriched;

            Destroy(LoadedLevel);
            //goto upgrade screen
            currentLevel++;
            if (currentLevel >= this.GetComponent<Levels>().Levelslist.Count)
            {
                UI_Control.instance.Gameover();
            }
            else
            {
                UI_Control.instance.LevelOver(true);
                
            }
            SaveGame(false,true);
            

        }
        else
        {

            Debug.Log("YOU LOOSE");
            Destroy(LoadedLevel);
            //goto upgrade screen
            UI_Control.instance.LevelOver(false);
           // this.GetComponent<AdManager>().PlaySkipable();
        }
        //reset level cash
        Master.instance.levelMoney = 0;
    }
    public void DebugResetlevels()
    {
        currentLevel = 0;
    }
    public void LoadGame()
    {
        SaveLoadMaster.ProgressData pdata = SaveLoadMaster.LoadProgress();
        currentLevel = pdata.curLevel;
        masterMoney = pdata.cashamount;
        this.GetComponent<Upgrades>().SetSavedGrades(pdata.boostUpgrades, pdata.activeUpgrades);
        this.GetComponent<EndGameStats>().UnpackStats(pdata.gameStats);




    }
    public void SaveGame(bool upgrades)
    {

        if (!upgrades)
            SaveLoadMaster.SaveProgress(currentLevel, masterMoney);
        else {
            Upgrades ups = this.GetComponent<Upgrades>();
            SaveLoadMaster.SaveProgress(currentLevel, masterMoney, new int[3]{ ups.aimSpeedLVL,ups.fireSpeedLVL,ups.pullPowerLVL }, new bool[6]{ups.Hooksize, ups.scannerUpgrade, ups.aimUpgrade, ups.BackGrab , ups.Destroyer, ups.autograb });
        }
    }
    public void SaveGame(bool upgrades, bool stats)
    {
        if (!upgrades)
            SaveLoadMaster.SaveProgress(currentLevel, masterMoney);
        else
        {
            Upgrades ups = this.GetComponent<Upgrades>();
            if (!stats)
            {
                SaveLoadMaster.SaveProgress(currentLevel, masterMoney, new int[3] { ups.aimSpeedLVL, ups.fireSpeedLVL, ups.pullPowerLVL }, new bool[6] { ups.Hooksize, ups.scannerUpgrade, ups.aimUpgrade, ups.BackGrab, ups.Destroyer, ups.autograb });

            }
            else
            {
                EndGameStats gamestats = this.GetComponent<EndGameStats>();
               SaveLoadMaster.SaveProgress(currentLevel, masterMoney, new int[3] { ups.aimSpeedLVL, ups.fireSpeedLVL, ups.pullPowerLVL }, new bool[6] { ups.Hooksize, ups.scannerUpgrade, ups.aimUpgrade, ups.BackGrab, ups.Destroyer, ups.autograb }, new int[] { gamestats.totalCash, gamestats.orescollected, gamestats.bombesExploded, gamestats.oresenriched, gamestats.upgradespurchased });
            }
        }
    }
        public void NewGame()
    {// reset stats
        SaveLoadMaster.SaveProgress(0, 0,new int[3] { 0,0,0}, new bool[6] { false, false, false,false,false,false }, new int[5] { 0,0,0,0,0});
    }
    public void ApplyAttributes()
    {
        // apply base stats and multipliers to game
        Resources tempsource = this.GetComponent<Resources>();
        Upgrades grades = this.GetComponent<Upgrades>(); 

          //apply activated upgrades
        this.GetComponent<Upgrades>().UnloadActives();
        this.GetComponent<Upgrades>().LoadActives();

        //aimtraverse
        Targeterobj.GetComponent<Targeter>().anglePerSec = tempsource.baseAimSpeed + ( tempsource.baseAimSpeed * grades.aimSpeedLVL )/ grades.maxLevel;
        Targeterobj.GetComponent<Targeter>().taranglePerSec = tempsource.baseAimSpeed + (tempsource.baseAimSpeed * grades.aimSpeedLVL) / grades.maxLevel;
        tempsource.Hook.GetComponent<Hook>().Hookspeed = grades.speedmod * (tempsource.baseFireSpeed + (tempsource.baseFireSpeed * grades.fireSpeedLVL) / grades.maxLevel);
        tempsource.Hook.GetComponent<Hook>().pullSpeed = grades.speedmod * tempsource.basePullSpeed + (tempsource.basePullSpeed * grades.fireSpeedLVL) / grades.maxLevel;
        tempsource.Hook.GetComponent<Hook>().baseLoad = tempsource.basePullPower + (tempsource.basePullPower * grades.pullPowerLVL) / grades.maxLevel;



    }

    public void PlayTutlvl()
    {
        LoadedLevel = Instantiate<GameObject>(this.GetComponent<Levels>().tutorial);
        Targeterobj.GetComponent<Targeter>().currentAngle = 0f;
        Targeterobj.GetComponent<Targeter>().dir = -1;
        Targeterobj.GetComponent<Targeter>().ResetTargeter();
        UI_Control.instance.knob.GetComponent<KnobTargeter>().Reset();


        //Load UI
        //UI_Control.instance.startPanel.GetComponent<UI_StartPanel>().LoadLevelData(LoadedLevel.GetComponent<Level>());
        UI_Control.instance.PanelSwitch(UI_Control.ActiveUI.startPanel);

       
    }
    public void FacebookLink()
    {
        Application.OpenURL("https://www.facebook.com/SpaceMiner2018");
    }
    public void AddEnded(int i)
    {
        switch (i)
        {
            case 1:
                NewGame();
                LoadGame();
                StartLVL(false);
                break;
            case 2:
                //upgrade discount
                UI_Control.instance.menuUpgrade.GetComponent<Ui_PanelController>().Panels[1].GetComponent<UpgradeMenu>().ApplyAdDiscount();
                break;
            default:

                break;

        }
        
    }
}
