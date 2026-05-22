using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    private bool _enemyTurn;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _attackSFX;

    private void Awake()
    {
        _playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void DoDamage()
    {
        StartCoroutine(AttackRoutine());
    }

    private void RollDamage()
    {
        int roll = Random.Range(0, 100);
        int damage;
        if (roll < 50) damage = 1;      // 50% chance
        else if (roll < 75) damage = 2; // 25%
        else if (roll < 90) damage = 3; // 15%
        else damage = 4;                // 10%
        Debug.Log("damage" + damage);
        _playerHealth.TakeDamage(damage);
    }

    public IEnumerator AttackRoutine()
    {
        int attackIndex = Random.Range(1, 4);
        _animator.SetTrigger("Attack_" + attackIndex);
        _attackSFX.Play();
        yield return new WaitForSeconds(1f);
        RollDamage();
    }
}