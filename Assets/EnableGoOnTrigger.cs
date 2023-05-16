using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnableGoOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectToEnable;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        gameObjectToEnable.SetActive(true);
        
    }
}
