using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    [SerializeField] private string text;
    [SerializeField] private bool fadeout;
    [SerializeField] private float fadeoutTime;
    
    private float ticksPerSecond = 13f;
    private float bobbingSpeed = 1.5f;
    private float bobbingAmplitude = 0.05f;
    
    private float spriteOffsetPerLine = 0.11f;
    private float canvasoffsetPerLine = 0.11f;
    private GameObject _spawnedSpeechBubble;
    private TMP_Text _text;
    private SpriteRenderer _speechBubbleSpriteRenderer;
    private float bubbleStartHeight;
    private RectTransform canvasRectTransform;
    private float canvasStartHeight;
    private void Start()
    {
        _spawnedSpeechBubble = transform.GetChild(0).gameObject;
        _text = _spawnedSpeechBubble.GetComponentInChildren<TMP_Text>();
        _speechBubbleSpriteRenderer = _spawnedSpeechBubble.GetComponentInChildren<SpriteRenderer>();
        canvasRectTransform = _spawnedSpeechBubble.GetComponentInChildren<Canvas>().GetComponent<RectTransform>();
        bubbleStartHeight = _spawnedSpeechBubble.GetComponentInChildren<SpriteRenderer>().size.y;
        canvasStartHeight = _spawnedSpeechBubble.GetComponentInChildren<Canvas>().GetComponent<RectTransform>().GetTop();
        _text.text = "";
        _spawnedSpeechBubble.SetActive(false);
        
    }
    [ContextMenu("StartTextAnimation")]
    public void StartTextAnimation()
    {
        StartCoroutine(TypeText());
    }
    private IEnumerator TypeText()
    {
        _text.text = ""; // Clear the current text
        _text.ForceMeshUpdate();
        _speechBubbleSpriteRenderer.size = new Vector2(_speechBubbleSpriteRenderer.size.x, bubbleStartHeight + spriteOffsetPerLine * math.max(_text.textInfo.lineCount-1,0));
        canvasRectTransform.SetTop(canvasStartHeight - canvasoffsetPerLine * math.max(_text.textInfo.lineCount-1,0));
        _spawnedSpeechBubble.SetActive(true);

        // Calculate delay time
        var delay = 1f / ticksPerSecond;

        foreach (var letter in text)
        {
            _text.text += letter; // Add next letter to textMesh
            _text.ForceMeshUpdate();
            _speechBubbleSpriteRenderer.size = new Vector2(_speechBubbleSpriteRenderer.size.x, bubbleStartHeight + spriteOffsetPerLine * math.max(_text.textInfo.lineCount-1,0));
            canvasRectTransform.SetTop(canvasStartHeight - canvasoffsetPerLine * math.max(_text.textInfo.lineCount-1,0));
            if (letter.ToString() != " ")
            {
                yield return new WaitForSeconds(delay); // Wait for the specified time
            }
        }

        if (!fadeout) yield break;
        yield return new WaitForSeconds(fadeoutTime);
        _spawnedSpeechBubble.SetActive(false);
    }

    private void Update()
    {
        if (_spawnedSpeechBubble != null && _spawnedSpeechBubble.activeSelf)
        {
            _spawnedSpeechBubble.transform.position = (math.round(math.sin(Time.time * bobbingSpeed)) * bobbingAmplitude * Vector3.up) + transform.position;
        }
    }

    [ContextMenu("ResetTextAnimation")]

    public void ResetTextAnimation()
    {
        _text.text = "";
        _text.ForceMeshUpdate();
        _speechBubbleSpriteRenderer.size = new Vector2(_speechBubbleSpriteRenderer.size.x, bubbleStartHeight + spriteOffsetPerLine * math.max(_text.textInfo.lineCount-1,0));
        canvasRectTransform.SetTop(canvasStartHeight - canvasoffsetPerLine * math.max(_text.textInfo.lineCount-1,0));
        _spawnedSpeechBubble.SetActive(false);

    }
}
