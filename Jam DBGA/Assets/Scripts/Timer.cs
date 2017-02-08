using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour
{
    public Image imageTime;
    private TapBalloon refTap;
    public float seconds = 15;
    private float thresholdTime;

    private void Awake()
    {
        thresholdTime = imageTime.fillAmount / seconds;
        StartCoroutine(DecreaseBar());
        refTap = FindObjectOfType<TapBalloon>();
    }

    public IEnumerator DecreaseBar()
    {
        while (imageTime.fillAmount > 0)
        {
            yield return new WaitForSeconds(1);
            imageTime.fillAmount -= thresholdTime;
        }
        refTap.timerFinish = true;
        yield break;
    }
}
