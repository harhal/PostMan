using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AttachController : MonoBehaviour
{
    RelativeJoint2D ControlledBox;
    BoxStats ControlledBoxStats;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if ((Input.GetMouseButton(0) || Input.touchCount > 0) && ControlledBox == null)
        {
            Vector3 CursorLocation = InputRaycastPlaneSingletone.GetCursorWorldLocation();

            int Layer = LayerMask.NameToLayer("Boxes");

            Collider2D Target = Physics2D.OverlapPoint(CursorLocation, 1 << Layer);

            if (Target != null)
            {
                CatchBox(Target.gameObject);
            }
        }
        else if (ControlledBox != null && (!Input.GetMouseButton(0)) && !(Input.touchCount > 0))
        {
            FreeBox();
        }
        if (ControlledBoxStats != null)
        {
            if (ControlledBoxStats.State.State == BoxState.Losted)
            {
                FreeBox();
            }
        }
    }

    void CatchBox(GameObject Target)
    {
        if (Target != null)
        {
            ControlledBoxStats = Target.GetComponent<BoxStats>();
            ControlledBox = Target.GetComponent<RelativeJoint2D>();

            if (ControlledBoxStats.State.State == BoxState.Losted)
            {
                FreeBox();
            }
            else
            {
                ControlledBox.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                ControlledBox.connectedBody = GetComponent<Rigidbody2D>();
                ControlledBox.enabled = true;
            }
        }
    }

    void FreeBox()
    {
        if (ControlledBox != null)
        {
            ControlledBox.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Discrete;
            ControlledBox.enabled = false;
            ControlledBox = null;
            ControlledBoxStats = null;
        }
    }
}
