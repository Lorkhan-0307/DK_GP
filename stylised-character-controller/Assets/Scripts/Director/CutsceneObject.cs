using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneObject : MonoBehaviour
{
    [SerializeField] private PlayableDirector _director;
    [SerializeField] private GameObject[] roadblocks;
    [SerializeField] private GameObject key = null;
    public bool isInteractable = false;

    private void Start()
    {
        if (key == null) isInteractable = true;
    }

    public bool PlayCutScene()
    {
        if (key == null)
        {
            foreach(GameObject block in roadblocks)
            {
                block.SetActive(false);
            }
            _director.Play();
            return true;
        }
        else
        {
            if (key.activeInHierarchy)
            {
                foreach(GameObject block in roadblocks)
                {
                    block.SetActive(false);
                }
                _director.Play();
                return true;
            }
        }

        return false;
    }
        
}
