using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour 
{
    static TimeManager _instance = null;
    public static TimeManager Instance()
    {
        return _instance;
    }

    static int _day = 1;
    static int _hour = 0;
    static int _min = 0;

    static float time = 0;

    static UILabel label = null;

    public int day
    {
        get
        {
            return _day;
        }
    }
    public int hour
    {
        get
        {
            return _hour;
        }
    }

    public int min
    {
        get
        {
            return _min;
        }
    }

    void Start()
    {
        if(_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

        label = GetComponent<UILabel>();
        //StartCoroutine(Tick(0.2f));
    }

    void Update()
    {
        time += Time.deltaTime;

        if(time >= 0.5f)
        {
            Tick();
            time = 0.0f;
        }
    }

    static void Tick()
    {
            _min++;
            if (_min >= 60)
            {   
                _min = 0;
                _hour++;
            }   
            if (_hour >= 1)
            {
                _hour = 0;
                _day++;
            }

            label.text = "Day: " + _day.ToString() + "     " + "Time: " + _hour.ToString() + " : " + _min.ToString();
    }
}