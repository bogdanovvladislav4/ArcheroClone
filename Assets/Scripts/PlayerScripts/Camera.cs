using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform character;
    [SerializeField] private Vector3 cameraOffset;

    void LateUpdate()
    {
        var trans = transform;
        var position = character.position;
        trans.position = position + cameraOffset;
        /*trans.LookAt(position);*/
    }
}
