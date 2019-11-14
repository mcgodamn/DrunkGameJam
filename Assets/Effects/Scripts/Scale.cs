using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{

    [SerializeField]
    int loopTime;
    [SerializeField]
    float[] scaleRate;
    // Use this for initialization
    [SerializeField]
    float time;
    void Start()
    {
        CoroutineQueue queue = CoroutineUtility.instance.Do();
        for (int i = 0; i < loopTime; i++)
        {
            for (int a = 0; a < scaleRate.Length; a++)
            {
                queue.ScaleUI(gameObject, Vector3.one * scaleRate[a], time);
            }
        }
        queue.Go();
    }

}
