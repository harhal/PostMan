using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntOutput : MonoBehaviour {

    [SerializeField]
    Text Value;

    public void RefreshData(int NewValue)
    {
        Value.text = NewValue.ToString();
    }
}
