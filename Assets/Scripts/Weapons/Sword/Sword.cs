using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sword : MonoBehaviour
{
    [SerializeField] private int _damageAmount = 5;

    public event EventHandler OnSwordSwing;

    private PolygonCollider2D _polygonCollider2D;

    private void Awake()
    {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
    }
    private void Start()
    {
        AttackColliderTurnOff();
    }

    public void Attack()
    {
        //Debug.Log("Мэч");
        AttackColliderTurnOffOn();
        OnSwordSwing?.Invoke(this, EventArgs.Empty);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity)) // Получаем данные о соприкосновении с коллайдером противника
        {
            enemyEntity.TakeDamage(_damageAmount);
        }
    }

    public void AttackColliderTurnOff()
    {
        _polygonCollider2D.enabled = false;
    }
    private void AttackColliderTurnOn()
    {
        _polygonCollider2D.enabled = true;
    }
    private void AttackColliderTurnOffOn()
    {
        AttackColliderTurnOff();
        AttackColliderTurnOn();
    }
}
