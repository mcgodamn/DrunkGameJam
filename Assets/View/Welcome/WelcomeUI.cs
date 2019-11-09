using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class WelcomeUI : MonoBehaviour
{

    [SerializeField]
    Transform leftDoor, rightDoor;

    [SerializeField]
    Image whiteBackground, flag;

    [SerializeField]
    float openDoorSec = 3, scalSec = 3, buttonFadeSec = 0.5f, backgroundFadeSec = 1, screenScal2 = 1100, containerTween2 = 150;

    [SerializeField]
    CanvasScaler canvasScaler;

    [SerializeField]
    RectTransform container;

    [SerializeField]
    Button StartButton, InsturctionButton;

    void Start()
    {
        DOTween.Init();
    }

    public void OnStartGame()
    {
        TweenCallback onButtonFadeout = () =>
        {
            rightDoor.DOLocalMoveX((rightDoor as RectTransform).sizeDelta.x * 1.5f, openDoorSec);
            leftDoor.DOLocalMoveX((leftDoor as RectTransform).sizeDelta.x * -1.5f, openDoorSec).onComplete = () =>
            {
                DOTween.To(
                    () => canvasScaler.referenceResolution,
                    v2 => canvasScaler.referenceResolution = v2,
                    new Vector2(screenScal2, canvasScaler.referenceResolution.y), scalSec
                );
                container.DOLocalMoveY(containerTween2, scalSec).onComplete = () =>
                {
                    whiteBackground.DOFade(0, backgroundFadeSec);
                };
                flag.DOFade(0, scalSec / 2);
            };
        };

        StartButton.image.DOFade(0, buttonFadeSec);
        InsturctionButton.image.DOFade(0, buttonFadeSec).onComplete = onButtonFadeout;
    }
}
