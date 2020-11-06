using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text _timeText;
    public float _initTime = 60f;
    public float _endTime = 0f;
    private float _t;

    void Start()
    {
        _t = _initTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(_t > 0 && !MapleManager._isClear)
        {
            _t -= Time.unscaledDeltaTime;
            string text = TimeToString(_t);

            _timeText.text = text;
        }
    }

    public string GetTime() { return TimeToString(_t); }

    public string TimeToString(float t)
    {
        int integerPart = Mathf.FloorToInt(t);
        int fractionalPart = (int)((t - integerPart) * 100);

        string text = string.Format("{0:D2}:{1:D2}", integerPart, fractionalPart);

        return text;
    }
}
