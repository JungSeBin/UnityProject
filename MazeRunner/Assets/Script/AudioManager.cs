using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioClip characWalk = null;

    static public AudioManager _instance = null;
    public static AudioManager Instance
    {
        get { return _instance; }
    }

    void Start()
    {
        if (_instance == null)
            _instance = this;

        GetComponent<AudioSource>().clip = null;
        GetComponent<AudioSource>().loop = false;
        StartCoroutine(Loop(0.5f));
    }

    public void PlaySfx()
    {
        GetComponent<AudioSource>().PlayOneShot(characWalk);
    }

    public void PlayLoop(AudioClip clip)
    {
        GetComponent<AudioSource>().clip = clip;
    }

    public void EndLoop()
    {
        GetComponent<AudioSource>().clip = null;
    }

    IEnumerator Loop(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            Debug.Log("Play");
            GetComponent<AudioSource>().Play();
        }
    }
}
