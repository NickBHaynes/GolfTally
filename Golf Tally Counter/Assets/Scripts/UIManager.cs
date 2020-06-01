using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public List<GameObject> menus = new List<GameObject>();
    public TMP_InputField opponentName;
    public TMP_InputField opponentShots;
    public Slider whoIsOwed;

    public TMP_Dropdown opponentsDrop;

    public int gameScene;
    string selectedPlayer = "";

    public void Start()
    {
        PopulateDropDown();
    }

    public void OpenMenuBtn(int menuNum)
    {
        menus[menuNum].SetActive(true);
    }

    public void CloseMenuBtn(int menuNum)
    {
        menus[menuNum].SetActive(false);
    }
    #region New Players
    public void SaveNewPlayerBtn(int menuNum)
    {
        if (opponentName.text != "" && opponentShots.text != "") {
            if (!GameManager.instance.opponents.ContainsKey(opponentName.text))
            {
                //Add New Player here
                AddNewOpponent newOp = new AddNewOpponent();
                Person newPlayer = newOp.AddOpponent(opponentName.text, opponentShots.text);
                float shotValue = float.Parse(opponentShots.text);
                if (whoIsOwed.value == 1)
                {
                    shotValue *= -1;
                }
                newPlayer.pastRounds.Add(shotValue);
                GameManager.instance.opponents.Add(opponentName.text, newPlayer);
                GameManager.instance.Save();

                opponentName.text = "";
                opponentShots.text = "";
                PopulateDropDown();
                CloseMenuBtn(menuNum);

            } else if (GameManager.instance.opponents.ContainsKey(opponentName.text))
            {
                //Handle Errors
                Debug.Log($"Player {opponentName.text} already exists");

            } 
        }
        else
        {
            //Handle Errors
            Debug.Log("Please Add A valid player");
        }
    }

    #endregion

    #region Existing Players

    public void PopulateDropDown()
    {
        opponentsDrop.ClearOptions();
        List<string> players = new List<string>();
        foreach (var opponent in GameManager.instance.opponents)
        {
            players.Add(opponent.Key);
        }

        opponentsDrop.AddOptions(players);
    }

    #endregion

    #region Starting select player

    public void ConfirmPlayer()
    {
        selectedPlayer = opponentsDrop.options[opponentsDrop.value].text;
        GameManager.instance.selectedPlayer = selectedPlayer;
        GameManager.instance.SelectScene(gameScene);
    }
    #endregion
}
