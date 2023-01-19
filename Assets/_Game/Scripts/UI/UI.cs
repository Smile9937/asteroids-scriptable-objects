using System;
using TMPro;
using UnityEngine;
using Variables;

namespace UI
{
    public class UI : MonoBehaviour
    {
        [Header("Health:")]
        [SerializeField] private IntVariable _healthVar;
        [SerializeField] private TextMeshProUGUI _healthText;
        
        [Header("Score:")]
        [SerializeField] private TextMeshProUGUI _scoreText;
        
        [Header("Timer:")]
        [SerializeField] private TextMeshProUGUI _timerText;
        
        [Header("Laser:")]
        [SerializeField] private IntVariable _laserVar;
        [SerializeField] private TextMeshProUGUI _laserText;

        private void OnEnable()
        {
            EventManager.onUpdateHealthUI += OnHealthChanged;
        }

        private void OnDisable()
        {
            EventManager.onUpdateHealthUI -= OnHealthChanged;
        }

        private void Start()
        {
            SetHealthText($"Health: {_healthVar.Value}");
            SetLaserText($"Laser Shot: {_laserVar.Value}");
        }
        
        public void OnHealthChanged(int newValue)
        {
            SetHealthText($"Health: {newValue}");
        }

        private void SetHealthText(string text)
        {
            _healthText.text = text;
        }
        
        private void SetScoreText(string text)
        {
            _scoreText.text = text;
        }
        
        private void SetTimerText(string text)
        {
            _timerText.text = text;
        }
        
        private void SetLaserText(string text)
        {
            _laserText.text = text;
        }
    }
}
