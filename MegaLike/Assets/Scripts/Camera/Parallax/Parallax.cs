using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Vector3 previousCameraPosition;
    [SerializeField] private float parallaxMultiplier;

    [SerializeField] private bool moveHorizontally;
    [SerializeField] private bool moveVertically;

    [SerializeField] private float spriteWidth;
    [SerializeField] private float startPosition;

    private void Start()
    {
        previousCameraPosition = cameraTransform.position;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = spriteRenderer.bounds.size.x;
        startPosition = transform.position.x;
    }

    private void LateUpdate()
    {
        float deltaX = 0;
        float deltaY = 0;

        if (moveHorizontally)
        {
            deltaX = (cameraTransform.position.x - previousCameraPosition.x) * parallaxMultiplier;
        }
        if (moveVertically)
        {
            deltaY = (cameraTransform.position.y - previousCameraPosition.y) * parallaxMultiplier;
        }

        transform.Translate(new Vector3(deltaX, deltaY, 0));

        previousCameraPosition = cameraTransform.position;

        float moveAmount = 0;
        if (moveHorizontally)
        {
            moveAmount = cameraTransform.position.x * (1 - parallaxMultiplier);
        }
        else if (moveVertically)
        {
            moveAmount = cameraTransform.position.y * (1 - parallaxMultiplier);
        }

        if (moveAmount > startPosition + spriteWidth)
        {
            transform.Translate(new Vector3(spriteWidth, 0, 0));
            startPosition += spriteWidth;
        }
        else if (moveAmount < startPosition - spriteWidth)
        {
            transform.Translate(new Vector3(-spriteWidth, 0, 0));
            startPosition -= spriteWidth;
        }
    }
}
