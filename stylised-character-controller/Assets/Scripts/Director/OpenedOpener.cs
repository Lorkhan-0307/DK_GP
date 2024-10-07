using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenedOpener : MonoBehaviour
{
    [SerializeField] private GameObject[] opened_parents;
    [SerializeField] public float duration = 2.0f;

    public void opened_adder(int i)
    {
        foreach (SkinnedMeshRenderer renderer in opened_parents[i].GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            StartCoroutine(AnimateBlendShape(renderer, duration));
        }
    }
    
    
    private System.Collections.IEnumerator AnimateBlendShape(SkinnedMeshRenderer renderer, float duration)
    {
        float timer = 0.0f;

        while (timer <= duration)
        {
            timer += Time.deltaTime;
            float blendValue = Mathf.Lerp(0.0f, 100.0f, timer / duration); // Blendshape 값은 0에서 100 사이로 설정
            renderer.SetBlendShapeWeight(0, blendValue);
            yield return null;
        }

        // Ensure it ends at the target value
        renderer.SetBlendShapeWeight(0, 100.0f);
    }
}
