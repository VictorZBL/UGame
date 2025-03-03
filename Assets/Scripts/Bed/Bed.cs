using UnityEngine;
using UnityEngine.UI;

public class Bed : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _hintSprite;
    [SerializeField] private int _heallingValue = 100;

    private bool _isPlayerNear = false;

    private void Start()
    {
        GameInput.Instance.OnPlayerInteract += Bed_OnPlayerInteract;
    }

    private void Bed_OnPlayerInteract(object sender, System.EventArgs e)
    {
        if (_isPlayerNear)
        {
            Player.Instance.AddHealth(_heallingValue);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision);
        if (collision.transform.TryGetComponent(out Player player))
        {
            _isPlayerNear = true;
            _hintSprite.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            _isPlayerNear = false;
            _hintSprite.enabled = false;
        }
    }
}
