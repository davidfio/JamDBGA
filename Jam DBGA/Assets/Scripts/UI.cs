using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour
{
	private GameManager refGM;
    public GameObject panelTutorial;

	private void Start()
	{
		refGM = FindObjectOfType<GameManager>();

	    if (refGM.isFirstTime)
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
        refGM.isFirstTime = false;
    }
}
