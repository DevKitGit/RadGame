using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float bottom, top;
    private void Update()
    {
        float clampedY = Mathf.Clamp(target.position.y, bottom, top);
        
        transform.position = new Vector3(target.position.x, clampedY, transform.position.z);
        
    }
}
