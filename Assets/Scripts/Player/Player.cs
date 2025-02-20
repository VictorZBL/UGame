using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[SelectionBase] // Чтобы всегда тоскать объект, к которому привязан скрипт, а не дочерки
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public event EventHandler OnPlayerDeath;

    [SerializeField] private float _movingSpeed = 5f;
    [SerializeField] private int _maxHealth = 10;
    [SerializeField] private float _damageRecoveryTime = 0.5f;

    private Vector2 _inputVector;

    private Rigidbody2D _rb;
    private KnockBack _knockBack;

    private float _minMovingSpeed = 0.1f;
    private bool _isRunning = false;

    private int _currentHealth;
    private bool _canTakeDamage;
    private bool _isAlive;

    private void Awake()
    {
        Instance = this;
        _rb = GetComponent<Rigidbody2D>();
        _knockBack = GetComponent<KnockBack>();
    }
    private void Start()
    {
        _currentHealth = _maxHealth;
        _canTakeDamage = true;
        _isAlive = true;
        GameInput.Instance.OnPlayerAttack += GameInput_OnPlayerAttack;
    }

    private void Update()
    {
        _inputVector = GameInput.Instance.GetMovementVector();
    }
    private void FixedUpdate()
    {
        if (_knockBack.IsGettingKnockBack)
        {
            return;
        }
        HandleMovement();
    }
    public bool IsAlive() => _isAlive; // То же что и с фигурными скобками, только короче
    public void TakeDamage(Transform damageSource, int damage)
    {
        if (_canTakeDamage && _isAlive)
        {
            _canTakeDamage = false;
            _currentHealth = Mathf.Max(0, _currentHealth -= damage); // Конструкция чтобы хп не могло уйти в минус
            //Debug.Log(_currentHealth);
            _knockBack.GetKnockBack(damageSource);

            StartCoroutine(DamageRecoveryRuotine());
        }
        DeteckDeath();
    }
    private void DeteckDeath()
    {
        if (_currentHealth == 0 && _isAlive)
        {
            _isAlive = false;
            _knockBack.StopKnockBackMovement();
            GameInput.Instance.DisableMovement();

            OnPlayerDeath?.Invoke(this, EventArgs.Empty);

        }
    }
    private IEnumerator DamageRecoveryRuotine() // Таймер для отката получения урона 
    {
        yield return new WaitForSeconds(_damageRecoveryTime); // Действие выполнится через указанное время
        _canTakeDamage = true;
    }
    public bool IsRunning()
    {
        return _isRunning;
    }
    public Vector3 GetPlayerScreenPosition()
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }
    private void GameInput_OnPlayerAttack(object sender, System.EventArgs e)
    {
        ActiveWeapon.Instance.GetActiveWeapon().Attack();
    }
    private void HandleMovement()
    {
        //Vector2 inputVector = GameInput.Instance.GetMovementVector();
        //inputVector = inputVector.normalized;                             // Возможно не нужно ибо оно и так и так (0,71; 0,71)
        _rb.MovePosition(_rb.position + _inputVector * (Time.fixedDeltaTime * _movingSpeed));
        //Debug.Log(inputVector);
        if (Mathf.Abs(_inputVector.x) > _minMovingSpeed || Mathf.Abs(_inputVector.y) > _minMovingSpeed) // Идёт ли чел
        {
            _isRunning = true;
        }
        else
        {
            _isRunning = false;
        }
    }
}
