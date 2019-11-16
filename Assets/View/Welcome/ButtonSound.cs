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
    [SerializeField]
    AudioSource cursorSound;

    [SerializeField]
    AudioSource decisionSound;

    public void OnPlaySound(int type)
    {
        switch(type)
        {
            case (int)BUTTON_SOUND_TYPE.CURSOR:
                cursorSound.Play();
                break;
            case (int)BUTTON_SOUND_TYPE.DECISION:
                decisionSound.Play();
                break;
        }
    }
}