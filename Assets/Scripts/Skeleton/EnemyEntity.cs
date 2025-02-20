using System;
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
        _currentHealth = _enemySO.enemyHealth;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Атака");
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
        }
    }


}
