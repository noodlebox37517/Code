using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Lottie : MonoBehaviour
{

    public TextMeshProUGUI textfield;
    public Image lottieSprite;
    private bool isShowing = false;
    private const float randomjokeTimer = 15; //seconds
    private const float showTimer =15;
    private float lastShow = 0;
    private float startedShowing = 0;
    public List<string> randomJokes;
    
    // Start is called before the first frame update
    void Start()
    {
        lastShow = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShowing == false && randomJokes.Count > 0)
        {
            if(Time.time >= lastShow + randomjokeTimer)
            {
                RandomJoke();
            }
        }
        if (isShowing)
        {
            if (showTimer + startedShowing <= Time.time)
            {
                isShowing = false;
                ShowLottie(isShowing);
                lastShow = Time.time;
            }
        }
    }

    private void RandomJoke()
    {
        if (randomJokes.Count > 0)
        {
            var joke = randomJokes[Random.Range(0,randomJokes.Count)];
            textfield.text = joke;
            isShowing = true;
            startedShowing = Time.time;
            ShowLottie(isShowing);
        }
    }

    public void ShowLottie(bool show)
    {
        lottieSprite.gameObject.SetActive(show);
        textfield.gameObject.SetActive(show);
    }
}
