using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    [SerializeField]
    int DefeatValue = 10;

    public void NewGame()
    {
        PlayerStats.Scores = 0;
        PlayerStats.LeftToLost = DefeatValue;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.CancelQuit();
	}
}
