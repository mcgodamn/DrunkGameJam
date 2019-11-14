using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoroutineUtility : MonoBehaviour
{
    private static CoroutineUtility _instance;
    public static CoroutineUtility instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameObject("CoroutineUtility").AddComponent<CoroutineUtility>();
            return _instance;
        }
    }

    //建立CoroutineQueue回傳並開始排序工作
    public CoroutineQueue Do()
    {
        return CoroutineQueue.GetQueue(this);
    }
}

public class CoroutineQueue : IDisposable
{
    public static CoroutineQueue GetQueue(CoroutineUtility utility)
    {
        return new CoroutineQueue(utility);
    }

    CoroutineUtility coroutineUtility;
    List<IEnumerator> waitQueue;
    bool isStartPlaying = false;

    public CoroutineQueue(CoroutineUtility utility)
    {
        waitQueue = new List<IEnumerator>();
        coroutineUtility = utility;
    }

    public CoroutineQueue Play(Animator animator, string animStateName)
    {
        waitQueue.Add(PlayAnimation(animator, animStateName));
        return this;
    }

    public CoroutineQueue Wait(float time)
    {
        waitQueue.Add(WaitTime(time));
        return this;
    }

    public CoroutineQueue Move(GameObject obj, Vector3 newPos, float time)
    {
        waitQueue.Add(MoveObj(obj, newPos, time));
        return this;
    }

    public CoroutineQueue MoveUI(GameObject obj, Vector2 newPos, float time)
    {
        waitQueue.Add(MoveUIObj(obj, newPos, time));
        return this;
    }

    public CoroutineQueue ScaleUI(GameObject obj, Vector2 newScale, float time)
    {
        waitQueue.Add(ScaleUIObj(obj, newScale, time));
        return this;
    }

    public CoroutineQueue RadiusUIMove(GameObject obj, Vector2 newPos, float xRadius, float yRadius, float time)
    {
        waitQueue.Add(RadiusMoveUIObj(obj, newPos, xRadius, yRadius, time));
        return this;
    }

    public CoroutineQueue Then(UnityAction call)
    {
        waitQueue.Add(DoCall(call));
        return this;
    }

    public CoroutineQueue DoEnumerator(IEnumerator ie)
    {
        waitQueue.Add(ie);
        return this;
    }

    public CoroutineQueue WaitUntil(Func<bool> require)
    {
        waitQueue.Add(WaitUntilReqTrue(require));
        return this;
    }

    public void Go()
    {
        if (!isStartPlaying)
        {
            isStartPlaying = true;
            coroutineUtility.StartCoroutine(DoQueue());
        }
    }

    IEnumerator WaitUntilReqTrue(Func<bool> require)
    {
        yield return new WaitUntil(() => { return require(); });
    }

    IEnumerator PlayAnimation(Animator animator, string animStateName)
    {
        if (animator != null)
        {
            animator.Play(animStateName);
            yield return new WaitUntil(() =>
            {
                if (animator != null && animator.enabled)
                    return animator.GetCurrentAnimatorStateInfo(0).IsName(animStateName);
                else
                    return true;
            });
            yield return new WaitUntil(() =>
            {
                if (animator != null && animator.enabled)
                    return !animator.GetCurrentAnimatorStateInfo(0).IsName(animStateName);
                else
                    return true;
            });
        }
    }

    IEnumerator RadiusMoveUIObj(GameObject obj, Vector2 newPos, float xRadius, float yRadius, float time)
    {
        RectTransform rect = obj.GetComponent<RectTransform>();
        Vector2 prePos = rect.anchoredPosition;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / time;
            if (t > 1)
                t = 1;
            float x = Mathf.Lerp(rect.anchoredPosition.x, newPos.x, xRadius);
            float y = Mathf.Lerp(rect.anchoredPosition.y, newPos.y, yRadius);
            rect.anchoredPosition = new Vector2(x, y);
            yield return null;
        }
    }

    IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);
    }

    IEnumerator ScaleUIObj(GameObject obj, Vector3 newScale, float time)
    {
        RectTransform rectTransform = obj.GetComponent<RectTransform>();
        Vector3 preScale = rectTransform.localScale;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / time;
            if (t > 1)
                t = 1;
            rectTransform.localScale = Vector3.Lerp(preScale, newScale, t);
            yield return null;
        }
    }

    IEnumerator MoveObj(GameObject obj, Vector3 newPos, float time)
    {
        Vector3 prePos = obj.transform.position;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / time;
            if (t > 1)
                t = 1;
            obj.transform.position = Vector3.Lerp(prePos, newPos, t);
            yield return null;
        }
    }

    IEnumerator MoveUIObj(GameObject obj, Vector2 newPos, float time)
    {
        RectTransform rectTransform = obj.GetComponent<RectTransform>();
        Vector2 prePos = rectTransform.anchoredPosition;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / time;
            if (t > 1)
                t = 1;
            rectTransform.anchoredPosition = Vector3.Lerp(prePos, newPos, t);
            yield return null;
        }
    }

    IEnumerator DoQueue()
    {
        while (waitQueue.Count > 0)
        {
            yield return coroutineUtility.StartCoroutine(waitQueue[0]);
            waitQueue.RemoveAt(0);
        }

        //Dispose self
        CoroutineQueue.Dispose(this);
    }

    IEnumerator DoCall(UnityAction func)
    {
        func.Invoke();
        yield return null;
    }

    public void Dispose()
    {
        waitQueue = null;
    }

    public static void Dispose(CoroutineQueue q){
        q.Dispose();
        q = null;
    }
}