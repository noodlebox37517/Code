using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class UIController : MonoBehaviour
{
    public Text notifactionText;
    public UISpriteRow healthrow;
    public Slider GoalSlider;
    public Text distText;
    public Text Lives;
    public Text MultiText;
    public Text levelText;
    public List<GameObject> livesimages;
    public GameObject shield;
    public GameObject slow;
    public GameObject blind;
    public GameObject magnet;
    public GameObject combo;
    public GameObject blindsprite;
    public GameObject activePanel;
    public GameObject[] panels;
    public Text DeathText;
    public bool inputAllowed = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
       
    }

    // Update is called once per frame
    void Update()
    {
        if (true)
        {
            foreach (Touch touch in Input.touches)
            {
                int id = touch.fingerId;
                if (EventSystem.current.IsPointerOverGameObject(id))
                {
                    if (EventSystem.current.currentSelectedGameObject != null)
                    {
                        GameMaster.instance.UI.notifactionText.text = EventSystem.current.currentSelectedGameObject.name;
                        if (EventSystem.current.currentSelectedGameObject.GetComponent<TouchEvent>() != null)
                        {
                            EventSystem.current.currentSelectedGameObject.GetComponent<TouchEvent>().Touched();
                            Debug.Log("click! " + EventSystem.current.currentSelectedGameObject.name);
                        }
                    }
                }
                Debug.Log("click!");

            }
            if (Input.GetMouseButton(0))
            {

                if (EventSystem.current.IsPointerOverGameObject())
                {

                    if (EventSystem.current.currentSelectedGameObject != null)
                    {
                        GameMaster.instance.UI.notifactionText.text = EventSystem.current.currentSelectedGameObject.name;
                        if (EventSystem.current.currentSelectedGameObject.GetComponent<TouchEvent>() != null)
                        {
                            EventSystem.current.currentSelectedGameObject.GetComponent<TouchEvent>().Touched();
                        }
                    }
                }

            }
        }
    }
    public void Shield(bool active)
    {
        shield.SetActive(active);
    }
    public void toggle(int id,bool active)
    {
        switch (id)
        {
            case 0:
                blind.SetActive(active);
                break;
            case 1:
                slow.SetActive(active);
                break;
            case 2:
                combo.SetActive(active);
                break;
            case 3:
                magnet.SetActive(active);
                break;
            default:
                break;
        }
    }
    public void UpdateLives(int CurrentLives)
    {
        Lives.text = CurrentLives.ToString();
        for (int i =0; i<livesimages.Count;i++)
        {
            if (i < CurrentLives)
            {
                livesimages[i].SetActive(true);
            }
            else
            {
                livesimages[i].SetActive(false);
            }
        }
        // Change to visual representation
    }
    public void UpdateMulti(float multi)
    {
        MultiText.text = multi.ToString();
    }

    public void SwitchPanels(int newpanel)
    {
        activePanel.SetActive(false);
        activePanel = panels[newpanel];
        activePanel.SetActive(true);
    }
    public void  GoalUpdate(float value)
    {
        GoalSlider.value = value;
    }
    public void EndScreenUpdate()
    {
        DeathText.text =  GameMaster.instance.LC.level.name;
    }
    public void InitiliazeUI()
    {
        //healthrow.Initil();
        //GoalUpdate(0f);

        UpdateLives(GameMaster.instance.life.lives);
    }
    public void BackToMenu()
    {
        // unload level TODO
        GameMaster.instance.LevelExit();
    }
    public void LoadUpgrades()
    {

    }
    public void SaveUpgrades()
    {

    }

}
