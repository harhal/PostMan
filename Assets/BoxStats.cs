using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoxState
{
    Common,
    Collected,
    Losted
}
public enum BoxType
{
    Fragile
}

[System.Serializable]
public struct BoxStatsStruct
{
    public int Scores;
    public BoxState State;
    public float ScoresMultiplyer;
}

public class BoxStats : MonoBehaviour {

    public BoxStatsStruct State;

    [SerializeField]
    float DestroyDelay;

    Collider2D Collider;

    // Use this for initialization
    void Awake () {
        Collider = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<SafeZone>() != null && State.State == BoxState.Common)
        {
            ContactFilter2D Filter = new ContactFilter2D();
            Filter.SetLayerMask(LayerMask.GetMask("SafeZone"));
            Filter.useTriggers = true;
            Collider2D[] arr = new Collider2D[512];
            if (Collider.OverlapCollider(Filter, arr) <= 0)
            {
                gameObject.layer = LayerMask.NameToLayer("LostedBoxes");
                State.State = BoxState.Losted;
                PopupsManager.Manager.CreatePopup("Lost", Color.red, transform.position);
                PlayerStats.Player.LostBox();
                GameObject.Destroy(gameObject, DestroyDelay);
            }
        }
    }
}
