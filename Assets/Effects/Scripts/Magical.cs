using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Magical : MonoBehaviour
{


    float count = 0;
    float colorCount = 0;

    public GameObject circleMagicalPrefab;
    public GameObject verticalMagicalPrefab;
    private RectTransform circleMagical;
    private RectTransform verticalMagical;

    Image circleMagicalImage;
    Image verticalMagicalImage;

    bool isStarted = false;

    public int timeLast;

    // Update is called once per frame
    private void Update()
    {
        if (isStarted)
        {
            circleMagical.Rotate(0, 0, Time.deltaTime * 200);
            count += Time.deltaTime;
            colorCount += Time.deltaTime * 5f;
            verticalMagical.anchoredPosition = new Vector2(verticalMagical.position.x, Mathf.Sin(count) * 600);
            circleMagicalImage.color = new Color(circleMagicalImage.color.r, circleMagicalImage.color.g, circleMagicalImage.color.b, (Mathf.Sin(colorCount) + 1) / 5 + 0.28f);
        }
    }

    public void OnEatten()
    {
        circleMagical = Instantiate(circleMagicalPrefab, MainController.GetInstance().canvas.transform).GetComponent<RectTransform>();
        verticalMagical = Instantiate(verticalMagicalPrefab, MainController.GetInstance().canvas.transform).GetComponent<RectTransform>();
        circleMagicalImage = circleMagical.GetComponent<Image>();
        isStarted = true;
        CoroutineUtility.GetInstance().Do().Wait(timeLast).Then(() =>
        {
            DestroyMagical();
        }).Go();
    }

    private void DestroyMagical()
    {
        DestroyImmediate(circleMagical.gameObject);
        DestroyImmediate(verticalMagical.gameObject);
        DestroyImmediate(gameObject);
    }
}
