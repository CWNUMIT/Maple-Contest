using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MapleManager : MonoBehaviour
{
    public Timer _timer;
    public GameObject _clearTextObject;
    public string _nickname = "";

    public static bool _isOn;
    public static bool _isClear;
    public static string _nicknameToSend = "";

    private bool _isUploadComplete = false;
    private bool _canOverWrite = true;

    void Start()
    {
        Time.timeScale = 1f;
        _isOn = false;
        _isClear = false;
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Main");
        }
    }

    public void Clear()
    {
        if(!_clearTextObject.activeSelf)
        {
            _clearTextObject.SetActive(true);
            Time.timeScale = 0f;
            if(_nickname != "")
            {
                WriteRanking();
            }
            else
            {
                _isUploadComplete = true;
            }
        }
    }

    void WriteRanking()
    {
        StartCoroutine(SendDataToServer());
    }

    IEnumerator CanOverwriteRanking(List<IMultipartFormSection> form)
    {
        UnityWebRequest webRequest = UnityWebRequest.Post("https://mit-games.kr/MAPLE_CONTEST/write_ranking.php", form);

        yield return webRequest.SendWebRequest();
    }

    IEnumerator SendDataToServer()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("Nickname", _nickname));
        form.Add(new MultipartFormDataSection("ClearTime", _timer.GetTime().ToString()));
        
        yield return CanOverwriteRanking(form);

        if(_canOverWrite)
        {
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
        else
        {
            Debug.Log("기록을 갱신하지 못했습니다.");
        }
    }
}
