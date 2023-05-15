using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public Rigidbody2D linkedBoday;

    public void Break()
    {
        linkedBoday.isKinematic = false;
        Destroy(gameObject);
    }
}
