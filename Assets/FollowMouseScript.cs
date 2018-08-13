using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouseScript : MonoBehaviour
{
	void Update ()
    {
        transform.position = InputRaycastPlaneSingletone.GetCursorWorldLocation();
    }
}
