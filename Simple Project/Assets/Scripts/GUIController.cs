using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GUIController : MonoBehaviour
{
    public GameObject GameOverPanel;
    public GameObject WinPanel;
    public Button[] RestartButtons;
    public TextMeshProUGUI Text;

    void Start()
    {
        foreach (Button restartButton in RestartButtons)
        {
            restartButton.onClick.AddListener(() => { SceneManager.LoadScene(SceneManager.GetActiveScene().name); Time.timeScale = 1; });
        }
    }

    public void SetPlayerChangedStateGuiBehaviour(PlayerState state, bool isPlayerCrouching)
    {
        if(state!= PlayerState.Dead && state!= PlayerState.Finished)
        {
            if (!isPlayerCrouching || state == PlayerState.Crouching)
            {
                Text.text = string.Format("Current State: {0}", state.ToString());
            }
            else
            {
                Text.text = string.Format("Current State: Crouching & {0}", state.ToString());
            }
        }else if(state == PlayerState.Finished)
        {
            WinPanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            GameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
