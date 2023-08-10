using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SaveTheSystem
{
    public class UIGameplay : CustomCanvas
    {
        [Header("Data")]
        [SerializeField] private TheSystem _theSystem;

        [Header("Buttons")]
        [SerializeField] private Button _pauseBtn;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _pauseBtnText;

        [Header("Images")]
        [SerializeField] private Image[] _hearts = new Image[3];

        [Header("Sprites")]
        [SerializeField] private Sprite _redHeart;
        [SerializeField] private Sprite _blackHeart;

        private string _scoreString = "";

        private void OnEnable()
        {
            GameManager.OnScoreUp += UpdateScoreUI;
            TheSystem.OnTheSystemTakeDamaged += UpdateHeartsUI;
            LanguageManager.OnLanguageChanged += LoadLanguague;
        }

        private void OnDisable()
        {
            GameManager.OnScoreUp -= UpdateScoreUI;
            TheSystem.OnTheSystemTakeDamaged -= UpdateHeartsUI;
            LanguageManager.OnLanguageChanged -= LoadLanguague;
        }


        private void Start()
        {
            LoadLanguague();
            UpdateScoreUI();
            UpdateHeartsUI();
            

            _pauseBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);
                GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.PAUSE);

                UIGameplayManager.Instance.CloseAll();
                UIGameplayManager.Instance.DisplayPauseMenu(true);
            });

        }

        private void OnDestroy()
        {
            _pauseBtn.onClick.RemoveAllListeners();
        }

        private void UpdateScoreUI()
        {
            _scoreText.text = $"{_scoreString}: {GameManager.Instance.Score}";
        }

        private void UpdateHeartsUI()
        {
            for (int i = 0; i < _hearts.Length; i++)
            {
                if (_theSystem.Health > i)
                {
                    _hearts[i].sprite = _redHeart;
                }
                else
                {
                    _hearts[i].sprite = _blackHeart;
                }
            }
        }

        private void LoadLanguague()
        {
            _scoreString = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "SCORE");
            _pauseBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "PAUSE");
        }
    }
}
