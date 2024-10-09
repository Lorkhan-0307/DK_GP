using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MainPage : PageHandler
{
    private SwitchSceneManager switchSceneManager;
    public override void OnWillEnter(object param)
    {
        switchSceneManager = FindObjectOfType<SwitchSceneManager>();
    }

    public override void OnDidEnter(object param)
    {
    }

    public override void OnWillLeave()
    {
    }

    public override void OnDidLeave()
    {
    }
    
    public void OnClickCharacterSelect(string character)
    {
        
        switch (character)
        {
            // case에 따라 SwitchSceneManager를 통한 Scene 전환
            // 현재 하나의 캐릭터에 대응되는 씬만 존재하므로 이에 맞춰서 제작... 
            // TODO : 각 캐릭터별 SSM에 mainSceneName, Action의 Page name 수정 필요
            case "Flower":
                switchSceneManager.SwitchScene("Title", "Flower_Map", () => {
                    PageManager.ChangeImmediate("FlowerPlayPage");
                });
                Debug.Log("Flower selected");
                break;
            case "Crystal":
                switchSceneManager.SwitchScene("Title", "Crystal_Map", () => {
                    PageManager.ChangeImmediate("CrystalPlayPage");
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