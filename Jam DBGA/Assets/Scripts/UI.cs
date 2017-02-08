using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour
{
    private SceneController refSC;
    public GameObject panelTutorial;

	private void Start()
	{
	    refSC = FindObjectOfType<SceneController>();

	    if (refSC.isFirstTime)
	    {
	        panelTutorial.SetActive(true);
            Time.timeScale = 0;
        }
	    else
	    {
	        panelTutorial.SetActive(false);
	        Time.timeScale = 1;
	    }    
	}

    public void TapToPlay()
    {
        Time.timeScale = 1;
        refSC.isFirstTime = false;
    }

    public IEnumerator PlayerButtonFade(GameObject _go)
    {
        yield return new WaitForSeconds(2f);
        _go.SetActive(false);
    }
}
