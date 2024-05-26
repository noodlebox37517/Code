using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public bool useTime =true;
    public float localacceleration = 0f;
    public float totalfoodEaten;
    public float foodEaten = 0;
    public float distanceTraveled = 0;

    public bool randomSpawn = false;
    public bool LevelPlay = false;
    public List<Slate> slates;
    public int levelno = 0;
    public Level level;
    public int food;

    public int foodGoal = 0;
    public int currentSlate = 0;
    public float levelTime;
    public float dist;
    public float levelpos;

    public bool useMulti = false;
    public float basemulti = 0.1f;
    public int Streaks = 0;
    public int streakNo = 10;
    public int streakCount = 0;
    private float lastSpawn = 0;
    private int section = 0;
    public bool randomSections = false;
    public float Accelbrakepoint= 5;
    public float LevelAccel    
    {
        get
        {
           return 1 +      (GameMaster.instance.stats.levelcompletes[levelno] - GameMaster.instance.stats.levelcompletes[levelno] % Accelbrakepoint) / Accelbrakepoint;
        }

    }
    public LevelData LD;


    [Header("test variables")]
    /// <summary>
    /// out of 1 how chance of spawn
    /// </summary>

    /// <summary>
    /// speed per metre
    /// </summary>
    public float mPMin = 10;
    public float speedMod = 1f;

    //Change to QUE
    public List<Row> RowSpawnList;
    public int curRow = 0;

    public List<GameObject> liveLevelObjects;
    public bool advanceSpawn = true;
    public float boonSpawnChance = 1f;
    public float bonusBoon = 0f;
    public float hazardSpawnChance =1f;
    public float bonusHazard = 0f;
    public float rowTickChance = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        if (GameMaster.instance.UI != null)
        {
            GameMaster.instance.UI.GoalUpdate(foodEaten / (float)foodGoal);

        }

    }

    // Update is called once per frame
    void Update()
    {

        if (LevelPlay)
        {

            if (useTime)
            {
                levelTime += Time.deltaTime;
                dist += speedMod * Time.deltaTime;
                //TODO: change to be a more accurate rep of level time instead of just from last frame (Real time puase etc)
                if (randomSpawn)
                {
                    float timetospawn = (currentSlate * (level.slateInterval+ localacceleration));


                    if (levelTime >= timetospawn)
                    {
                        Vector3 tempos = (Vector3)GameMaster.instance.topLeftBound;
                        tempos.z = GameMaster.instance.spawnOffset + currentSlate * (level.slateInterval + localacceleration);
                        if (level.PureRandomInfinte == false || level.PureRandomInfinte == true)
                        {
                            if (currentSlate < slates.Count)
                            {


                                Debug.Log("spawn " + currentSlate);

                                slates[currentSlate].Spawn(tempos);
                                currentSlate++;

                                //if spawn threshold past/read point
                                //spawn next slate at precise position
                                //set speed


                            }
                        }
                        else
                        {



                            //////gen coin
                            ////InputController IC = GameMaster.instance.GetComponent<InputController>();
                            //////ranndom chance spawn, random lane
                            ////int coinpos = -1;
                            ////if (Random.Range(0, (1 / coinSpawnChance)) <= 1)
                            ////{
                            ////    int lane = Random.Range(0, IC.column);
                            ////    coinpos = lane;
                            ////    Vector3 spawnpos = IC.movePositions[lane];
                            ////    spawnpos.z = GameMaster.instance.spawnOffset;
                            ////    GameObject tempGO = Instantiate<GameObject>(level.Coin, spawnpos, Quaternion.identity);

                            ////    //Debug.Log("spawn " + currentSlate);

                            ////}
                            ////if (Random.Range(0, (1 / hazardSpawnChance)) <= 1)
                            ////{
                            ////    int lane = Random.Range(0, IC.column);
                            ////    if (lane != coinpos)
                            ////    {
                            ////        Vector3 spawnpos = IC.movePositions[lane];
                            ////        spawnpos.z = GameMaster.instance.spawnOffset;
                            ////        GameObject tempGO = Instantiate<GameObject>(level.Hazard, spawnpos, Quaternion.identity);
                            ////    }

                            ////}

                            currentSlate++;

                            //if spawn threshold past/read point
                            //spawn next slate at precise position
                            //set speed


                        }
                    }
                }
                else
                {
                    // spawn until next row is beyond offset
                    float timetospawn = (level.slateInterval);
                    // doesnt like fast change
                    //work of time since lastspawn



                    if (lastSpawn >= timetospawn)
                    {
                        Vector3 tempos = (Vector3)GameMaster.instance.topLeftBound;
                        tempos.z = GameMaster.instance.spawnOffset + curRow * (level.slateInterval + localacceleration);
                        if (level.PureRandomInfinte == false || level.PureRandomInfinte == true)
                        {
                            if (curRow < RowSpawnList.Count)
                            {


                                //Debug.Log("spawn row: " + RowSpawnList[curRow].name);
                                // liveLevelObjects.AddRange
                                RowSpawnList[curRow].Spawn(tempos);
                                curRow++;

                                //if spawn threshold past/read point
                                //spawn next slate at precise position
                                //set speed


                            }
                            else
                            {
                                //curRow++;
                                Debug.Log("no rows detected");

                                //TODO section logic get new section
                                int secNO = 0;
                                if (randomSections)
                                {
                                    secNO = Random.Range(0, level.sections.Count);
                                    Debug.Log(level.sections.Count + " Sections");
                                }
                                else
                                {
                                    section++;
                                    //reset
                                    if (section >= level.sections.Count)
                                    {
                                        section = 0;
                                    }
                                    secNO = section;
                                }
                                if (LoadNewSection(secNO))
                                {
                                    Debug.Log(secNO + " Section loaded");
                                }

                            }
                        }
                        lastSpawn = 0f;

                    }
                    else
                    {
                        lastSpawn += Time.deltaTime;// not perfect with puase
                    }
                }

                //update UI
                distanceTraveled = levelTime * speedMod;
                //TODO not accurate
                int temp = (int)(distanceTraveled);
                GameMaster.instance.UI.distText.text = (speedMod.ToString() + " Mbps");
            }
            else
            {   dist += speedMod * Time.deltaTime;
                levelTime += Time.deltaTime;

                //TODO: change to be a more accurate rep of level time instead of just from last frame (Real time puase etc)
                if (randomSpawn)
                {
                    float timetospawn = (currentSlate * (level.slateInterval + localacceleration));


                    if (levelTime >= timetospawn)
                    {
                        Vector3 tempos = (Vector3)GameMaster.instance.topLeftBound;
                        tempos.z = GameMaster.instance.spawnOffset + currentSlate * (level.slateInterval + localacceleration);
                        if (level.PureRandomInfinte == false || level.PureRandomInfinte == true)
                        {
                            if (currentSlate < slates.Count)
                            {


                                Debug.Log("spawn " + currentSlate);

                                slates[currentSlate].Spawn(tempos);
                                currentSlate++;

                                //if spawn threshold past/read point
                                //spawn next slate at precise position
                                //set speed


                            }
                        }
                        else
                        {



                            //////gen coin
                            ////InputController IC = GameMaster.instance.GetComponent<InputController>();
                            //////ranndom chance spawn, random lane
                            ////int coinpos = -1;
                            ////if (Random.Range(0, (1 / coinSpawnChance)) <= 1)
                            ////{
                            ////    int lane = Random.Range(0, IC.column);
                            ////    coinpos = lane;
                            ////    Vector3 spawnpos = IC.movePositions[lane];
                            ////    spawnpos.z = GameMaster.instance.spawnOffset;
                            ////    GameObject tempGO = Instantiate<GameObject>(level.Coin, spawnpos, Quaternion.identity);

                            ////    //Debug.Log("spawn " + currentSlate);

                            ////}
                            ////if (Random.Range(0, (1 / hazardSpawnChance)) <= 1)
                            ////{
                            ////    int lane = Random.Range(0, IC.column);
                            ////    if (lane != coinpos)
                            ////    {
                            ////        Vector3 spawnpos = IC.movePositions[lane];
                            ////        spawnpos.z = GameMaster.instance.spawnOffset;
                            ////        GameObject tempGO = Instantiate<GameObject>(level.Hazard, spawnpos, Quaternion.identity);
                            ////    }

                            ////}

                            currentSlate++;

                            //if spawn threshold past/read point
                            //spawn next slate at precise position
                            //set speed


                        }
                    }
                }
                else
                {
                    // spawn until next row is beyond offset
                    float timetospawn = ((level.slateInterval + localacceleration));
                    // doesnt like fast change
                    //work of time since lastspawn



                    if (lastSpawn >= timetospawn)
                    {
                        //Debug.Log(timetospawn+ " spawn pos");
                        Vector3 tempos = (Vector3)GameMaster.instance.topLeftBound;
                        tempos.z = GameMaster.instance.spawnOffset + curRow * (level.slateInterval + localacceleration);
                        if (level.PureRandomInfinte == false || level.PureRandomInfinte == true)
                        {
                            if (curRow < RowSpawnList.Count)
                            {


                                //Debug.Log("spawn row: " + RowSpawnList[curRow].name);
                                // liveLevelObjects.AddRange
                                RowSpawnList[curRow].Spawn(tempos);
                                curRow++;

                                //if spawn threshold past/read point
                                //spawn next slate at precise position
                                //set speed


                            }
                            else
                            {
                                //curRow++;
                                Debug.Log("no rows detected");

                                //TODO section logic get new section
                                int secNO = 0;
                                if (randomSections)
                                {
                                    secNO = Random.Range(0, level.sections.Count);
                                    Debug.Log(level.sections.Count + " Sections");
                                }
                                else
                                {
                                    section++;
                                    //reset
                                    if (section >= level.sections.Count)
                                    {
                                        section = 0;
                                    }
                                    secNO = section;
                                }
                                if (LoadNewSection(secNO))
                                {
                                    Debug.Log(secNO + " Section loaded");
                                }

                            }
                        }
                        lastSpawn = 0f;

                    }
                    else
                    {
                        lastSpawn += speedMod * Time.deltaTime;// not perfect with puase
                    }
                }

                //update UI
                distanceTraveled = levelTime * speedMod;
                //TODO not accurate
                int temp = (int)(distanceTraveled);
                GameMaster.instance.UI.distText.text = (speedMod.ToString() + " Mbps");

            }
        }
    }
    public void LevelFailed()
    {
        LevelPlay = false;
        Debug.Log("level failed");
        //update loss values
        GameMaster.instance.UI.EndScreenUpdate();
       GameMaster.instance.UI.SwitchPanels(3);
        //GameMaster.instance.ResetScene();
        //insert ui prompt
    }
    public void StartLevel(int LevelID)
    {
        //load level

        LevelInitialiize();
        LevelPlay = true;
        Debug.Log("paused at " + levelTime);

    }
    public void ResetLevel()
    {
        // unloadlevel
        GameMaster.instance.ResetRun();

        RowSpawnList = new List<Row>();
        level = GameMaster.instance.Levels[0];
        LoadLevel();
        // remove progress, dump food


    }
    public void PuaseLevel(bool target)
    {
        LevelPlay = target;
    }
    public void NextLevel()
    {
        if (levelno + 1 < GameMaster.instance.Levels.Count && GameMaster.instance.Levels[levelno + 1] != null)
        {
            levelno++;
            level = GameMaster.instance.Levels[levelno];

            
            //unload rows
            ///GameMaster.instance.GetComponent<TerrainController>().DumpActive(4);
            //unload objects or keep going?
            GameMaster.instance.GetComponent<TerrainController>().DestroyLevelElements();
            //unload RowSpawnList

            //load new level
            LoadLevel();

        }
        else
        {
            Debug.Log("No level");
            //TODO UNLOCK RANDOM INFINITE
            PuaseLevel(false);
            GameMaster.instance.UI.SwitchPanels(7);

        }
    }
    public bool LoadNewSection(int sectionNO)
    {

        int blockNO = 0;
        float lastpos = 0f;
        int rowNO = 0;
        float targetpos = GameMaster.instance.spawnOffset * 2 + (curRow * (level.slateInterval + localacceleration));

        if (level.patternInfinite)
        {
            while (lastpos < targetpos)
            {
                // get pattern
                List<Level.Difficulty> pat = level.pattern;
                //for each in pattern get random diff from LD and load into row
                foreach( Level.Difficulty d in pat)
                {
                  Block tempBlock =  LD.RandomBlock(d);
                    foreach(Row r in tempBlock.Rows)
                    {
                        Row temprow = r;
                        //advanced row logic
                        if (advanceSpawn)
                        {
                            float chance = Random.Range(0,100);
                            if (chance <= (boonSpawnChance)&& bonusBoon>5)
                            {
                                // pick row
                                // temprow = new row
                                temprow = LD.RandomBoon();
                                bonusBoon = 0;
                                //Debug.Log("boon spawn " + chance);
                                
                            }
                            else
                            {
                                chance = Random.Range(0, 100);
                                if (chance <= (hazardSpawnChance) && bonusHazard > 5) {
                                    // pick row
                                    // temprow = new row
                                    temprow = LD.RandomHazard();
                                    bonusHazard = 0;
                                   // Debug.Log("Hazard spawn");
                                }
                            }
                                bonusHazard += 1;
                                bonusBoon += 1;


                        }
                        RowSpawnList.Add(temprow);
                    }
                   // 
                }
                lastpos = RowSpawnList.Count * (level.slateInterval + localacceleration) + GameMaster.instance.spawnOffset;

            }
                //build from level data
                return true;
        }
        else
        {
            while (lastpos < targetpos && blockNO < level.sections[sectionNO].blocks.Count)
            {

                // while rows left in blocks and valid block

                Debug.Log(" block/ " + blockNO);
                bool fliprows = level.sections[sectionNO].blocks[blockNO].flip;

                while (lastpos < targetpos && rowNO < level.sections[sectionNO].blocks[blockNO].Rows.Count)
                {
                    if (fliprows)
                    {
                        //cant creat rows on the fly
                        if (level.sections[sectionNO].blocks[blockNO].Rows[rowNO].FlippedRow != null)
                        {
                            RowSpawnList.Add(level.sections[sectionNO].blocks[blockNO].Rows[rowNO].FlippedRow);
                        }
                        else
                        {
                            //cant leave row empty
                            RowSpawnList.Add(level.sections[sectionNO].blocks[blockNO].Rows[rowNO]);

                        }
                    }
                    else
                    {
                        RowSpawnList.Add(level.sections[sectionNO].blocks[blockNO].Rows[rowNO]);
                        //Debug.Log("row ADDEd"  + rowNO);
                    }

                    //  Debug.Log(rowNO + " row" + level.sections[sectionNO].blocks[blockNO].Rows.Count + " newlastpos: " + lastpos + "/" + targetpos);
                    lastpos = RowSpawnList.Count * (level.slateInterval + localacceleration) + GameMaster.instance.spawnOffset;
                    GameMaster.instance.UI.notifactionText.text = "loaded row" + (rowNO + 1);


                    rowNO++;
                }
                blockNO++;
                rowNO = 0;
            }
            return true;
        }
    }
    public void LoadLevel()
    {
        Debug.Log("level "+ levelno + "accelerator = " + LevelAccel);
        GameMaster.instance.UI.levelText.text = ("Level " + (levelno+1));
        foodEaten = 0;
        foodGoal = level.Goal; 
        speedMod = level.levelSpeed;
        GameMaster.instance.UI.GoalUpdate(foodEaten / (float)foodGoal);
    }
    public void LevelInitialiize()
    {
        
        if(level== null)
        {
            if(GameMaster.instance.test == true)
            {
            level = GameMaster.instance.testlevel;
                levelno = -1;

            }
            else
            level = GameMaster.instance.Levels[levelno];
        }
        LoadLevel();
        GameMaster.instance.life.lives = GameMaster.instance.life.maxLives;
        if (true)
        {
            
            //load 2 sections"section should be a visible length of terrain"
            int sectionNO = section;
            float lastpos = 0f;
            bool end = false;
            float targetpos = GameMaster.instance.spawnOffset *2 + (curRow * (level.slateInterval + localacceleration));
            RowSpawnList.Clear();
            curRow = 0;
            //times 2 for size of section to load
            while (lastpos < targetpos && !end) 
            {

                while (lastpos < targetpos && sectionNO < level.sections.Count)
                {


                    LoadNewSection( sectionNO);
                    sectionNO++;
                    section = sectionNO;

                }
                //put here query to get mroe sections , or repeat sections to 0
                Debug.Log("no more sections");
                end = true;
            } 
        }
        else
        {

        }
        GameMaster.instance.UI.notifactionText.text = " level? ";
        GameMaster.instance.UI.InitiliazeUI();

        //load
    }

    public void FoodEaten(int amount)
    {
        float gain = amount * LevelAccel;
        if (useMulti)
            GameMaster.instance.LC.foodEaten += (gain + (basemulti * Streaks));
        else
        {
           
            GameMaster.instance.LC.foodEaten += (gain);
            totalfoodEaten += gain;
        }
        GameMaster.instance.UI.GoalUpdate(foodEaten / (float)foodGoal);

        //GameMaster.instance.UI.GoalUpdate(foodEaten / (float)foodGoal);
        if (foodEaten >= foodGoal&& !level.PureRandomInfinte)
        {
           
            NextLevel();
            GameMaster.instance.UI.GoalUpdate(foodEaten / (float)foodGoal);

        }
        else if(level.PureRandomInfinte)
        {
            foodEaten = 0;
            foodGoal = level.Goal;
            speedMod += 1f;
            // todo hard cap
        }
    }
    public void LevelWon()
    {
        LevelPlay = false;
        Debug.Log("level won");
        GameMaster.instance.ResetScene();
    }
    public void StreakReset()
    {
        if (useMulti)
        {
            Debug.Log("streak reset from " + Streaks);
            streakCount = 0;
            Streaks = 0;
            GameMaster.instance.UI.UpdateMulti(0);
        }
    }
    public void StreakAdd()
    {
        if (useMulti)
        {
            streakCount++;
            if (streakCount >= streakNo)
            {
                Streaks++;
                streakCount = 0;
            }
            GameMaster.instance.UI.UpdateMulti((basemulti * Streaks));
        }
    }
}
