using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class TapBalloon : MonoBehaviour
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

    private void Awake ()
    {
		refNP = FindObjectOfType<NPlayer>();
        refUI = FindObjectOfType<UI>();
        refTimer = FindObjectOfType<Timer>();
		refGM = FindObjectOfType<GameManager>();
        // Random tickTot for each match
	    tickTot = Random.Range(30, 51);
        // Player tick
        tickCount = 0;
        // Threshold out tiskTot
        riskyTick = 10;
        // For debug 
	    textMesh.text = tickCount + " /" + tickTot;

        startScale = Vector3.one;
        finalScale = new Vector3(3.2f, 2.5f, 3.2f);
        thresholdScale = finalScale / tickTot;
		numberPlayer = refNP.nPlayer;

        //if (SceneManager.GetActiveScene().name == "GameSceneMulti")
        //{
        //    refUI.StartCoroutine(refUI.PlayerButtonFade(nextPlayerButton.gameObject));
        //}
    }

    private void Update ()
    {
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
            refGM.DecisionAfterMatch();
            isExplosed = true;            
        }

        // If timer is finish start Decision and set isExplosed as true;
        if (timerFinish)
        {
			// SALVA PUNTEGGIO
			refGM.SaveScore(tickCount);
			refGM.DecisionAfterMatch();
			StopAllCoroutines ();
            isExplosed = true;
        }
        
        // Make a method that it's called when you don't tap
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(DeflateCO());
        }
    }

#region Methods


    private IEnumerator DeflateCO()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(ReduceScaleCO());
        StartCoroutine(DecreaseCO());
        waterBall.gameObject.SetActive(true);
    }

    private void Increase()
    {
        this.transform.localScale += thresholdScale;
        tickCount++;
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
		refGM.SaveScore(0);
        this.GetComponent<MeshRenderer>().enabled = false;
        Effetto.SetActive(true);
        StartCoroutine(ParticleSystemCO(5));
        // Stop the coroutin inside timer to stop the time when balloon explose
        refTimer.StopAllCoroutines();
    }

    private void RandomExplosion()
    {
        probability = Random.Range(0, 21);

        if (probability > 5) return;

        Explosion();
		refGM.DecisionAfterMatch();
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
}
