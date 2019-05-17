using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    public bool debug = true;

    private void Awake()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
        }
        else
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        Player.instance.canMove = true;
        if (!debug)
        {
            StartCoroutine(StartGameCorroutine(5));
            ScreenTransitionImageEffect.singleton.setBlack();
            AudioManager.singleton.Play("ostRolling");
        }
        UIManager.singleton.ChangeActiveScreen("uiDefault");
    }

    private IEnumerator StartGameCorroutine(int delay)
    {
        StartCoroutine(ScreenTransitionImageEffect.singleton.fadeIn(0.01f));
        yield return new WaitForSeconds(delay);
        Player.instance.canMove = true;
        yield return null;
    }
}
