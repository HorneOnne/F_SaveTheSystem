using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SaveTheSystem
{
    public class TheSystemHealthUI : MonoBehaviour
    {
        [Header("Data")]
        private TheSystem _theSystem;

        [Header("Gameobjects")]
        [SerializeField] private GameObject _defend_1;
        [SerializeField] private GameObject _defend_2;
        [SerializeField] private GameObject _core;


        private void Awake()
        {
            _theSystem = GetComponent<TheSystem>();
        }
        private void Start()
        {
            UpdateHealthUI();
            TheSystem.OnTheSystemTakeDamaged += UpdateHealthUI;
        }

        private void UpdateHealthUI()
        {
            if(_theSystem == null) return;
            switch (_theSystem.Health)
            {
                default: break;
                case 0:
                    _defend_1.SetActive(false);
                    _defend_2.SetActive(false);
                    _core.SetActive(false);
                    break;
                case 1:
                    _defend_1.SetActive(false);
                    _defend_2.SetActive(false);
                    _core.SetActive(true);
                    break;
                case 2:
                    _defend_1.SetActive(true);
                    _defend_2.SetActive(false);
                    _core.SetActive(true);
                    break;
                case 3:
                    _defend_1.SetActive(true);
                    _defend_2.SetActive(true);
                    _core.SetActive(true);
                    break;
            }
        }
    }
}
