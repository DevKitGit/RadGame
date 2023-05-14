using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milk : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().PickUp(this);
            transform.parent = other.gameObject.transform;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
