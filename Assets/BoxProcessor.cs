using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxProcessor : MonoBehaviour {

    Collider2D Collider;

    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
    }

    public List<BoxStatsStruct> CollectBoxes()
    {
        ContactFilter2D Filter = new ContactFilter2D();
        Filter.SetLayerMask(LayerMask.GetMask("Boxes"));
        Collider2D[] arr = new Collider2D[512];
        int n = Collider.OverlapCollider(Filter, arr);
        List<BoxStatsStruct> OutArr = new List<BoxStatsStruct>();
        for (int i = 0; i < n; i++)
        {
            if (arr[i].GetComponent<BoxStats>().State.State == BoxState.Common)
            {
                arr[i].GetComponent<BoxStats>().State.State = BoxState.Collected;
                //FIXME:Add more scores for multy load
                arr[i].GetComponent<BoxStats>().State.ScoresMultiplyer = 1;
                OutArr.Add(arr[i].GetComponent<BoxStats>().State);
            }
            GameObject.Destroy(arr[i].gameObject);
        }
        return OutArr;
    }
}
