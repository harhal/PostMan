using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRaycastPlaneSingletone : MonoBehaviour {
    
    static BoxCollider Plane;
    
    void Awake ()
    {
        Plane = GetComponent<BoxCollider>();
    }

    public static Vector3 GetCursorWorldLocation()
    {
        Ray MouseRay;
        if (Input.touchCount <= 0)
        {
            MouseRay = CameraSingletone.Camera.ScreenPointToRay(Input.mousePosition);
        }
        else
        {
            MouseRay = CameraSingletone.Camera.ScreenPointToRay(Input.touches[0].position);
        }

        RaycastHit Hit;

        int Layer = LayerMask.NameToLayer("InputRaycastPlane");

        Physics.Raycast(MouseRay, out Hit, Mathf.Infinity, 1 << Layer);

        Debug.DrawRay(MouseRay.origin, MouseRay.direction * 1000);

        if (Hit.collider != Plane)
        {
            return new Vector3();
        }

        return Hit.point;
    }
}
