using UnityEngine;
using System.Collections;

public class NGUIManager : MonoBehaviour
{
    static NGUIManager _instance;
    public static NGUIManager Instance()
    {
        return _instance;
    }

    public UILabel _label;

    public UILabel label
    {
        get
        {
            return _label;
        }
    }

    public void ChangeLabel(string s)
    {
        _label.text = s;
        TweenAlpha.Begin(_label.gameObject, 8.0f, 0.0f);
    }

    void Start()
    {
        if (_instance == null)
            _instance = this;
    }
}
