//using System.Numerics;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public static ActiveWeapon Instance { get; private set; }

    [SerializeField] private Sword _sword;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (Player.Instance.IsAlive())
        {
            FollowMousePosition();
        }
    }

    public Sword GetActiveWeapon()
    {
        return _sword;
    }

    private void FollowMousePosition()
    {
        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerPos = Player.Instance.GetPlayerScreenPosition();

        if (mousePos.x > playerPos.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
