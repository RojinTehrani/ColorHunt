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


public class myGame : MonoBehaviour
{
    [SerializeField] Sprite[] monsterImages;
    //public GameObject[] G_Object;
    public Button[] pb;

    public List<int> takeList=new List<int>();
    private int randomNumber;

    Dictionary<string, Sprite> Monsters;
    Dictionary<string, Sprite> MonstersEng;
    Dictionary<string, Color> colorText;
    public Color colorPick;
    public string picName;

    public int level = 1;
    public int score = 0;
    public int lives = 3;
    public TextMeshProUGUI txtScore;
    public TextMeshProUGUI txtColor;


    public AudioSource correctSound;
    public AudioClip correctSoundClip;
    public AudioSource discorrectSound;
    public AudioClip discorrectSoundClip;



    void Start()
    {


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
        switch(i)
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
        int rand = Random.Range(1, Monsters.Count+1);
        colorPick = colorText.ElementAt(rand-1).Value;

        picName = MonstersEng.ElementAt(rand - 1).Key;

        txtColor.text = Monsters.ElementAt(rand-1).Key;
        txtColor.color = setTextColor(Random.Range(0,8));

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
            pb[i].GetComponent<Image>().sprite= monsterImages[(takeList[i] - 1)];



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

            score=score+5;
            txtScore.text = "امتیاز: " + score;
            if (score >= 40)
            {
                Debug.Log("You Win");

            }
        }
        else
        {
            score = score - 5;
            lives = lives - 1;
            txtScore.text = "امتیاز: " + score;
            discorrectSound.PlayOneShot(discorrectSoundClip);
            if(lives == 0)
            {
                Debug.Log("You Lose");

            }
        }
        


    }




    private void NewGame()
        {
            this.score = 0;

        }





    private void Update()
    {

        //if (Input.GetMouseButtonDown(0))
        //{

        //}
    }






    }











   


   





