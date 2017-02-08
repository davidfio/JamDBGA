using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ClassificaManager : MonoBehaviour {
    List<Player> playerInGame = new List<Player>();
    public int playerNumber = 1;
    public Text myText;
    public GameObject scorePannel;
    public Text[] scoreText;

    public void AddPlayer()
    {
        if (playerNumber < 4)
        {
            playerNumber++;
            myText.text = "Player: " + playerNumber;
        }
        return;
    }
    public void SubPlayer()
    {
        if (playerNumber > 1)
        {
            playerNumber--;
            myText.text = "Player: " + playerNumber;
        }
        return;
            
    }

    public void ToScore()
    {
       
        for(int i = 1; i< playerNumber+1; i++)
        {
            playerInGame.Add(new Player ("Player " + i,1 + Random.Range(1, 60)));
        }
        playerInGame.Sort();
        scorePannel.SetActive(true);
        for(int j= 0; j < playerNumber; j++)
        {
            scoreText[j].text = playerInGame[j].name + "    " + playerInGame[j].point;
        }
    }
    public void Reset()
    {
        playerInGame.Clear();
        scorePannel.SetActive(false);
        playerNumber = 1;
        myText.text = "Player: " + playerNumber;
    }
}
