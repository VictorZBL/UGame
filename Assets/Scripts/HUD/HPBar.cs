using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    private Image Bar;

    private void Awake()
    {
        Bar = GetComponent<Image>();
    }
    public void SetHPBarValue(int health)
    {
        //Debug.Log(health / 10f);
        Bar.fillAmount = health / 10f;
    }
}
