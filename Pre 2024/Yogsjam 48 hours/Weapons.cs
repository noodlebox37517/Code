using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapons : MonoBehaviour
{
    public List<SanatModel> santamodels;
    public List<SanatModel> unlockedsantamodels;
    public List<SanatModel> LockedModels;
    public  int counter = 0;
    public List<Sprite> uisprites;
    public int curmodel;
    // Start is called before the first frame update
    void Start()
    {
        
        LockedModels = new List<SanatModel>(santamodels);
        UnlockWeapon(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            LoadModel(0);
        }
        if (Input.GetKeyDown("2"))
        {
            LoadModel(1);
        }
        if (Input.GetKeyDown("3"))
        {
            LoadModel(2);
        }
        if (Input.GetKeyDown("4"))
        {
            LoadModel(3);
        }
        if (Input.GetKeyDown("5"))
        {
            LoadModel(4);
        }

    }
    public void LoadModel(int modelno)
    {
        if(modelno < unlockedsantamodels.Count )
        {
            Game.instance.santa.SwapModel(unlockedsantamodels[modelno]);

        }

    }
    public void UnlockWeapon(int i)
    {
        if (i < LockedModels.Count)
        {
            unlockedsantamodels.Add(LockedModels[i]);
            LockedModels.RemoveAt(i);
            LoadModel(i);

        }
    }
    public void UnlockWeaponRandom()
    {
        if (LockedModels.Count > 0)
        {
            int i = Random.Range(0, LockedModels.Count);

            unlockedsantamodels.Add(LockedModels[i]);
            LockedModels.RemoveAt(i);
            LoadModel(unlockedsantamodels.Count-1);

            //Game.instance.CV.GetComponent<ImageShower>().images[counter].sprite = uisprites[unlockedsantamodels.Count - 1];
            //uisprites.RemoveAt(unlockedsantamodels.Count-1);
            //Game.instance.CV.GetComponent<ImageShower>().images[counter].gameObject.SetActive(true);
            //counter++;


        }
    }
}
