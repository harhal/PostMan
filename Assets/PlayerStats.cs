using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {

    public static PlayerStats Player;

    [SerializeField]
    IntOutput ScoresOutput;

    [SerializeField]
    IntOutput LeftToLostOutput;

    public static int Scores = 0;

    public static int LeftToLost = 15;

    private void Awake()
    {
        Player = this;
        ScoresOutput.RefreshData(Scores);
        LeftToLostOutput.RefreshData(LeftToLost);
    }

    public void AddScores (int Scores)
    {
        PlayerStats.Scores += Scores;
        ScoresOutput.RefreshData(PlayerStats.Scores);
    }
	
	public void LostBox () {
        LeftToLost--;
        LeftToLostOutput.RefreshData(LeftToLost);
        if (LeftToLost <= 0)
        {
            FinishGame();
        }
    }

    void FinishGame()
    {
        SceneManager.LoadScene(2);
    }
}
