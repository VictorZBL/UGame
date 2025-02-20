using UnityEngine;

// ПРЕДУПРЕЖДАЮ: этот кал работает некоректно и при смене направления движения спрайт меняется на статик.

public class PlayerVisual : MonoBehaviour
{
    private Animator _animator;

    private SpriteRenderer _spriteRenderer;

    private const string IS_RUNNING = "IsRunning";
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        _animator.SetBool(IS_RUNNING, Player.Instance.IsRunning());
        AdjustPlayerFacingDirection();
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
