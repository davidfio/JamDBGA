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
    public Timer refTimer;
    public int numberPlayerFromMenu; 
    public int playerCounter;
    public GameObject balloon;
	public GameObject canvasFader;
    public bool isFirstTime;
    public Text playerShift;

	protected static SceneController _self;
	public static SceneController Self
	{
		get
		{
			if (_self == null)
				_self = FindObjectOfType(typeof(SceneController)) as SceneController;
			return _self;
		}
	}

    private void Awake()
    {
        refTap = FindObjectOfType<TapBalloon>();
        refTimer = FindObjectOfType<Timer>();
        playerCounter = 1;
        playerShift.text = "Player " + playerCounter + " it's your turn!";
    }

    public void StartMatch(int _player)
    {
        //SceneManager.LoadScene(1);
		StartCoroutine(FadeToScene());
        //numberPlayerFromMenu = _player;
    }

    public void RestartSingle()
    {     
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
	public IEnumerator FadeToScene ()
	{
		canvasFader = GameObject.FindGameObjectWithTag ("Fader");
		canvasFader.GetComponent<Fade>().FadeIn();
		yield return new WaitForSeconds(0.8f);
		SceneManager.LoadScene(2);
	}
    
    public void ResetMulti()
    {
        if (numberPlayerFromMenu.Equals(2) || numberPlayerFromMenu.Equals(3) || numberPlayerFromMenu.Equals(4))
        {
            // Increase playerCount
            playerCounter++;
            // Bool timerFinish seto false
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
}
