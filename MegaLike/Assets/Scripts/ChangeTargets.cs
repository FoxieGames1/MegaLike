using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTargets : MonoBehaviour
{
    public Transform TargetChangeA;
    public Transform TargetChangeB;

    public FollowTargetCamera TARGETSCAM;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            TARGETSCAM.pointA = TargetChangeA;
            TARGETSCAM.pointB = TargetChangeB;
        }
    }
}
