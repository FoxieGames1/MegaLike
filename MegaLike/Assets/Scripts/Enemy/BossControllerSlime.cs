using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossControllerSlime : MonoBehaviour
{
    public EnemyMovement enemyMovement;
    public EnemyHealth enemyHealth;
    public EnemyAttack enemyAttack;
    public SceneTransition sceneTransition;
    public float scaleIncreaseHalf;
    public float scaleIncreaseQuarter;
    public float speedDecreaseHalf;
    public float speedDecreaseQuarter;
    public string NameOfNextScene;
    public GameObject objTransition;
    public float TimeToNextScene;


    private Vector3 initialScale;
    private float initialSpeed;

    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyAttack = GetComponent<EnemyAttack>();

        if (enemyMovement == null || enemyHealth == null || enemyAttack == null)
        {
            Debug.LogError("No se encontraron los componentes EnemyMovement, EnemyHealth o EnemyAttack en el jefe.");
        }

        // Guardar la escala y velocidad inicial del jefe
        initialScale = transform.localScale;
        initialSpeed = enemyMovement.speed;
    }

    void Update()
    {
        UpdateSizeAndSpeedBasedOnHealth();
        DefeatBoss();
    }

    void UpdateSizeAndSpeedBasedOnHealth()
    {
        if (enemyHealth.CurrentHealth <= enemyHealth.MaxHealth / 4)
        {
            transform.localScale = initialScale + Vector3.one * scaleIncreaseQuarter;
            enemyMovement.speed = initialSpeed - speedDecreaseQuarter;
        }
        else if (enemyHealth.CurrentHealth <= enemyHealth.MaxHealth / 2)
        {
            transform.localScale = initialScale + Vector3.one * scaleIncreaseHalf;
            enemyMovement.speed = initialSpeed - speedDecreaseHalf;
        }
    }

    public void DefeatBoss()
    {
        if (enemyHealth.CurrentHealth <= 0)
        {
            StartCoroutine(CoroutineNextScene());
        }
    }

    private IEnumerator CoroutineNextScene()
    {
        GameManager.Instance.ActivateTransition(objTransition);
        yield return new WaitForSeconds(TimeToNextScene);
        sceneTransition.LoadScene("GAME-OVER");
    }
}
