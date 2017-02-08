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
    public Button retryButton, nextPlayerButton;
    private Vector3 thresholdScale, finalScale; 
    public Vector3 startScale;
    private SceneController refSC;
    public ParticleSystem particle;
    public float tickTot, tickCount;
    private int numberPlayer, riskyTick, probability;
    public bool isExplosed, timerFinish;
    public GameObject Effetto, waterBall;
    public GameObject Effetto2;

    private void Awake ()
    {
        refSC = FindObjectOfType<SceneController>();

        // Random tickTot for each match
	    tickTot = Random.Range(30, 51);
        // Player tick
        tickCount = 0;
        // Threshold out tiskTot
        riskyTick = 10;
        // For debug 
	    textMesh.text = tickCount + " /" + tickTot;

        startScale = Vector3.one;
        finalScale = new Vector3(3.5f, 2.7f, 3.5f);
        thresholdScale = finalScale / tickTot;
        numberPlayer = refSC.numberPlayer;
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
            DecisionAfterMatch(numberPlayer);
            isExplosed = true;            
        }

        // Brutal but for debug
        if (timerFinish)
        {
            DecisionAfterMatch(numberPlayer);
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
        this.GetComponent<MeshRenderer>().enabled = false;
        Effetto.SetActive(true);
        StartCoroutine(ParticleSystemCO(5));
    }

    private void RandomExplosion()
    {
        probability = Random.Range(0, 21);

        if (probability > 5) return;

        Explosion();
        DecisionAfterMatch(numberPlayer);
        isExplosed = true;
    }

    public void DecisionAfterMatch(int _numPlayer)
    {
        // Spawn restart or retry button
        if (_numPlayer.Equals(1))
        {
            retryButton.gameObject.SetActive(true);
            StopAllCoroutines();
        }

        else if (_numPlayer.Equals(2) || _numPlayer.Equals(3) || _numPlayer.Equals(4))
        {
            nextPlayerButton.gameObject.SetActive(true);

            StopAllCoroutines();
        }
        
        if (refSC.playerCounter.Equals(_numPlayer))
        {
            nextPlayerButton.gameObject.SetActive(false);
            retryButton.gameObject.SetActive(true);

            StopAllCoroutines();
        }
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
