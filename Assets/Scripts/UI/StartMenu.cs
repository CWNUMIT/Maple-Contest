using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenu : MonoBehaviour
{
    public TMP_InputField _inputField;
    public TMP_Text _errorText;
    public Transform _rankingScrollBar;
    public GameObject _rankingObjPref;

    public GameObject _startMenu;
    public GameObject _waitToStartText;

    void Start()
    {
        _errorText.alpha = 0;
        _errorText.gameObject.SetActive(false);

        _waitToStartText.SetActive(true);
        _startMenu.SetActive(false);

        LoadRanking();
    }

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Return) && _waitToStartText.activeSelf && !_startMenu.activeSelf)
        {
            _startMenu.SetActive(true);
            _waitToStartText.SetActive(false);
        }

        if(_errorText.IsActive())
        {
            _errorText.alpha -= Time.unscaledDeltaTime;
            if(_errorText.alpha < 0.1)
            {
                _errorText.gameObject.SetActive(false);
            }
        }
    }

    public void StartGame()
    {
        if(CheckInputName())
        {
            MapleManager._nicknameToSend = _inputField.text;
            SceneManager.LoadScene("Main");
        }
        else
        {
            _inputField.text = "";
            _errorText.alpha = 1;
            _errorText.gameObject.SetActive(true);
        }
    }

    bool CheckInputName()
    {
        if( 20 > _inputField.text.Length && _inputField.text.Length >  3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void LoadRanking()
    {
        StartCoroutine(LoadRankingData());
    }

    IEnumerator LoadRankingData()
    {

        UnityWebRequest webRequest = UnityWebRequest.Get("https://mit-games.kr/MAPLE_CONTEST/load_ranking.php");

        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            string[] stringSeparators = new string[] { "\n" };
            string[] lines = webRequest.downloadHandler.text.Split(stringSeparators, System.StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(':');
                string nickname = parts[0];
                string clearTime = parts[1];

                string str = string.Format("{0} - {1}", nickname, clearTime);
                AddRankingObject(i, str);
            }
        }
    }

    void AddRankingObject(int i, string data)
    {
        GameObject obj = Instantiate(_rankingObjPref);
        obj.transform.SetParent(_rankingScrollBar);

        Vector3 pos = Vector3.zero;
        Vector3 scale = Vector3.one;
        pos.y += (i * -15 + 115);

        obj.GetComponent<RectTransform>().anchoredPosition = pos;
        obj.GetComponent<RectTransform>().localScale = scale;
        obj.GetComponent<TMP_Text>().text = data;
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit(); // 어플리케이션 종료
        #endif
    }
}
