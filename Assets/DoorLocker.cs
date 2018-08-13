using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLocker : MonoBehaviour {

    [SerializeField]
    Collider2D Lock;

    [SerializeField]
    Collider2D Open;

    public void UnLock()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<DistanceJoint2D>().enabled = true;
        GetComponent<DistanceJoint2D>().connectedBody = Open.attachedRigidbody;
    }

    public void Break()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<DistanceJoint2D>().enabled = false;
    }

    public void TryLock()
    {
        GetComponent<DistanceJoint2D>().enabled = true;
        GetComponent<DistanceJoint2D>().connectedBody = Lock.attachedRigidbody;
    }

    public bool CheckLock()
    {
        return GetComponent<Collider2D>().OverlapPoint(Lock.transform.position);
    }

    public void Freeze()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == Lock)
        {
            CheckLock();
        }
    }
}
