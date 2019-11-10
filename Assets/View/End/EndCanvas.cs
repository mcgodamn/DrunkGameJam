using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;

public class EndCanvas : MonoBehaviour
{
    [SerializeField]
    RectTransform[] Pukes;

    [SerializeField]
    float flowEndPostion, DoorCloseSec;
    public static EndCanvas instance;

    private void Awake()
    {
        instance = this;
    }

    public void OnEndGame()
    {
		gameObject.SetActive(true);
        Guy.guyType = GuyType.GUY_RANBOW;
        OnStartPuke();
    }

    [SerializeField]
    RectTransform Light;

    [SerializeField]
    RectTransform[] Masks;

    [SerializeField]
    RectTransform LeftDoor, RightDoor;

    [SerializeField]
    Button againButton;

    int maski = 0;
    void MaskExpand(float time, Action callback)
    {
        if (time < 0.15f) time = 0.15f;
        DOTween.To(
                () => Masks[maski].sizeDelta.x,
                x =>
                {
                    Masks[maski].sizeDelta = new Vector2(x, x / 16 * 9);
                }, 3000, time
            ).onComplete = () =>
            {
                maski++;
                if (maski >= Masks.Length)
                    callback.Invoke();
                else MaskExpand(time * 0.5f, callback);
            };
    }

    void OnStartPuke()
    {
        Light.DOScale(Vector3.one, 0.5f).onComplete = () =>
        {
            float time = 2;
            MaskExpand(time, OnPukeProps);
        };
    }

    [SerializeField]
    GameObject TestObj;

	[SerializeField]
    Transform PropMother;

    List<int> list = new List<int>();
    int propi = 0;
    void OnPukeProps()
    {
        list.Add(1);
        PopProps(() =>
        {
			OnFullScreenPukeFlow();

            // var t = Light.DOScale(new Vector3(10, 10, 1), 1);
            // t.SetDelay(0.5f);
            // t.onComplete = OnFullScreenPukeFlow;
            // t.Play();
        });
    }

    [SerializeField]
    float fullPropTime = 1;
    void PopProps(Action callback)
    {
        var temp = Instantiate(TestObj, Vector2.zero, Quaternion.identity);
        temp.transform.localScale = Vector2.zero;
		temp.transform.SetParent(PropMother,true);
        var rotation = new Vector2(
            UnityEngine.Random.Range(-1.0f, 1.0f),
            UnityEngine.Random.Range(-1.0f, 1.0f));
        var dist = UnityEngine.Random.Range(200, 700);
        var dest = rotation.normalized * dist;

        Tween rotate = temp.transform.DORotate(new Vector3(0, 0, 360), 3, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
        rotate.Play();
		Debug.Log(dest);
        Debug.Log(dist);
        temp.transform.DOScale(Vector3.one,3 * (dist / 700.0f));
        temp.transform.DOLocalMove(dest, 3 * (dist / 700.0f)).onComplete = () =>
        {
            rotate.Kill(true);
            propi++;
            if (propi >= list.Count)
            {
                callback.Invoke();
                return;
            }
            else PopProps(callback);
        };
    }

    void OnFullScreenPukeFlow()
    {
        float waitsec = 0;
        Tween completeTween = null;
        var tweens = new List<Tween>();
        foreach (var puke in Pukes)
        {
            var sec = UnityEngine.Random.Range(2.0f, 5.0f);
            var t = puke.DOLocalMoveY(flowEndPostion, sec);
            if (sec > waitsec)
            {
                waitsec = sec;
                completeTween = t;
            }
            tweens.Add(t);
        }
        completeTween.onComplete = OnCloseDoor;
        foreach (var t in tweens) t.Play();
    }

    void OnCloseDoor()
    {
        LeftDoor.DOLocalMoveX(-480, DoorCloseSec);
        RightDoor.DOLocalMoveX(480, DoorCloseSec).onComplete = () =>
        {
            againButton.gameObject.SetActive(true);
            againButton.image.DOFade(1, 2f);
        };
    }

	public void RestartGame()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
