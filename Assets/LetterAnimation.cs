using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterAnimation : MonoBehaviour
{
    private float timer;
    public bool animating;
    [SerializeField] private GameObject firstLetter;
    
    [SerializeField] private GameObject secondLetter;
    [SerializeField] private GameObject thirdLetter;
    
    [SerializeField] private RectTransform paper;
    [SerializeField] private float bottom;
    [SerializeField] private float top;

    [SerializeField] private Vector3 type;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!animating)
        {
            return;
        }

        firstLetter.SetActive(true);
        timer += Time.deltaTime;
        if (timer>1)
        {
            firstLetter.SetActive(false);
            secondLetter.SetActive(true);
            thirdLetter.SetActive(true);
            paper.gameObject.SetActive(true);
            paper.localPosition = Vector3.up*Mathf.Lerp(bottom,top,(timer-1)/2);
        }
        
       
    }
}
