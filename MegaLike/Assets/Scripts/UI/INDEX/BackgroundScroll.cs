using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed = 1f;
    public float maxLimitScroll = -10f;

    void Update()
    {
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        if (transform.position.x < maxLimitScroll)
        {
            RepositionBackground();
        }
    }

    void RepositionBackground()
    {
        Vector3 newPos = transform.position;
        newPos.x += 40f;
        transform.position = newPos;
    }
}
