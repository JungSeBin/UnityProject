using UnityEngine;
using System.Collections;

public class StartManager : MonoBehaviour
{
    public void StartScene()
    {
        StartCoroutine(StartMain());
    }
    IEnumerator StartMain()
    {
        AsyncOperation async = Application.LoadLevelAsync("MainScene");

        while (async.progress < 0.9f)
        {
            yield return new WaitForEndOfFrame();
        }

        while (true)
        {
            if (async.isDone)
                break;
            yield return new WaitForEndOfFrame();
        }

        async.allowSceneActivation = true;
    }
}
