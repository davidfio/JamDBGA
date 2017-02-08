using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// #TODO: Find a way to connect SceneController between other scenes
// #TODO: ReduceCO has to reduce tickCount
// #TODO: Add a database for each player

public class SceneController : MonoBehaviour
{
    
	public int numberPlayerFromMenu = 1; 
    
	public GameObject canvasFader;
    
	protected static SceneController _self;
	public static SceneController Self
	{
		get
		{
			if (_self == null)
				_self = FindObjectOfType(typeof(SceneController)) as SceneController;
			return _self;
		}
	}

    public void StartMatch(int nMode)
    {
		NPlayer.Self.nPlayer = numberPlayerFromMenu;

		switch (nMode)
		{
		case 1:
			StartCoroutine (FadeToScene (1));
			break;
		case 2:
			StartCoroutine (FadeToScene (2));
			break;
		case 3:
			StartCoroutine (FadeToScene (3));
			break;
		case 4:
			StartCoroutine (FadeToScene (4));
			break;
		default:
			print ("Incorrect level.");
			break;
		}
		/*if (numberPlayerFromMenu == 1) {
			//SceneManager.LoadScene(1);
			StartCoroutine (FadeToScene (1));
			//numberPlayerFromMenu = _player;
		} else {
			StartCoroutine (FadeToScene (2));
		}*/
    }

    
    
	public IEnumerator FadeToScene (int sceneIndex)
	{
		canvasFader = GameObject.FindGameObjectWithTag ("Fader");
		canvasFader.GetComponent<Fade>().FadeIn();
		yield return new WaitForSeconds(0.8f);
		SceneManager.LoadScene(sceneIndex);
	}
    
    
}
