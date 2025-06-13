using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public void Player_Button()
    {
        SceneManager.LoadScene(1);//playing
    }

    public void Howto_Button()
    {

    }
    public void Score_Button()
    {

    }
    public void Exit_Button()
    {
        Application.Quit();
    }
}
