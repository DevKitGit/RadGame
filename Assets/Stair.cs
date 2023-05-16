using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    private Collider2D collider;
    public bool on;
    private void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    public void Disable()
    {
        if (!on)
        {
            return;
        }
        collider.enabled = false;
    }
    public void Enable()
    {
        if (!on)
        {
            return;
        }
        collider.enabled = true;
    }

    public void ShutOff()
    {
        on = false;
        collider.enabled = false;
    }

    public void TurnOn()
    {
        on = true;
        collider.enabled = true;
    }

}
