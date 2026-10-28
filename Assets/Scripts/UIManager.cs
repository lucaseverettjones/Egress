using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    public void LoadScene(string toLoad)
    {
        SceneManager.LoadScene(toLoad);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
