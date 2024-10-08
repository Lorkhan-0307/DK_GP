using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneObject : MonoBehaviour
{
    [SerializeField] private PlayableDirector _director;
    [SerializeField] private GameObject[] roadblocks;
    public void PlayCutScene()
    {
        foreach(GameObject block in roadblocks)
        {
            block.SetActive(false);
        }
        _director.Play();
    }
}
