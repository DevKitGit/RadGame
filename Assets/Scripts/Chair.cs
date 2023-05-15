using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public PlayerController playerController;
    private Collider2D col2D;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        col2D = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }


    public void Dettach(Vector3 pos,bool facingRight)
    {
        col2D.enabled = true;
        rb.isKinematic = false;
        sr.enabled = true;
        //transform.parent = null;
        //col2D.usedByComposite = false;
        transform.position = pos;
        sr.flipX = facingRight;
    }

    public void Attach(Vector3 pos)
    {
        col2D.enabled = false;
        rb.isKinematic = true;
        sr.enabled = false;
        //col2D.usedByComposite = true;
        //transform.parent = playerController.transform;
        
        transform.position = pos;
    }
}
