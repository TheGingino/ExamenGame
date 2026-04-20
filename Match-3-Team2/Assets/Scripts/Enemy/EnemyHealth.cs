using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] Animator animator;
    [SerializeField] private AudioSource deathSFX;
    [SerializeField] private HealthBar healthBar;
    
    public int _currentHealth;
    private GameEndManager _gameEndManager;
   
    private void Start()
    {
        _currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        _gameEndManager = FindObjectOfType<GameEndManager>();
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth <= 0) return;

        _currentHealth -= damage;
        _currentHealth = Mathf.Max(_currentHealth, 0);

        Debug.Log(_currentHealth);
        healthBar.SetHealth(_currentHealth);

        CheckState();
    }

        public void CheckState()
        {
            if (_currentHealth <= 0)
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
            _gameEndManager.TriggerWin();
        }
}