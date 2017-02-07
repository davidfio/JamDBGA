using UnityEngine;
using System.Collections;

public class TapBalloon : MonoBehaviour
{
    private Vector3 startScale, ReduceVector;
    private byte tickTot, tickCount;
    private int riskyTick;
    public bool explosiveTick;

    // For debug
    public TextMesh textMesh;
    public ParticleSystem particle;

	private void Awake ()
	{
        tickCount = 0;
        tickTot = 30;
        // Threshold out tiskTot
        riskyTick = 10;
        startScale = this.transform.localScale;
        ReduceVector = new Vector3(5, 0, 0);
	    textMesh.text = tickCount.ToString() + " /" + tickTot.ToString();
	    this.GetComponent<MeshRenderer>().sharedMaterial.color = Color.white;
	}
	
	private void Update ()
    {
        if (Input.GetMouseButtonDown(0) && tickCount <= tickTot)
        { 
            // Risky zone
            if (tickCount >= tickTot - riskyTick)
            {
                textMesh.color = Color.red;
                Explosion();
            }
            Increase();
            textMesh.text = tickCount.ToString() + " /" + tickTot.ToString();
        }

        else if (tickCount >= tickTot)
        {
            StartCoroutine(WaterParticle());
            this.GetComponent<MeshRenderer>().enabled = false;
            Debug.Log("EXPLOSION!!!!!");
        }
	}
  
    private void Increase()
    {
        this.transform.localScale += ReduceVector * Time.deltaTime;
        this.GetComponent<MeshRenderer>().sharedMaterial.color += new Color(0, 0, 0.2f);
        tickCount++;
    }

    private void Explosion()
    {
        int probability = Random.Range(0, 21);
        Debug.Log("Random Number is " + probability);

        if (probability > 10) return;

        explosiveTick = true;
        StartCoroutine(WaterParticle());
        this.GetComponent<MeshRenderer>().enabled = false;
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
}
