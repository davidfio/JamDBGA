using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	private TapBalloon refTap;
	public Timer refTimer;
	public bool isFirstTime;
	public Text playerShift;
	public int playerCounter;
	public GameObject balloon;
	public GameObject panelScore;
	public Button retryButton, nextPlayerButton;
	public Text[] scoreTextArray;
	public bool canPlay;

	private void Awake()
	{
		refTap = FindObjectOfType<TapBalloon>();
		refTimer = FindObjectOfType<Timer>();
		playerCounter = 0;
		playerShift.text = "Player " + playerCounter + " it's your turn!";

	}

	public void DecisionAfterMatch()
	{

		if (NPlayer.Self.nPlayer == 1)
		{
			retryButton.gameObject.SetActive(true);
			StopAllCoroutines();
		}

		else if (NPlayer.Self.nPlayer > 1)
		{
			nextPlayerButton.gameObject.SetActive(true);

			StopAllCoroutines();
		}

		if (playerCounter.Equals(NPlayer.Self.nPlayer-1))
		{
			nextPlayerButton.gameObject.SetActive(false);
			retryButton.gameObject.SetActive(true);
			panelScore.SetActive (true);
			StopAllCoroutines();
		}
	}

	public void SaveScore (float click) 
	{
		scoreTextArray [playerCounter].gameObject.SetActive (true);
		int realNPlayer = playerCounter + 1;
		scoreTextArray [playerCounter].text = "Player " + realNPlayer + " : " + click;
	}


	public void NextPlayer()
	{
		//if (numberPlayerFromMenu.Equals(2) || numberPlayerFromMenu.Equals(3) || numberPlayerFromMenu.Equals(4))
		{
			// Increase playerCount
			playerCounter++;
			ResetMatch ();
		}
	}


	public void ResetMatch()
	{     
		//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

		// Bool timerFinish set false
		refTap.timerFinish = false;
		// Reset fillAmount of timer
		refTimer.ResetTimer();
		// Set not explosed
		refTap.isExplosed = false;
		// Reset tickCount
		refTap.tickCount = 0;
		// Reset color of the text mesh
		refTap.textMesh.color = Color.blue;
		// Reset scale and mesh renderer of the balloon
		balloon.gameObject.SetActive (true);
		balloon.transform.localScale = refTap.startScale;

		balloon.GetComponent<MeshRenderer>().enabled = true;
		// Disable particles systems
		refTap.Effetto.SetActive(false);
		refTap.Effetto2.SetActive(false);
		refTap.waterBall.SetActive(false);
		// Print text to playershift
		playerShift.text = "Player " + playerCounter + " it's your turn!";
		// Start again the timer
		refTimer.StartCoroutine(refTimer.DecreaseBar());
	}
}
