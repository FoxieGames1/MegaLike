using UnityEngine;
using TMPro;

public class PlayerPoints : MonoBehaviour
{
    public int points = 0;
    public TMP_Text pointsText;

    public void AddPoints(int amount)
    {
        points += amount;
        UpdatePointsUI();
    }

    private void UpdatePointsUI()
    {
        pointsText.text = "Points: " + points.ToString();
    }
}
