using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteRow : CustomUI
{
    public Sprite sprite;
    public GameObject image;
    public float gap = 0;
    /// <summary>
    /// 0 indicates fillpanel
    /// </summary>
    public int maxSprites = 0;
    public int currenthealth = 0;
    public bool fillRight = true;
    public List<Image> Images;
    // Start is called before the first frame update

    public void Start()
    {
        sprite = image.GetComponent<Image>().sprite;
    }
    public void Update()
    {

    }
    public void ChangeHealth(int newhealth)
    {
        // turn all images <= new health on and rest off
        for(int i =0; i< Images.Count; i++)
        {
            if (i<= newhealth-1)
            {
                Images[i].color = Color.white;
            }
            else
            {
                Images[i].color = Color.clear;
                // turn off
            }
            currenthealth = newhealth;
        }
    }
    public override void Initil()
    {
        //get length
       float pwidth = this.GetComponent<RectTransform>().rect.width;
        //get sprite length
       float sprwidth = image.GetComponent<RectTransform>().rect.width; 
        //check enough room for atleast one sprite
        if (pwidth < sprwidth)
            return;

       int sprfit = 1 + (int)((pwidth - sprwidth) / (sprwidth + gap));


        if (maxSprites!=0 && sprfit > maxSprites)
            sprfit = maxSprites;
        Debug.Log(sprfit + " max sprites");

        //create image in row from starting poing
        //left or right
        float xpos = this.GetComponent<RectTransform>().rect.xMin + sprwidth/2;
        float ypos = this.GetComponent<RectTransform>().rect.yMax - sprite.rect.height/2;
        float dir = 1;

        if (fillRight)
        {
            xpos = this.GetComponent<RectTransform>().rect.xMax - sprwidth / 2;
            dir = - 1;
        }

        Vector2 startpos = new Vector2(xpos, ypos);
        for (int i = 0; i <sprfit ; i++)
        {
            GameObject tempgo = Instantiate(image,this.transform);
            tempgo.GetComponent<RectTransform>().anchoredPosition = startpos + dir * new Vector2(sprwidth*i+(+gap*i), 0);
            Images.Add(tempgo.GetComponent<Image>());
            //Debug.Log(i);
            //Debug.Log(sprwidth + "   "+ dir * new Vector2(sprwidth * i + (+gap * i * 0), 0));
         //   tempgo

        }
       
        //get max amount of sprites
        
    }
}
