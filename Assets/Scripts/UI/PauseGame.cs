using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public bool IsPaused { private set; get; } = false;

    [SerializeField]
    private GameObject PauseMenu;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Resume()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        IsPaused = true;
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Switch()
    {
        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0f : 1f;
        PauseMenu.SetActive(IsPaused);

        Cursor.lockState = IsPaused? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = IsPaused;
    }
}
