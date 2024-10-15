using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class OceanPlayPage : PageHandler
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

    public void ExitScene()
    {
        switchSceneManager.SwitchScene("Title", "LoadingScene", () => {
            PageManager.ChangeImmediate("MainPage");
        });
    }
}
