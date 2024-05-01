using TMPro;
using UnityEngine;

public class NextScene : MonoBehaviour
{
    public GameObject levelCompleteUI;
    public TextMeshProUGUI pointsText;

    private void Start()
    {
        HideLevelCompleteUI(); 
    }

    public void ShowLevelCompleteUI()
    {
        levelCompleteUI.SetActive(true);
        
        //pointsText.text = "Points: " + PlayerPoints.points.ToString(); // Actualiza el texto con los puntos recolectados
    }

    public void HideLevelCompleteUI()
    {
        levelCompleteUI.SetActive(false); // Oculta la pantalla de nivel completado
    }

    public void NextLevelButtonOnClick()
    {

    }
}
