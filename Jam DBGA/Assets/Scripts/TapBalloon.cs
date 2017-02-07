using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class TapBalloon : MonoBehaviour
{
    public Button retryButton, nextPlayerButton;

    private SceneController refSC;
    private Vector3 startScale, ReduceVector;
    private int tickTot, tickCount, riskyTick;
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
        startScale = this.transform.localScale;
        ReduceVector = new Vector3(5, 0, 0);
	    textMesh.text = tickCount.ToString() + " /" + tickTot.ToString();
	    this.GetComponent<MeshRenderer>().sharedMaterial.color = Color.white;
	    refSC = FindObjectOfType<SceneController>();
	    numberPlayer = refSC.numberPlayer;
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
	}
  
    private void Increase()
    {
        this.transform.localScale += ReduceVector * Time.deltaTime;
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
            this.transform.localScale -= ReduceVector * Time.deltaTime;
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
