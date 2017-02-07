using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class TapBalloon : MonoBehaviour
{
    public Button retryButton, nextPlayerButton;
    private Vector3 thresholdScale, finalScale;
    private SceneController refSC;
    private Vector3 startScale, ReduceVector;
    public int tickTot, tickCount, riskyTick;
    // Number of players choosed in MainMenu
    private int numberPlayer;
    public bool explosiveTick;

    // For debug
    public TextMesh textMesh;
    public ParticleSystem particle;

    private void Awake ()
	{
        tickCount = 0;
        // Random tickTot for each match
	    tickTot = Random.Range(30, 51);
        // Threshold out tiskTot
        riskyTick = 10;
        startScale = Vector3.one;
        ReduceVector = new Vector3(5, 0, 0);
	    textMesh.text = tickCount.ToString() + " /" + tickTot.ToString();
	    this.GetComponent<MeshRenderer>().sharedMaterial.color = Color.white;
	    refSC = FindObjectOfType<SceneController>();
        finalScale = new Vector3(3.5f, 2.7f, 3.5f);
        thresholdScale = finalScale / tickTot;
        //numberPlayer = refSC.numberPlayer;
    }

    private void Update ()
    {

        if (Input.GetMouseButtonDown(0) && tickCount <= tickTot)
        { 
            // Risky zone
            if (tickCount >= tickTot - riskyTick)
            {
                textMesh.color = Color.red;
                RandomExplosion();
                DecisionAfterMatch();
            }
            Increase();
            textMesh.text = tickCount.ToString() + " /" + tickTot.ToString();
        }

        else if (Input.GetMouseButtonDown(0) && tickCount >= tickTot)
        {
            FinishExplosion();
            DecisionAfterMatch();
        }

        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(ReduceCO());
        }

    }
  
    private void Increase()
    {
        this.transform.localScale += thresholdScale;
        this.GetComponent<MeshRenderer>().sharedMaterial.color += new Color(0, 0, 0.2f);
        tickCount++;
    }

    private void RandomExplosion()
    {
        int probability = Random.Range(0, 21);
        Debug.Log("Random Number is " + probability);

        if (probability > 5) return;

        explosiveTick = true;
        StartCoroutine(WaterParticle());
        this.GetComponent<MeshRenderer>().enabled = false;
        
    }

    private void FinishExplosion()
    {
        StartCoroutine(WaterParticle());
        this.GetComponent<MeshRenderer>().enabled = false;
        Debug.Log("EXPLOSION!!!!!");      
    }

    private IEnumerator WaterParticle()
    {
        particle.Play();
        yield return  new WaitForSeconds(0.5f);
        particle.Stop();
    }

    // For debug
    private IEnumerator ReduceCO()
    {
        while (this.transform.localScale.sqrMagnitude > startScale.sqrMagnitude)
        {
            this.transform.localScale -= thresholdScale * Time.deltaTime;
            yield return null;
        }
        yield break;
    }

    private void DecisionAfterMatch()
    {
        switch (numberPlayer)
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
    }
}
