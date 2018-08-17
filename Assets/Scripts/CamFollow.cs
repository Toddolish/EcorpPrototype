using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{

    public Transform Target;
    public float smoothSpeed = 0.125f;
    public Vector3 Offset;

    [Header("Mask")]
    LayerMask mask;

    float zoom = 0.5f;

    void Start()
    {

    }

    void Update() //this will run after the player has finished all of the movement cycles in update
    {

        Vector3 desiredPos = Target.position + Offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.position = smoothedPos;
    }
}
