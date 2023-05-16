using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarTransport : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")&&other.gameObject.TryGetComponent(out PlayerController pc))
        {
            pc.GoToWar();
        }
    }
}