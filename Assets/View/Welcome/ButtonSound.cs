using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BUTTON_SOUND_TYPE
{
    CURSOR = 0,
    DECISION = 1
}

public class ButtonSound : MonoBehaviour
{

    AudioSource[] sources;

    void Awake()
    {
        sources = GetComponents<AudioSource>();
    }

    public void OnPlaySound(int type)
    {
        switch(type)
        {
            case (int)BUTTON_SOUND_TYPE.CURSOR:
                sources[0].Play();
                break;
            case (int)BUTTON_SOUND_TYPE.DECISION:
                sources[1].Play();
                break;
        }
    }
}