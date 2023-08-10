using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace SaveTheSystem
{
    public class UIGameover : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _homeBtn;
        [SerializeField] private Button _replayBtn;
        

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _gameoverText;
        [SerializeField] private TextMeshProUGUI _replayBtnText;
        [SerializeField] private TextMeshProUGUI _homeBtnText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _recordText;

        // Cached
        private GameManager _gameManager;
        private string _scoreString = "";
        private string _recordString = "";

        private void OnEnable()
        {
            LanguageManager.OnLanguageChanged += LoadLanguague;
            GameplayManager.OnGameOver += LoadScore;
            GameplayManager.OnGameOver += LoadBest;
        }

        private void OnDisable()
        {
            LanguageManager.OnLanguageChanged -= LoadLanguague;
            GameplayManager.OnGameOver -= LoadScore;
            GameplayManager.OnGameOver -= LoadBest;
        }


        private void Start()
        {
            _gameManager = GameManager.Instance;
            LoadScore();
            LoadBest();

            LoadLanguague();

            _replayBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);

                Loader.LoadTweening(Loader.Scene.GameplayScene);
            });

            _homeBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);

                GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.EXIT);
                Loader.LoadTweening(Loader.Scene.MenuScene);            
            });
        }

        private void OnDestroy()
        {
            _replayBtn.onClick.RemoveAllListeners();
            _homeBtn.onClick.RemoveAllListeners();
        }

        private void LoadScore()
        {
            _scoreText.text = $"{_scoreString}: {_gameManager.Score}";
        }

        private void LoadBest()
        {
            _gameManager.SetBestScore(_gameManager.Score);
            _recordText.text = $"{_recordString}: {_gameManager.BestScore}";
        }

        private void LoadLanguague()
        {

            if (LanguageManager.Instance.CurrentLanguague == LanguageManager.Languague.English)
            {
                _gameoverText.fontSize = 80;
                _replayBtnText.fontSize = 50;
                _homeBtnText.fontSize = 60;
            }
            else
            {
                _gameoverText.fontSize = 70;
                _replayBtnText.fontSize = 40;
                _homeBtnText.fontSize = 50;
            }

            _gameoverText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "GAME\nOVER");    
            _scoreString = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "SCORE");
            _recordString = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "RECORD");
            _replayBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "REPLAY");
            _homeBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "HOME");
        }
    }
}
