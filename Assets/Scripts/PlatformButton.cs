using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformButton : MonoBehaviour
{
   public float timer;
   public float topTimer;
   public float maxTimer;
   [SerializeField] private bool lifting;

   public GameObject platform;


   public Vector2 bottom;
   public Vector2 top;
   private void Update()
   {
      timer += lifting ? Time.deltaTime : -Time.deltaTime;

      platform.transform.position = Vector2.Lerp(bottom, top, timer/topTimer);
      if (timer> maxTimer)
      {
         timer = maxTimer;
      }
      else if(timer < 0)
      {
         
         timer = 0;
      }
   }

   private void OnTriggerExit2D(Collider2D other)
   {
      if (other.gameObject.CompareTag("Player")||other.gameObject.CompareTag("Chair"))
      {
         lifting = false;
      }
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.CompareTag("Player")||other.gameObject.CompareTag("Chair"))
      {
         lifting = true;
      }
   }
}
