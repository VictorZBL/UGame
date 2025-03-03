using System;
using Unity.VisualScripting;
using UnityEngine;

[SelectionBase] // Чтобы всегда тоскать объект, к которому привязан скрипт, а не дочерки
[RequireComponent(typeof(PolygonCollider2D))] // Автоматически добавит компонент, если его нет в инспекторе
[RequireComponent(typeof(BoxCollider2D))]
public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private EnemySO _enemySO;

    public event EventHandler OnTakeHit;
    public event EventHandler OnDeath;

    //[SerializeField] private int _maxHealth = 20;
    private int _currentHealth;
    private int _enemyScore;

    private PolygonCollider2D _polygonCollider2D;
    private BoxCollider2D _boxCollider2D;
    private EnemyAI _enemyAI;

    private void Awake()
    {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _enemyAI = GetComponent<EnemyAI>();
    }

    private void Start()
    {
        _enemyScore = _enemySO.enemyScore;
        _currentHealth = _enemySO.enemyHealth;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            player.TakeDamage(transform, _enemySO.enemyDamageAmount);
        }
    }
    public void PolygonColliderTurnOff()
    {
        _polygonCollider2D.enabled = false;
    }
    public void PolygonColliderTurnOn()
    {
        _polygonCollider2D.enabled = true;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        OnTakeHit?.Invoke(this, EventArgs.Empty);
        DetectDeath();
    }
    public void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            _boxCollider2D.enabled = false;
            _polygonCollider2D.enabled = false;

            _enemyAI.SetDeathState();

            OnDeath?.Invoke(this, EventArgs.Empty);

            if (Score.Instance != null)
            {
                Score.Instance.ScoreAdd(_enemyScore);
                Score.Instance.UpdateScoreText();
            }            
        }
    }
}
