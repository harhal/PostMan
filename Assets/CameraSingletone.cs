using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSingletone : MonoBehaviour {

    public static Camera Camera { get; private set;}
    
	void Awake ()
    {
        Camera = GetComponent<Camera>();
    }
}
