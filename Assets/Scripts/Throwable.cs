using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Throwable : MonoBehaviour
{
    public int force = 1;
    private Rigidbody2D rb;

    public bool isMilk;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(Vector3 screenToWorldPoint)
    {
       
        screenToWorldPoint.y = 0;
        rb.isKinematic = false;
        rb.velocity = Vector2.zero;
        rb.AddForce((screenToWorldPoint-transform.position)*force);
        transform.parent = null;
    }

    public void Freeze()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().PickUp(this);
            rb.isKinematic = true;
            transform.parent = other.gameObject.transform;
        }else if (other.gameObject.CompareTag("Breakable"))
        {
            other.gameObject.GetComponent<Breakable>().Break();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponentInParent<PlayerController>().PickUp(this);
            rb.isKinematic = true;
            transform.parent = col.gameObject.transform;
        }
    }
}
