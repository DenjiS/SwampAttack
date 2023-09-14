using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private Player _player;

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
        _player.enabled = false;
        Time.timeScale = 0;
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        _player.enabled = true;
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
