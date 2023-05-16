using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionSpeechBubbleTrigger : MonoBehaviour
{
    [SerializeField, Multiline] private string Text;
    [SerializeField] private bool dropout;
    [SerializeField] private float bubbleDuration;
    [SerializeField] private GameObject speechBubble;
    private bool hasbeenTriggered;
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (hasbeenTriggered || !col.CompareTag("Player"))
        {
            return;
        }
        print("hit");
        speechBubble.GetComponent<SpeechBubble>().StartTextAnimation(Text, dropout, bubbleDuration);
        hasbeenTriggered = true;
    }
}
