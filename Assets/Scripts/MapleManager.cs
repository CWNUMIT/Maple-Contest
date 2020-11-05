using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapleManager : MonoBehaviour
{
    public GameObject _clearTextObject;
    public static bool isOn = false;
    public static bool isClear = false;

    public static void SlowDown()
    {
        if(!isOn)
        {
            Time.timeScale = 0.2f;
        }
    }

    private void Update() 
    {
        if(isClear)
        {
            Clear();
        }
    }

    public void Clear()
    {
        _clearTextObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
