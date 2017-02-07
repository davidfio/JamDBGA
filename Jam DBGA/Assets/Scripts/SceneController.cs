using System;
using UnityEngine;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private TapBalloon refTap;
    public int numberPlayer;

    private void Awake()
    {
        refTap = FindObjectOfType<TapBalloon>();
        DontDestroyOnLoad(this.gameObject);
    }
    public void StartMatch(int _player)
    {
        SceneManager.LoadScene(1);
        numberPlayer = _player;
    }
}
