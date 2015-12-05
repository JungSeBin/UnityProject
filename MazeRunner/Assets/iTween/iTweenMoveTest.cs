using UnityEngine;
using System.Collections;

public class iTweenMoveTest : MonoBehaviour
{
    public Transform target;
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Hashtable hash = new Hashtable();
            hash.Add("position", target);
            hash.Add("orienttopath", true);
            hash.Add("looktime", 2.0f);
            hash.Add("easetype", iTween.EaseType.easeInOutExpo);
            hash.Add("time", 2.0f);
            iTween.MoveTo(gameObject, hash);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Hashtable hash = new Hashtable();
            hash.Add("scale", Vector3.one * 2);
            hash.Add("time", 2.0f);
            hash.Add("easetype", iTween.EaseType.easeOutElastic);
            iTween.ScaleTo(gameObject, hash);
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            Hashtable hash = new Hashtable();
            hash.Add("y", 1080);
            hash.Add("time", 2.0f);
            hash.Add("easetype", iTween.EaseType.easeOutExpo);
            iTween.RotateTo(gameObject, hash);
        }
	}
}
