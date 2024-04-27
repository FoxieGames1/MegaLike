using UnityEngine;

public class IdleTimer : MonoBehaviour
{
    public float maxIdleTime;
    [SerializeField] private float idleTime = 0.0f;
    public GameObject contentTitle;
    public GameObject contentSubtitle;
    public GameObject[] otherContents;
    private bool isTitleOpen = true;
    private GameObject[] activatedContents;

    void Start()
    {
        activatedContents = new GameObject[otherContents.Length];
    }

    void Update()
    {
        if (isTitleOpen && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return)))
        {
            CloseTitle();
            ActivateOtherContents();
        }
        else if (!Input.anyKey)
        {
            idleTime += Time.deltaTime;
            if (idleTime >= maxIdleTime && !isTitleOpen)
            {
                ShowTitle();
            }
            else if (idleTime >= maxIdleTime && isTitleOpen)
            {
                idleTime = 0.0f;
                DeactivateOtherContents();
            }
        }
        else
        {
            idleTime = 0.0f;
        }
    }

    void CloseTitle()
    {
        contentTitle.SetActive(false);
        contentSubtitle.SetActive(false);
        isTitleOpen = false;
        idleTime = 0.0f;
    }

    void ShowTitle()
    {
        contentTitle.SetActive(true);
        contentSubtitle.SetActive(true);
        isTitleOpen = true;
    }

    void ActivateOtherContents()
    {
        foreach (GameObject content in otherContents)
        {
            content.SetActive(true);
        }
    }

    void DeactivateOtherContents()
    {
        foreach (GameObject content in otherContents)
        {
            if (content != null)
            {
                content.SetActive(false);
            }
        }
    }

    public void ResetTime()
    {
        idleTime = 0.0f;
    }
}
