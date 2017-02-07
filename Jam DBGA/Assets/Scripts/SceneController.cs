using System;
using UnityEngine;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// #TODO: Find a way to connect SceneController between other scenes
// #TODO: ReduceCO has to reduce tickCount
// #TODO: Add a database for each player

public class SceneController : MonoBehaviour
{
    private TapBalloon refTap;
    public int numberPlayer, playerCounter = 1;
    public GameObject balloon;

    public Text playerShift;

    private void Awake()
    {
        //if (FindObjectsOfType<SceneController>().Length > 1)
          //  Destroy(this.gameObject);
        refTap = FindObjectOfType<TapBalloon>();
    }

    public void StartMatch(int _player)
    {
        SceneManager.LoadScene(1);
        numberPlayer = _player;
    }

    public void Restart(int _numPlayer)
    {
        if (_numPlayer.Equals(1))
        {
            SceneManager.LoadScene(1);
        }

        else if (_numPlayer.Equals(2) || _numPlayer.Equals(3) || _numPlayer.Equals(4))
        {
            // Reset del timer

            refTap.isExplosed = false;
            refTap.tickCount = 0;
            refTap.textMesh.color = Color.blue;
            balloon.transform.localScale = refTap.startScale;
            balloon.GetComponent<MeshRenderer>().enabled = true;
            playerCounter++;
            playerShift.text = "Player " + playerCounter + " it's your turn!";
        }

    }
}
