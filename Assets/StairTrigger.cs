using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairTrigger : MonoBehaviour
{
    public Stair stair;
    public bool turnOffer;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (turnOffer)
            {
                
                stair.Disable();
                
            }
            else
            {
                
                stair.Enable();
            }
        }
    }
}
