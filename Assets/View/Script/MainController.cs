using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    private static MainController Instance;    //Singleton
    public GameObject canvas;
    public int timeLimit;
    public bool isGameStarted;
    public Text countText;
    private float countTime = 0;
    [SerializeField]
    AudioSource BGM;
    [SerializeField]
    AudioSource ClickSound;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update() {
        if (isGameStarted) {

            countTime += Time.deltaTime;
            if (timeLimit != 120 - (int)countTime)
            {
                timeLimit = 120 - (int)countTime;
                countText.text = (120 - (int)countTime).ToString();
            }

            if (timeLimit <= 0) {
                GameStop();
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            ClickSound.Play();
        }
    }

    public static MainController GetInstance()
    {
        if (Instance == null)
            Instance = new GameObject("MainController").AddComponent<MainController>();
        return Instance;
    }
    public void StartCount()
    {
        isGameStarted = true;
        countText.gameObject.SetActive(true);
        // BGM.Play();
    }

    public void GameStop() {
        countText.gameObject.SetActive(false);
        countTime = 0;
        timeLimit = 60;
        EndCanvas.instance.OnEndGame();
    }
}
