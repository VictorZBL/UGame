using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _time = 60f;
    [SerializeField] private Text _timerText;
    [SerializeField] private int _timerDamage = 100;

    private float _timeLeft = 0f;

    private bool _isPlayerAlive;


    private void Start()
    {
        _timeLeft = _time;
        _isPlayerAlive = true;
        StartCoroutine(StartTimer());
        Player.Instance.OnPlayerDeath += Timer_OnPlayerDeath;
    }

    private void Timer_OnPlayerDeath(object sender, System.EventArgs e)
    {
        _isPlayerAlive = false;
    }

    private IEnumerator StartTimer()
    {
        while (_timeLeft > 0 && _isPlayerAlive)
        {
            _timeLeft -= Time.deltaTime;
            UpdateTeimeText();
            yield return null;
        }
        TimerDeath();
    }
    /*
    *   В этом методе используется Mathf.FloorToInt вместо (int) т.к. 
    *   он не отбрасывает дробную часть, 
    *   а округляет число по правилам математики
    */
    private void UpdateTeimeText()
    {
        if (_timeLeft < 0) _timeLeft = 0; // Защита от уходы таймера в минус

        float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        _timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
    private void TimerDeath()
    {
        Player.Instance.TakeDamage(null, _timerDamage);
    }


}
