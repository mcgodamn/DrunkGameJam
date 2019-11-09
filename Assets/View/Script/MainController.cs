using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainController : MonoBehaviour
{
    private static MainController Instance;    //Singleton
    public GameObject canvas;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        canvas = GameObject.Find("MainCanvas");
    }

    public static MainController GetInstance()
    {
        if (Instance == null)
            Instance = new GameObject("MainController").AddComponent<MainController>();
        return Instance;
    }
}
