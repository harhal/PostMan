using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrackState
{
    Arriving,
    Opening,
    Closing,
    Leaving,
    GiveReward,
    Wait
}

[System.Serializable]
public struct FASMState
{
    public TrackState State;
    public float StateTime;
    public int NextState;
}

public class TrackController : MonoBehaviour {

    [SerializeField]
    AudioClip DoorBrokenSound;

    [SerializeField]
    AudioClip CloseSound;

    [SerializeField]
    AudioClip LeavingSound;

    [SerializeField]
    DoorLocker Door;

    [SerializeField]
    float LockBreakTimer = 5;

    [SerializeField]
    Vector3 LoadingLocation;

    [SerializeField]
    Vector3 WaitingLocation;

    [SerializeField]
    Vector3 TrackSize = new Vector3(9, 3, 1);

    [SerializeField]
    FASMState[] StateMachine;

    [SerializeField]
    int CurrentState = 0;

    [SerializeField]
    float StateLeftTime;

    [SerializeField]
    int NextState;

    [SerializeField]
    MeshRenderer InvisibleWall;

    [SerializeField]
    BoxProcessor BoxProcessor;

    [SerializeField]
    ProgressBar ProgressBar;

    Rigidbody2D RigidBody;

    List<BoxStatsStruct> CollectedBoxes;

    bool AllowedProcessReward = false;

    AudioSource Audio;

    [SerializeField]
    float RewardDelay;

    float CurrentRewardDelay;

    private void Awake()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        CollectedBoxes = new List<BoxStatsStruct>();
        Audio = GetComponent<AudioSource>();
    }

    void Update ()
    {
        bool JustStarted = false;
        if (StateLeftTime > 0)
        {
            StateLeftTime -= Time.deltaTime;
        }
        else
        {
            CurrentState = NextState;
            StateLeftTime = GetState().StateTime;
            NextState = GetState().NextState;
            JustStarted = true;
        }

        switch(GetState().State)
        {
            case (TrackState.Arriving):
                if (JustStarted)
                {
                    Audio.Play();
                }
                RigidBody.MovePosition(Vector3.Lerp(LoadingLocation, WaitingLocation, Mathf.InverseLerp(0, GetState().StateTime, StateLeftTime)));
                break;
            case (TrackState.Opening):
                if (JustStarted)
                {
                    Door.UnLock();
                    InvisibleWall.enabled = false;
                }
                break;
            case (TrackState.Closing):
                if (JustStarted)
                {
                    Door.TryLock();
                    if (CurrentState == 3)
                    {
                        Audio.clip = CloseSound;
                        Audio.Play();
                    }
                    ProgressBar.Hide();
                }
                if (Door.CheckLock())
                {
                    StateLeftTime = 0;
                    Door.Freeze();
                }
                break;
            case (TrackState.Leaving):
                if (JustStarted)
                {
                    Audio.clip = LeavingSound;
                    Audio.Play();
                    ProgressBar.Hide();
                    if (Door.CheckLock())
                    {
                        Door.Freeze();
                        CollectedBoxes.AddRange(BoxProcessor.CollectBoxes());
                    }
                    else
                    {
                        Door.Break();
                        //Audio.clip = DoorBrokenSound;
                        //Audio.Play();
                    }
                    InvisibleWall.enabled = true;
                }
                RigidBody.MovePosition(Vector3.Lerp(WaitingLocation, LoadingLocation, Mathf.InverseLerp(0, GetState().StateTime, StateLeftTime)));
                break;
            case (TrackState.GiveReward):
                if (JustStarted)
                {
                    CollectedBoxes.AddRange(BoxProcessor.CollectBoxes());
                    AllowedProcessReward = true;
                }
                break;
            case (TrackState.Wait):
                if (JustStarted)
                {
                    ProgressBar.Show();
                }
                ProgressBar.RefreshValue(Mathf.InverseLerp(0, GetState().StateTime, StateLeftTime));
                break;
        }
        TickReward();
    }

    void TickReward()
    {
        if (AllowedProcessReward)
        {
            if (CurrentRewardDelay > 0)
            {
                CurrentRewardDelay -= Time.deltaTime; ;
            }
            else
            {
                if (CollectedBoxes.Count > 0)
                {
                    CurrentRewardDelay = RewardDelay;
                    PlayerStats.Player.AddScores((int)(CollectedBoxes[0].Scores * CollectedBoxes[0].ScoresMultiplyer));
                    PopupsManager.Manager.CreatePopup("+ " + ((int)(CollectedBoxes[0].Scores * CollectedBoxes[0].ScoresMultiplyer)).ToString(), Color.yellow, LoadingLocation);
                    CollectedBoxes.RemoveAt(0);
                }
                else
                {
                    AllowedProcessReward = false;
                }
            }
        }
    }

    public void SetState(int NewState)
    {
        NextState = NewState;
        StateLeftTime = 0;
    }

    FASMState GetState()
    {
        return StateMachine[CurrentState];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(Color.blue.r, Color.blue.g, Color.blue.b, 0.8f);
        Gizmos.DrawCube(LoadingLocation, TrackSize);
        Gizmos.color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.8f);
        Gizmos.DrawCube(WaitingLocation, TrackSize);
    }
}
