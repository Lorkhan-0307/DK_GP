using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePlayPage : PageHandler
{
    private SwitchSceneManager switchSceneManager;
    private CameraRotationBasedOnMouse mainCamera;
    public override void OnWillEnter(object param)
    {
        switchSceneManager = FindObjectOfType<SwitchSceneManager>();
        mainCamera = FindObjectOfType<CameraRotationBasedOnMouse>();
        mainCamera.hasStarted = true;

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
