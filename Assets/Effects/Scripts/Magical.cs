using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Magical : MonoBehaviour
{
    public GameObject circleMagicalPrefab;
    public GameObject verticalMagicalPrefab;
    private RectTransform circleMagical;
    private RectTransform verticalMagical;

    Image circleMagicalImage;
    Image verticalMagicalImage;

    bool isStarted = false;

    public int timeLast = 10;

    static float timeLeft = 0;

    Image selfImage;
    void Start()
    {
        selfImage = GetComponent<Image>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isStarted)
        {
            circleMagical.Rotate(0, 0, Time.deltaTime * 200);
            verticalMagical.anchoredPosition = new Vector2(
                verticalMagical.position.x, Mathf.Sin(Time.time) * 600);
            circleMagicalImage.color = new Color(
                circleMagicalImage.color.r,circleMagicalImage.color.g, circleMagicalImage.color.b,
                (Mathf.Sin(Time.time * 5f) + 1) / 5 + 0.28f);
            
            Magical.timeLeft -= Time.deltaTime;

            if (Magical.timeLeft < 0) {
                isStarted = false;
                DestroyMagical();
            }
        }
    }

    public void OnEatten()
    {
        if (Magical.timeLeft > 0) {
            Magical.timeLeft = timeLast;
            DestroyMagical();
            return;
        }

        circleMagical = Instantiate(circleMagicalPrefab, MainController.GetInstance().canvas.transform).GetComponent<RectTransform>();
        verticalMagical = Instantiate(verticalMagicalPrefab, MainController.GetInstance().canvas.transform).GetComponent<RectTransform>();
        circleMagicalImage = circleMagical.GetComponent<Image>();
        Magical.timeLeft = timeLast;
        selfImage.enabled = false;
        isStarted = true;
        // CoroutineUtility.instance.Do().Wait(timeLast).Then(() =>
        // {
        //     DestroyMagical();
        // }).Go();
    }

    private void DestroyMagical()
    {
        Destroy(circleMagical?.gameObject);
        Destroy(verticalMagical?.gameObject);
        Destroy(gameObject);
    }
}
