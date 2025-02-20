using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class KnockBack : MonoBehaviour
{
    [SerializeField] private float _knockBackForce = 2f;
    [SerializeField] private float _knockBackMovingTimerMax = 0.3f;

    private float _knockBackMovingTimer;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        _knockBackMovingTimer -= Time.deltaTime;
        if( _knockBackMovingTimer < 0)
        {
            StopKnockBackMovement();
        }
    }
    private void StopKnockBackMovement()
    {
        _rb.linearVelocity = Vector2.zero;
    }


}
