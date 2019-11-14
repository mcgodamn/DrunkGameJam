using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour
{
    [SerializeField]
    int loopTime;
    [SerializeField]
    Vector2[] pos;
    [SerializeField]
    float time;
    // Use this for initialization
    void Start()
    {
        CoroutineQueue queue = CoroutineUtility.instance.Do();
        for (int i = 0; i < loopTime; i++)
        {
            for (int a = 0; a < pos.Length; a++)
            {
                queue.MoveUI(gameObject, pos[a], time);
            }

        }
        queue.Go();
    }
}
