using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MapleManager : MonoBehaviour
{
    public Timer _timer;
    public GameObject _clearTextObject;
    public static bool _isOn = false;
    public static bool _isClear = false;
    public static string _nicknameToSend;
    public string _nickname;

    private bool _isUploadComplete = false;

    void Start()
    {
        _nickname = _nicknameToSend;
    }

    public static void SlowDown()
    {
        if(!_isOn)
        {
            Time.timeScale = 0.2f;
        }
    }

    private void Update() 
    {
        if(_isClear)
        {
            Clear();
            if(Input.GetKeyDown(KeyCode.Escape) && _isUploadComplete)
            {
                SceneManager.LoadScene("StartMenu");
            }
        }
    }

    public void Clear()
    {
        if(!_clearTextObject.activeSelf)
        {
            _clearTextObject.SetActive(true);
            Time.timeScale = 0f;
            StartCoroutine(SendDataToServer());
        }
    }

    public IEnumerator SendDataToServer()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("Nickname", _nickname));
        form.Add(new MultipartFormDataSection("ClearTime", _timer.GetTime()));
        UnityWebRequest webRequest = UnityWebRequest.Post("https://mit-games.kr/MAPLE_CONTEST/write_ranking.php", form);
        
        yield return webRequest.SendWebRequest();
        
        if(webRequest.isNetworkError || webRequest.isHttpError)
        {
            StartCoroutine(SendDataToServer());
            Debug.Log(webRequest.error);
        }
        else
        {
            Debug.Log("Form upload complete");
            _isUploadComplete = true;
        }
    }
}
