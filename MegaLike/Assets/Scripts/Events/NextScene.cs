using TMPro;
using UnityEngine;

public class NextScene : MonoBehaviour
{
    public GameObject levelCompleteUI;
    public TextMeshProUGUI pointsText;
    //public Button nextLevelButton;

    private void Start()
    {
        HideLevelCompleteUI(); // Oculta la pantalla de nivel completado al inicio
    }

    public void ShowLevelCompleteUI()
    {
        levelCompleteUI.SetActive(true); // Muestra la pantalla de nivel completado
        
        //pointsText.text = "Points: " + PlayerPoints.points.ToString(); // Actualiza el texto con los puntos recolectados
    }

    public void HideLevelCompleteUI()
    {
        levelCompleteUI.SetActive(false); // Oculta la pantalla de nivel completado
    }

    // M�todo para manejar el evento del bot�n para continuar al siguiente nivel
    public void NextLevelButtonOnClick()
    {
        // Aqu� puedes cargar el siguiente nivel o realizar cualquier otra acci�n necesaria
    }
}
