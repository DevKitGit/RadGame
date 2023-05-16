using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    
    private string text = "";
    private bool fadeout = false;
    private float fadeoutTime = 1f;
    
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
    private AudioSource _audioSource;

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
        _audioSource = FindObjectOfType<AudioSource>();
    }
    [ContextMenu("StartTextAnimation")]
    public void StartTextAnimation()
    {
        StartCoroutine(TypeText());
    }
    
    public void StartTextAnimation(string text, bool fadeout, float fadeoutTime)
    {
        this.text = text;
        this.fadeout = fadeout;
        this.fadeoutTime = fadeoutTime;
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

            if (letter.ToString() == " ") continue;
            if (audioClip != null)
            {
                _audioSource.PlayOneShot(audioClip);
            }
            yield return new WaitForSeconds(delay); // Wait for the specified time
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
