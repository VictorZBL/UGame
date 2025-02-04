using UnityEngine;

// ПРЕДУПРЕЖДАЮ: этот кал работает некоректно и при смене направления движения спрайт меняется на статик.

public class PlayerVisual : MonoBehaviour
{
    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private const string IS_RUNNING = "IsRunning";
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        animator.SetBool(IS_RUNNING, Player.Instance.IsRunning());
        AdjustPlayerFacingDirection();
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerPos = Player.Instance.GetPlayerScreenPosition();

        if (mousePos.x < playerPos.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
}
