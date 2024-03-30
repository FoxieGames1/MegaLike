using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowTargetCamera : MonoBehaviour
{
    public Transform player;
    public Transform pointA;
    public Transform pointB;

    public float smoothSpeed;
    public Vector3 offset;

    private Vector3 targetPosition;

    private void Update()
    {
        float clampedX = Mathf.Clamp(player.position.x, pointA.position.x, pointB.position.x);
        float clampedY = Mathf.Clamp(player.position.y, pointA.position.y, pointB.position.y);
        targetPosition = new Vector3(clampedX, clampedY, -10) + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
