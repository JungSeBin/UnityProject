using UnityEngine;
using System.Collections;

public class EndingWarp : MonoBehaviour 
{
    public GameObject endingRoom;

    void OnTriggerEnter()
    {
        if (PlayerState.Instance().hasKey)
        {
            StartCoroutine(LoadScene());
        }
    }
    IEnumerator LoadScene()
    {
        AsyncOperation async = Application.LoadLevelAsync("EndingScene");

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
