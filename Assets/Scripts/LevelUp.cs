using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUp : MonoBehaviour {
    private void Start()
    {
        
    }
    public void OnClick()
    {
        if(SceneManager.GetActiveScene().name == "menuScene")
        {
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            SceneManager.LoadScene("menuScene");
        }
        
    }
}
