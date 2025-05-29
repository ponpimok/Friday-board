using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenegameManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    public void PauseUI()
    {
        if (!pauseUI.active)
        {
            pauseUI.SetActive(true);
        }
        else
        {
            pauseUI.SetActive(false);
        }
    }
    public void Howto_Button()
    {

    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);//main_menu
    }
    public void Exit()
    {
        Application.Quit();
    }
    // Start is called before the first frame update
    private void Start()
    {
        pauseUI.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUI();
        }
    }
}
