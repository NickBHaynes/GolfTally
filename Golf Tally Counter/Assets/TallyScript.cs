using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TallyScript : MonoBehaviour
{


    bool roundComplete = false;
    private float playerMultiplyer = 0;

    [Header ("Current Average")]
    public TMP_Text currentAverage;

    
    public float averageCount = 5;
    public string CUR_AV = "Current_Average";

    [Header ("Previous Scores")]
    private string prevScoreList = "PREV_SCORES";
    private string roundCompleteReference = "ROUND_COMPLETE";
    List<float> prevScores = new List<float>();

    public TMP_Text[] prevScoresArr;


    [Header("User Input")]
    public TMP_InputField scoreInput;

    // Start is called before the first frame update
    void Start()
    {
        if (ES3.KeyExists(prevScoreList))
        {
            prevScores = ES3.Load(prevScoreList, new List<float>());
        }

        if (ES3.KeyExists(roundCompleteReference))
        {
            roundComplete = ES3.Load(roundCompleteReference, false);
        }
        UpdateVisuals();
        CalculateAverage();
    }

    public void ChangeUser(int userNum)
    {
        switch (userNum)
        {
            case 1:
                playerMultiplyer = 1;
                break;
            case 2:
                playerMultiplyer = -1;
                break;

            default:
                Debug.Log("User not chosen");
                playerMultiplyer = 0;
                break;
        }
    }

    public void AddNewScore()
    {
        if (scoreInput.text != null && playerMultiplyer != 0)
        {
            float score = float.Parse(scoreInput.text);
            prevScores.Add(score * playerMultiplyer);
            UpdateVisuals();
            CalculateAverage();
        }
        scoreInput.text = "";
        ES3.Save(prevScoreList, prevScores);
        
    }

    public void DeleteLastScore()
    {
        if(prevScores.Count > 0)
        {
            prevScores.RemoveAt(prevScores.Count - 1);
            UpdateVisuals();
            CalculateAverage();
        } 
        ES3.Save(roundCompleteReference, roundComplete);
    }

    public void CalculateAverage()
    {
        int average = 0;
        float scoreSum = 0;
        float scoreCount = 0;

        roundComplete = (prevScores.Count > 0);

        for(int i = 0; i < averageCount; i++)
        {
            if (prevScores.Count - 1 >= scoreCount)
            {
                scoreSum += prevScores[prevScores.Count - 1 - i];
                scoreCount++;
            }
        }

        if (roundComplete)
        {
            average = (int)Math.Floor(scoreSum / scoreCount);
        } else
        {
            average = 0;
        }
        

            if(average < 0) { 
                currentAverage.text = ($"Monty owes {average * -1} shots");
            } else if (average == 0)
            {
                currentAverage.text = ("All Square");
            } else if (average > 0)
            {
                currentAverage.text = ($"Nick owes {average} shots");
            }
        
    }

    public void UpdateVisuals()
    {
        int arrCount = 0;
        for (int i = 0; i < prevScoresArr.Length; i++)
        {
            if (arrCount < prevScores.Count)
            {
                prevScoresArr[arrCount].text = prevScores[prevScores.Count - 1 - i].ToString();

                if (prevScores[prevScores.Count - 1 - i] < 0)
                {
                    prevScoresArr[arrCount].text = ($"Monty by {prevScores[prevScores.Count - 1 - i] * -1}");
                }
                else if (prevScores[prevScores.Count - 1 - i] == 0)
                {
                    prevScoresArr[arrCount].text = ("All Square");
                }
                else if (prevScores[prevScores.Count - 1 - i] > 0)
                {
                    prevScoresArr[arrCount].text = ($"Nick by {prevScores[prevScores.Count - 1 - i]}");
                }

            }
            else
            {
                prevScoresArr[arrCount].text = "Not Complete";
            }
            arrCount++;
        }
    }
}
