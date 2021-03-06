using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class RiskChallenge : MonoBehaviour
{
    // For debug
    public TextMesh textMesh;
    public UI refUI;
	private GameManager refGM;
    private Vector3 thresholdScale, finalScale; 
    public Vector3 startScale;
	private NPlayer refNP;
    private Timer refTimer;
    
    public float tickTot, tickCount;
    private int numberPlayer, riskyTick, probability;
    public bool isExplosed, timerFinish;
    public GameObject Effetto, Effetto2, waterBall;

	private int currentPlayer;

	public int clickPlayer1;
	public int clickPlayer2;
	public int clickPlayer3;
	public int clickPlayer4;


    private void Awake ()
    {
		refNP = FindObjectOfType<NPlayer>();
        refUI = FindObjectOfType<UI>();
        refTimer = FindObjectOfType<Timer>();
		refGM = FindObjectOfType<GameManager>();
        // Random tickTot for each match
	    tickTot = Random.Range(30, 51);
        // Player tick
        tickCount = 15;
        // Threshold out tiskTot
        riskyTick = Random.Range(10, 19);
        // For debug 
	    textMesh.text = tickCount + " /" + tickTot;

        startScale = new Vector3(2f, 1.8f, 2f);
        finalScale = new Vector3(3.2f, 2.5f, 3.2f);
        thresholdScale = finalScale / tickTot;
		numberPlayer = refNP.nPlayer;
		isExplosed = true;
		refGM.nextPlayerButton.transform.GetChild(0).GetComponent<Text>().text = "Player 1 pass your turn!";

        this.transform.localScale = startScale;
    }

    private void Update ()
    {
		/*
        if (Input.GetMouseButtonDown(0) && tickCount <= tickTot && !isExplosed)
        {
            // Risky zone
            if (tickCount >= tickTot - riskyTick)
            {
                textMesh.color = Color.red;
                RandomExplosion();
            }

            Increase();
            StopAllCoroutines();
            waterBall.gameObject.SetActive(false);
            textMesh.text = tickCount + " /" + tickTot;
        }

        else if (Input.GetMouseButtonDown(0) && tickCount >= tickTot && !isExplosed)
        {
            Explosion();
            //refGM.DecisionAfterMatch();
            isExplosed = true;            
        }

        
        
        // Make a method that it's called when you don't tap
        if (Input.GetMouseButtonUp(0))
        {
            //StartCoroutine(DeflateCO());
        }*/
    }

#region Methods

	public void StartGame () {
		isExplosed = false;
	}


	public void Inflate () {

		if (tickCount <= tickTot && !isExplosed)
		{
			// Risky zone
			if (tickCount >= tickTot - riskyTick)
			{
				textMesh.color = Color.red;
				RandomExplosion();
			}

			Increase();
			StopAllCoroutines();
			waterBall.gameObject.SetActive(false);
			textMesh.text = tickCount + " /" + tickTot;
		}

		else if (tickCount >= tickTot && !isExplosed)
		{
			Explosion();
			//refGM.DecisionAfterMatch();
			isExplosed = true;            
		}

	}


    private IEnumerator DeflateCO()
    {
        yield return new WaitForSeconds(1.5f);
        //StartCoroutine(ReduceScaleCO());
        //StartCoroutine(DecreaseCO());
        waterBall.gameObject.SetActive(true);
    }

    private void Increase()
    {
		tickCount++;
        this.transform.localScale += thresholdScale;
		currentPlayer = refGM.playerCounter + 1;
		if (currentPlayer == 1) {
			clickPlayer1++;
		} else if (currentPlayer == 2){
			clickPlayer2++;
		} else if (currentPlayer == 3) {
			clickPlayer3++;
		} else if (currentPlayer == 4) {
			clickPlayer4++;
		}
    }

    private IEnumerator DecreaseCO()
    {
        while (tickCount > 0 && !isExplosed)
        {
            tickCount--;
            textMesh.text = tickCount + " /" + tickTot;
            yield return new WaitForSeconds(1f);
        }
    }

    private void Explosion()
    {
		if (currentPlayer == 1) {
			clickPlayer1 = 0;
		} else if (currentPlayer == 2){
			clickPlayer2 = 0;
		} else if (currentPlayer == 3) {
			clickPlayer3 = 0;
		} else if (currentPlayer == 4) {
			clickPlayer4 = 0;
		}
		Debug.Log ("EXPLOSION_START");
		ScoreChart ();
		refGM.nextPlayerButton.gameObject.SetActive(false);
		this.GetComponent<MeshRenderer>().enabled = false;
		this.transform.localScale = startScale;
        Effetto.SetActive(true);
        StartCoroutine(ParticleSystemCO(5));

		this.gameObject.SetActive (false);
//		refGM.panelScore.SetActive (true);
		refGM.ActivateScorePanel();
		Debug.Log ("EXPLOSION_END");
    }

    private void RandomExplosion()
    {
		Debug.Log ("RANDOM_EXPLOSION");
        probability = Random.Range(0, 21);

        if (probability > 5) return;

        Explosion();
		//refGM.DecisionAfterMatch();
        isExplosed = true;
    }

    

    private IEnumerator ReduceScaleCO()
    {
        while (this.transform.localScale.sqrMagnitude > startScale.sqrMagnitude)
        {
            this.transform.localScale -= thresholdScale * Time.deltaTime;
            yield return null;
        }
        waterBall.gameObject.SetActive(false);
        yield break;
    }

    private IEnumerator ParticleSystemCO(int _time)
    {
        Effetto2.SetActive(true);
        yield return new WaitForSeconds(_time);
        Effetto2.SetActive(false);
    }
    #endregion


	public void ScoreChart () 
	{
		if(numberPlayer == 1) {
			refGM.scoreTextArray [0].gameObject.SetActive (true);
			refGM.scoreTextArray [0].text = "Player 1" + ": " + clickPlayer1;
		}
		if (numberPlayer == 2) {
			refGM.scoreTextArray [0].gameObject.SetActive (true);
			refGM.scoreTextArray [0].text = "Player 1" + ": " + clickPlayer1;
			refGM.scoreTextArray [1].gameObject.SetActive (true);
			refGM.scoreTextArray [1].text = "Player 2" + ": " + clickPlayer2;
		}
		if (numberPlayer == 3) {
			refGM.scoreTextArray [0].gameObject.SetActive (true);
			refGM.scoreTextArray [0].text = "Player 1" + ": " + clickPlayer1;
			refGM.scoreTextArray [1].gameObject.SetActive (true);
			refGM.scoreTextArray [1].text = "Player 2" + ": " + clickPlayer2;
			refGM.scoreTextArray [2].gameObject.SetActive (true);
			refGM.scoreTextArray [2].text = "Player 3" + ": " + clickPlayer3;
		}

		if (numberPlayer == 4) {
			refGM.scoreTextArray [0].gameObject.SetActive (true);
			refGM.scoreTextArray [0].text = "Player 1" + ": " + clickPlayer1;
			refGM.scoreTextArray [1].gameObject.SetActive (true);
			refGM.scoreTextArray [1].text = "Player 2" + ": " + clickPlayer2;
			refGM.scoreTextArray [2].gameObject.SetActive (true);
			refGM.scoreTextArray [2].text = "Player 3" + ": " + clickPlayer3;
			refGM.scoreTextArray [3].gameObject.SetActive (true);
			refGM.scoreTextArray [3].text = "Player 4" + ": " + clickPlayer4;
		}
	}



	public void RestartRisk()
	{     
		//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


		// Set not explosed
		isExplosed = false;
		// Reset tickCount
		tickCount = 0;
		// Reset color of the text mesh
		textMesh.color = Color.blue;
		// Reset scale and mesh renderer of the balloon
		this.gameObject.SetActive (true);
		this.transform.localScale = startScale;

		clickPlayer1 = 0;
		clickPlayer2 = 0;
		clickPlayer3 = 0;
		clickPlayer4 = 0;
		refGM.playerCounter = 0;

		this.GetComponent<MeshRenderer>().enabled = true;
		// Disable particles systems
		this.Effetto.SetActive(false);
		this.Effetto2.SetActive(false);
		this.waterBall.SetActive(false);
		// Print text to playershift
		int realNPlayer = refGM.playerCounter + 1;
		refGM.nextPlayerButton.transform.GetChild(0).GetComponent<Text>().text = "PLAYER " + realNPlayer + " it's your turn!";

	}


}
