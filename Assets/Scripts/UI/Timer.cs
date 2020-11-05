using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text _timeText;
    public float _initTime = 60f;
    public float _endTime = 0f;
    private float t;

    void Start()
    {
        t = _initTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(t > 0 && !MapleManager.isClear)
        {
            t -= Time.unscaledDeltaTime;
            int integerPart = Mathf.FloorToInt(t);
            int fractionalPart = (int)((t - integerPart) * 100);

            string text = string.Format("{0:D2}:{1:D2}", integerPart, fractionalPart);

            _timeText.text = text;
        }
    }
}
