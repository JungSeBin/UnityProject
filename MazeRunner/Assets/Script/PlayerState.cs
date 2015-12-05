using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour 
{
    static PlayerState _instance;
    public static PlayerState Instance()
    {
        return _instance;
    }

    bool _hasKey = true;
    bool _hasPendant = false;

    public bool hasKey
    {
        get
        {
            return _hasKey;
        }
    }

    public bool hasPendant
    {
        get
        {
            return _hasPendant;
        }
    }

    public void GetKey()
    {
        _hasKey = true;
    }

    public void GetPendant()
    {
        _hasPendant = true;
    }

    void Start()
    {
        if(_instance == null)
        {
            _instance = this;
        }
    }
}
