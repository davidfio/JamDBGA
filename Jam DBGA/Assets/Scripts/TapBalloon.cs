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
    public int tickTot, tickCount;
    private int numberPlayer, riskyTick;
    private bool explosiveTick; 
    public bool isExplosed;

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
	    textMesh.text = tickCount.ToString() + " /" + tickTot.ToString();

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
            textMesh.text = tickCount.ToString() + " /" + tickTot.ToString();
        }

        else if (Input.GetMouseButtonDown(0) && tickCount >= tickTot && !isExplosed)
        {
            FinishExplosion();
            DecisionAfterMatch(numberPlayer);
            isExplosed = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(ReduceCO());
        }
    }

#region Methods
    private void Increase()
    {
        this.transform.localScale += thresholdScale;
        tickCount++;
    }

    private void RandomExplosion()
    {
        int probability = Random.Range(0, 21);
        Debug.Log("Random Number is " + probability);

        if (probability > 5) return;

        explosiveTick = true;
        FinishExplosion();
        DecisionAfterMatch(numberPlayer);
        isExplosed = true;
    }

    private void FinishExplosion()
    {
        StartCoroutine(WaterParticle());
        this.GetComponent<MeshRenderer>().enabled = false;      
    }

    private void DecisionAfterMatch(int _numPlayer)
    {
        // Spawn restart or retry button
        switch (_numPlayer)
        {
            case 1:
                retryButton.gameObject.SetActive(true);
                break;
            case 2:
                nextPlayerButton.gameObject.SetActive(true);
                break;
            case 3:
                nextPlayerButton.gameObject.SetActive(true);
                break;
            case 4:
                nextPlayerButton.gameObject.SetActive(true);
                break;
        }

        if (refSC.playerCounter.Equals(_numPlayer))
        {
            nextPlayerButton.gameObject.SetActive(false);
            retryButton.gameObject.SetActive(true);
            return;
        }
    }

    private IEnumerator WaterParticle()
    {
        particle.Play();
        yield return new WaitForSeconds(0.5f);
        particle.Stop();
    }

    private IEnumerator ReduceCO()
    {
        while (this.transform.localScale.sqrMagnitude > startScale.sqrMagnitude)
        {
            this.transform.localScale -= thresholdScale * Time.deltaTime * 2.5f;
            yield return null;
        }
        yield break;
    }
    #endregion
}
