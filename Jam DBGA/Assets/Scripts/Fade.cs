using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour
{

	public float fadeValue = 0.02f;


	public void Start ()
	{
		GetComponent<CanvasGroup> ().alpha = 1;
		GetComponent<Fade> ().FadeOut ();
	}


    public void FadeIn()
    {
        StartCoroutine(FadeInCO());
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutCO());
    }

    private IEnumerator FadeInCO()
    {
		while (this.gameObject.GetComponent<CanvasGroup>().alpha < 1)
        {
			this.gameObject.GetComponent<CanvasGroup>().alpha += fadeValue;
            yield return null;
        }

        yield break;
    }

    private IEnumerator FadeOutCO()
    {
		while (this.gameObject.GetComponent<CanvasGroup>().alpha > 0)
        {
			this.gameObject.GetComponent<CanvasGroup>().alpha -= fadeValue;
            yield return null;
        }

        yield break;
    }
}