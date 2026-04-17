using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] Animator animator;
    [SerializeField] private AudioSource deathSFX;
    public int currentHealth;
    
    [SerializeField] private HealthBar _healthBar;

    private GameEndManager _gameEndManager;

    private void Start()
    {
       currentHealth = maxHealth;
       _healthBar.SetMaxHealth(maxHealth);
       _gameEndManager = FindObjectOfType<GameEndManager>();
    }
    public void TakeDamage(int damage) 
    {

            if (currentHealth <= 0)
                return;
        
            currentHealth -= damage;
            currentHealth = Mathf.Max(currentHealth, 0);
            Debug.Log(currentHealth);
            _healthBar.SetHealth(currentHealth);
            CheckState();
    }

    public void CheckState()
    {
        if (currentHealth <= 0) 
        {
                animator.SetTrigger("Boss_Death");
                StartCoroutine(AudioEffect());
                Debug.Log("Won");

                if (_gameEndManager != null)
                {
                    StartCoroutine(WinAfterAnim());
                }
        }
    }

    IEnumerator AudioEffect()
    {
            yield return new WaitForSeconds(0.2f);
            deathSFX.Play();
    }
    
    IEnumerator WinAfterAnim()
    {
            yield return new WaitForSeconds(3f);
            _gameEndManager.Win();
    }
}
