using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenu : MonoBehaviour
{
    public TMP_InputField _inputField;
    public TMP_Text _errorText;

    void Start()
    {
        _errorText.alpha = 0;
        _errorText.gameObject.SetActive(false);
    }

    void Update() 
    {
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
        if(_inputField.text.Length >  3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void LoadRanking()
    {
        
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit() // 어플리케이션 종료
        #endif
    }
}
