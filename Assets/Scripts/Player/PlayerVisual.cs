using UnityEngine;

// ПРЕДУПРЕЖДАЮ: этот кал работает некоректно и при смене направления движения спрайт меняется на статик.

public class PlayerVisual : MonoBehaviour
{
    private Animator _animator;

    private SpriteRenderer _spriteRenderer;

    private const string IS_RUNNING = "IsRunning";
    private const string IS_DIE = "IsDie";
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        Player.Instance.OnPlayerDeath += Player_OnPlayerDeath;
    }

    private void Player_OnPlayerDeath(object sender, System.EventArgs e)
    {
        _animator.SetBool(IS_DIE, true);
    }

    private void Update()
    {
        _animator.SetBool(IS_RUNNING, Player.Instance.IsRunning());
        if (Player.Instance.IsAlive())
        {
            AdjustPlayerFacingDirection();
        }
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerPos = Player.Instance.GetPlayerScreenPosition();

        if (mousePos.x > playerPos.x)
        {
            _spriteRenderer.flipX = false;
        }
        else
        {
            _spriteRenderer.flipX = true;
        }
    }
}
