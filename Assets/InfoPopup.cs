using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPopup : MonoBehaviour {

    [SerializeField]
    Text Text;

    public void Destroy()
    {
        GameObject.Destroy(gameObject);
    }

    public void SetText(string text, Color color)
    {
        Text.text = text;
        Text.color = color;
    }
}
