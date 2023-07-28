using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{

    [SerializeField] private RectTransform handlePos;

    private void Update()
    {
        Debug.Log(handlePos.rect);
    }

    public Vector2 JoystickPosition()
    {
        var rect = handlePos.rect;
        return new Vector2(rect.x, rect.y);
    }
}
