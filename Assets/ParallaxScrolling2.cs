using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling2 : MonoBehaviour
{
    public Camera cam;
    public Transform subject;

    private Vector2 _startPosition;
    private float _startZ;
    private float ParallaxFactor => Mathf.Abs(DistanceFromSubject) / ClippingPlane;
    private float DistanceFromSubject => transform.position.z - subject.position.z;
    private float ClippingPlane => cam.transform.position.z + (DistanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane);
    private Vector2 Travel => (Vector2)cam.transform.position - _startPosition;
    
    void Start()
    {
        _startPosition = transform.position;
        _startZ  = transform.position.z;
    }

    void LateUpdate()
    {
        Vector2 pos = _startPosition + Travel * ParallaxFactor;
        transform.position = new Vector3(pos.x, pos.y, _startZ);
    }
}
