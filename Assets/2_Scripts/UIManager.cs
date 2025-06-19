using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Text timeText;
    public Text surfaceSpeedText;
    public Text carSpeedText;
    public Text currentTimeText;

    public Text endingMessageText;    // ← 새로 연결할 Ending Message 텍스트
    public Text fastTimeText;
    public GameObject panel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void ShowEndingMessage(string msg)
    {
        if (endingMessageText == null) return;

        endingMessageText.text = msg;
        endingMessageText.gameObject.SetActive(!string.IsNullOrEmpty(msg));
    }
    public void UpdateTimeText(string time)
    {
        timeText.text = time;
    }

    public void UpdateSurfaceText(string speed)
    {
        surfaceSpeedText.text = speed;
    }

    public void UpdateCarSpeedText(string speed)
    {
        carSpeedText.text = speed;
    }

    public void UpdateCurrentTimeText(string time)
    {
        currentTimeText.text = time;
    }

    public void UpdateFastTimeText(string time)
    {
        fastTimeText.text = time;
    }



    public void ShowPanel()
    {
        panel.SetActive(true);
    }

    public void HidePanel()
    {
        panel.SetActive(false);

        ShowEndingMessage("");
    }

    public void GameRestart()
    {
        GameManager.Instance.GameRestart();
    }
    public void GameStop()
    {
        Application.Quit();
    }
}
