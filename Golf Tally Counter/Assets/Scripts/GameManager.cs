using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Dictionary<string, List<float>> opponents = new Dictionary<string, List<float>>();

    public string selectedPlayer;

    [Header ("Save References")]
    private string prevScoreList = "PREV_SCORES";
    private string roundCompleteReference = "ROUND_COMPLETE";

    public string CUR_AV = "Current_Average";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }

        }
        DontDestroyOnLoad(gameObject);

        if (ES3.KeyExists(prevScoreList))
        {
            opponents = ES3.Load(prevScoreList, new Dictionary<string, List<float>>());
        }
    }

    public void SelectScene(int sceneToSelect)
    {
        SceneManager.LoadScene(sceneToSelect);
    }


    // this is where I will add the save/load data for opponents
    public void Save()
    {
        ES3.Save(prevScoreList, opponents);
    }
}
