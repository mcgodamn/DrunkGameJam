using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainController : MonoBehaviour
{
    private static MainController Instance;    //Singleton
    public GameObject canvas;
    public int timeLimit;
    public bool isGameStarted;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public static MainController GetInstance()
    {
        if (Instance == null)
            Instance = new GameObject("MainController").AddComponent<MainController>();
        return Instance;
    }
}
