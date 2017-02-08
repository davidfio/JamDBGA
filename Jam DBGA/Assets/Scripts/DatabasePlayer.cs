using UnityEngine;
using System.Collections;

public class DatabasePlayer : MonoBehaviour
{
    private TapBalloon refTap;
    public float numberOfTap;
    private void Start ()
	{
	    refTap = FindObjectOfType<TapBalloon>();
	}

    private void AssignPoint()
    {
        numberOfTap = refTap.tickCount;
    }
}
