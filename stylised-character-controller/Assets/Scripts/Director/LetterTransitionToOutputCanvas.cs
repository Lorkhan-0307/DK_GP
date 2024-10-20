using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Playables;
using UnityEngine;

public class LetterTransitionToOutputCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text inputText;
    [SerializeField] private GameObject inputCanvas;
    [SerializeField] private TMP_Text outputText;
    [SerializeField] private PlayableDirector _director;

    public void OnClickSubmit()
    {
        outputText.text = inputText.text;
        inputCanvas.SetActive(false);
        _director.Play();
    }
}
