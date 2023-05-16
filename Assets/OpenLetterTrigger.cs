using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLetterTrigger : MonoBehaviour
{
    [SerializeField] private GameObject letter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            letter.SetActive(true);
        }
    }
}
