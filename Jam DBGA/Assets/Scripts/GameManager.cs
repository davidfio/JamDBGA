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

	public GameObject canvasFader;

	private void Awake()
	{
		canvasFader = GameObject.FindWithTag ("Fader");
		refTap = FindObjectOfType<TapBalloon>();
		refTimer = FindObjectOfType<Timer>();
		playerCounter = 0;
		//playerShift.text = "Player " + playerCounter + " it's your turn!";

	}

	public void DecisionAfterMatch()
	{
		/*
		if (NPlayer.Self.nPlayer == 1)
		{
			//retryButton.gameObject.SetActive(true);
			StopAllCoroutines();
		}*/

		if (playerCounter.Equals(NPlayer.Self.nPlayer-1))
		{
			Debug.Log ("PLAYER_FINITI");
			nextPlayerButton.gameObject.SetActive(false);
			//retryButton.gameObject.SetActive(true);
			StartCoroutine(PanelScore());
		} else if (NPlayer.Self.nPlayer > 1) {
			nextPlayerButton.gameObject.SetActive(true);

			StopAllCoroutines();
		}


	}

	public void ActivateScorePanel(){
		StartCoroutine(PanelScore());
	}


	public IEnumerator PanelScore () {
		yield return new WaitForSeconds (3f);
		//yield return null;
		panelScore.SetActive (true);
		StopAllCoroutines();
	}

	public void SaveScore (float click) 
	{
		scoreTextArray [playerCounter].gameObject.SetActive (true);
		int realNPlayer = playerCounter + 1;
		scoreTextArray [playerCounter].text = "PLAYER " + realNPlayer + " : " + click;
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


	public void RestartBalloonParty()
	{
		playerCounter = 0;
		ResetMatch ();
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
		int realNPlayer = playerCounter + 2;
		nextPlayerButton.transform.GetChild(0).GetComponent<Text>().text = "Player " + realNPlayer  + " it's your turn!";
		// Start again the timer
		refTimer.StartCoroutine(refTimer.DecreaseBar());
	}


	public void NextRisk()
	{
		if (playerCounter.Equals (NPlayer.Self.nPlayer-1)){
			playerCounter = 0;
			int realNPlayer = playerCounter + 1;
			nextPlayerButton.transform.GetChild(0).GetComponent<Text>().text = "Player " + realNPlayer   + " pass your turn!";
            
		}
        else
        {
			playerCounter++;
			int realNPlayer = playerCounter + 1;
			nextPlayerButton.transform.GetChild(0).GetComponent<Text>().text = "Player " + realNPlayer  + " pass your turn!";
		}

	} 

	public void RestartScene (int sceneIndex) {
		StartCoroutine(FadeToScene(sceneIndex));
	}


	public void BackToMenu(){
		StartCoroutine(FadeToScene(0));
	}

	public IEnumerator FadeToScene (int sceneIndex)
	{
		canvasFader = GameObject.FindGameObjectWithTag ("Fader");
		canvasFader.GetComponent<Fade>().FadeIn();
		yield return new WaitForSeconds(0.8f);
		SceneManager.LoadScene(sceneIndex);
	}

}
