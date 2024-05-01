using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    public EnemyMovement enemyMovement;
    public EnemyHealth enemyHealth;
    public EnemyAttack enemyAttack;
    public SceneTransition sceneTransition;
    public float increasedSpeedHalf;
    public float increasedSpeedQuarter;
    public string NameOfNextScene;
    public GameObject objTransition;
    public float TimeToNextScene;

    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyAttack = GetComponent<EnemyAttack>();

        if (enemyMovement == null || enemyHealth == null || enemyAttack == null)
        {
            Debug.LogError("No se encontraron los componentes EnemyMovement, EnemyHealth o EnemyAttack en el jefe.");
        }
    }

    void Update()
    {
        if (enemyHealth.CurrentHealth <= enemyHealth.MaxHealth / 4)
        {
            enemyMovement.speed = increasedSpeedQuarter;
        }
        else if (enemyHealth.CurrentHealth <= enemyHealth.MaxHealth / 2)
        {
            enemyMovement.speed = increasedSpeedHalf;
        }
        DefeatBoss();
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
        sceneTransition.LoadScene("LVL-1-1");
    }
}
