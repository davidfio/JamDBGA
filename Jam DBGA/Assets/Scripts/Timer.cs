using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

    public Slider handle;
    private float seconds;

	void Start ()
    {
        handle.minValue = 0;
        handle.maxValue = Random.Range(30, 50);

	}
	
	void Update ()
    {
        seconds += Time.deltaTime;
        handle.value = seconds;
	}
}
