using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GUIController : MonoBehaviour
{
    public GameObject GameOverPanel;
    public Button RestartButton;
    public TextMeshProUGUI Text;

    void Start()
    {
        RestartButton.onClick.AddListener(()=> { SceneManager.LoadScene(SceneManager.GetActiveScene().name); Time.timeScale = 1; });
    }

    public void SetPlayerChangedStateGuiBehaviour(PlayerState state)
    {
        if(state!= PlayerState.Dead)
        {
            Text.text = string.Format("Current State: {0}",state.ToString());
        }
        else
        {
            GameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
