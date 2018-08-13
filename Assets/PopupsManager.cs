using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupsManager : MonoBehaviour {

    public static PopupsManager Manager;

    [SerializeField]
    Camera MainCamera;

    [SerializeField]
    InfoPopup PopupPrototype;
    
	void Awake ()
    {
        Manager = this;
    }

    public void CreatePopup(string Text, Color Color, Vector3 Position)
    {
        InfoPopup NewPopoup = GameObject.Instantiate<InfoPopup>(PopupPrototype, transform);
        NewPopoup.transform.parent = transform;
        NewPopoup.SetText(Text, Color);
        RectTransform RTransf = NewPopoup.GetComponent<RectTransform>();
        print(MainCamera.WorldToScreenPoint(Position));
        RTransf.position = MainCamera.WorldToScreenPoint(Position);
    }
}
