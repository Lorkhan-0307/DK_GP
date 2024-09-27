using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Director : MonoBehaviour
{
    [SerializeField] private SwitchSceneManager switchSceneManager;
    
    public void OnClickCharacterSelect(string character)
    {
        
        switch (character)
        {
            // case에 따라 SwitchSceneManager를 통한 Scene 전환
            // 현재 하나의 캐릭터에 대응되는 씬만 존재하므로 이에 맞춰서 제작... 
            // TODO : 각 캐릭터별 SSM에 mainSceneName, Action의 Page name 수정 필요
            case "Flower":
                switchSceneManager.SwitchScene("Title", "FlowerPlayScene", () => {
                    PageManager.ChangeImmediate("FlowerPlayPage");
                });
                Debug.Log("Flower selected");
                break;
            case "Crystal":
                switchSceneManager.SwitchScene("Title", "FlowerPlayScene", () => {
                    PageManager.ChangeImmediate("FlowerPlayPage");
                });
                Debug.Log("Crystal selected");
                break;
            case "Tree":
                switchSceneManager.SwitchScene("Title", "FlowerPlayScene", () => {
                    PageManager.ChangeImmediate("FlowerPlayPage");
                });
                Debug.Log("Tree selected");
                break;
            default:
                Debug.Log("Unknown character selected");
                break;
        }
    }
}

