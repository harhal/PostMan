using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

    [SerializeField]
    Text Scores;
    	
    //FIXME::
	void Update ()
    {
        Scores.text = PlayerStats.Scores.ToString();
        PlayerStats.LeftToLost = 10;
    }

    public void Restart()
    {
        PlayerStats.Scores = 0;
        SceneManager.LoadScene(1);
    }


    public void Continue()
    {
        PlayerStats.Scores = 0;
        SceneManager.LoadScene(0);
    }
}
