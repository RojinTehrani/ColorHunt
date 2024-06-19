using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;



public class myGame : MonoBehaviour
{
    public GameObject loseObject;
    public GameObject winObject;

    [SerializeField] Sprite[] monsterImages;
    [SerializeField] Sprite[] heartImages;
    //public GameObject[] G_Object;
    public Button[] pb;
    public Button heartButton;

    public List<int> takeList = new List<int>();
    private int randomNumber;

    Dictionary<string, Sprite> Monsters;
    Dictionary<string, Sprite> MonstersEng;
    Dictionary<string, Color> colorText;

    public Color colorPick;
    public string picName;

    //level, score, lives
    public int level = 1;
    public int score = 0;
    public int lives = 3;

    public TextMeshProUGUI txtScore;
    public TextMeshProUGUI txtColor;

    public TextMeshProUGUI txtScoreInLose;
    public TextMeshProUGUI txtTimeInLose; 
    
    public TextMeshProUGUI txtScoreInWin;
    public TextMeshProUGUI txtTimeInWin;


    //audio
    public AudioSource backgroundMusic;
    public AudioSource correctSound;
    public AudioClip correctSoundClip;
    public AudioSource discorrectSound;
    public AudioClip discorrectSoundClip;
    public AudioSource loseSound;
    public AudioClip loseSoundClip;
    public AudioSource winSound;
    public AudioClip winSoundClip;

    //about timer
    [Header("Component")]
    public TextMeshProUGUI txtTimer;

    [Header("Timer setting")]
    public float currentTime;
    public bool countDown;

    [Header("Limit settings")]
    public bool hasLimit;
    public float timerLimit;


    private bool win;
    private bool lose;

    private bool timeBegin;

    string time;

    void Start()
    {
        loseObject.SetActive(false);
        winObject.SetActive(false);

        win = false;
        lose=false;
        timeBegin = true;

        Monsters = new Dictionary<string, Sprite>
        {
            { "آبی", monsterImages[0] },
            { "سبز", monsterImages[1] },
            { "صورتی", monsterImages[2] },
            { "بنفش", monsterImages[3] },
           { "قرمز", monsterImages[4] } ,
            { "زرد", monsterImages[5] }
        };

        MonstersEng = new Dictionary<string, Sprite>
        {
            { "Blue", monsterImages[0] },
            { "Green", monsterImages[1] },
            { "Pink", monsterImages[2] },
            { "Purple", monsterImages[3] },
           { "Red", monsterImages[4] } ,
            { "Yellow", monsterImages[5] }
        };

        colorText = new Dictionary<string, Color>
        {
            { "Blue", Color.blue },
            { "Green", Color.green },
            { "Yellow", Color.yellow },
            { "Red", Color.red },
            { "Pink", Color.magenta },
            { "Purple", Color.white }

        };



        randMonster();
        setText();

    }

    public Color setTextColor(int i)
    {
        switch (i)
        {
            case 0:
                return Color.blue;
            case 1:
                return Color.gray;
            case 2:
                return Color.cyan;
            case 3:
                return Color.green;
            case 4:
                return Color.magenta;
            case 5:
                return Color.yellow;
            case 6:
                return Color.red;

            default: return Color.white;
        }
    }

    public void setText()
    {
        int rand = Random.Range(1, Monsters.Count + 1);
        colorPick = colorText.ElementAt(rand - 1).Value;

        picName = MonstersEng.ElementAt(rand - 1).Key;

        txtColor.text = Monsters.ElementAt(rand - 1).Key;
        txtColor.color = setTextColor(Random.Range(0, 8));

        txtScore.text = "امتیاز: " + score;
    }
    public void randMonster()
    {

        takeList = new List<int>(new int[pb.Length]);

        for (int i = 0; i < pb.Length; i++)
        {
            randomNumber = UnityEngine.Random.Range(1, monsterImages.Length + 1);
            while (takeList.Contains(randomNumber))
            {
                randomNumber = UnityEngine.Random.Range(1, monsterImages.Length + 1);

            }
            takeList[i] = randomNumber;
            pb[i].GetComponent<Image>().sprite = monsterImages[(takeList[i] - 1)];



            //G_Object[i].GetComponent<SpriteRenderer>().sprite = monsterImages[(takeList[i] - 1)];
        }

    }




    public void checkCorrectMonster(Image img)
    {

        if (img.sprite.name.ToString() == picName)
        {
            correctSound.PlayOneShot(correctSoundClip);
            Debug.Log(picName);
            Debug.Log(img.sprite.name);

            randMonster();
            setText();

            score = score + 5;
            txtScore.text = "امتیاز: " + score;
            currentTime=currentTime + 1.5f;

 
        }
        else
        {
            score = score - 5;
            lives = lives - 1;
            txtScore.text = "امتیاز: " + score;
            discorrectSound.PlayOneShot(discorrectSoundClip);

        }
        if (score == 40)
        {
            win = true;
            checkWin();

        }
        if (lives == 0)
        {
            lose = true;
            checkLose();

        }


    }

    public void checkWin()
    {
        
        if (win == true)
        {
            winObject.SetActive(true);

            timeBegin = false;
            txtTimer.color = Color.green;
            txtScoreInWin.text ="امتیاز: "+ score.ToString();
            txtTimeInWin.text ="زمان: "+ txtTimer.text;


            backgroundMusic.Stop();
            winSound.PlayOneShot(winSoundClip);
            Debug.Log("You Win");
        }
    }
    public void checkLose()
    {
      
        if (lose == true)
        {
            loseObject.SetActive(true);
            txtScoreInLose.text ="امتیاز: "+score.ToString();
            txtTimeInLose.text="زمان: "+ txtTimer.text;

            timeBegin = false;
            txtTimer.color = Color.red;
           
            backgroundMusic.Stop();
            loseSound.PlayOneShot(loseSoundClip);

            Debug.Log("You Lose");
        }
    }

    public void changeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }


    private void NewGame()
    {
        this.score = 0;

    }





    private void Update()
    {
        liveManager();
        //time = txtTimer.text;
        currentTime = countDown ? currentTime -= Time.deltaTime: currentTime+=Time.deltaTime;
        if (timeBegin)
        {
       
        if(hasLimit && ((countDown && currentTime<=timerLimit)) || (!countDown && currentTime>=timerLimit)) 
        {
            currentTime = timerLimit;
            setTimerText();
            txtTimer.color = Color.red;
            enabled = false;
            lose = true;
            checkLose();
        }
            setTimerText();

        }

    }
    private void setTimerText()
    {
        txtTimer.text = currentTime.ToString("0.0");

    }
    public void liveManager()
    {
    
        if (lives == 3)
        {
            heartButton.GetComponent<Image>().sprite = heartImages[0];
        }
        if (lives == 2)
        {
            heartButton.GetComponent<Image>().sprite = heartImages[1];
        }
        if (lives == 1)
        {
            heartButton.GetComponent<Image>().sprite = heartImages[2];
        } 
        if (lives == 0)
        {
            heartButton.GetComponent<Image>().sprite = heartImages[3];
        }
    }
}











   


   





