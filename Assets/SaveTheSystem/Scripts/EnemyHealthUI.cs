using UnityEngine;
using TMPro;

namespace SaveTheSystem
{
    public class EnemyHealthUI : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private TextMeshPro _uiHealthText;


        private void Start()
        {
            UpdateHealthUI();

            _enemy.OnEnemyTakeDamaged += UpdateHealthUI;
        }

        private void UpdateHealthUI()
        {
            _uiHealthText.text = _enemy.Health.ToString();
        }

        private void OnDestroy()
        {
            _enemy.OnEnemyTakeDamaged -= UpdateHealthUI;
        }
    }
}
