using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SaveTheSystem
{
    public class UIPause : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _homeBtn;
        [SerializeField] private Button _backBtn;


        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _pauseText;
        [SerializeField] private TextMeshProUGUI _homeBtnText;
        [SerializeField] private TextMeshProUGUI _backBtnText;

        private void OnEnable()
        {
            LanguageManager.OnLanguageChanged += LoadLanguague;
        }

        private void OnDisable()
        {
            LanguageManager.OnLanguageChanged -= LoadLanguague;
        }


        private void Start()
        {
            LoadLanguague();

            _homeBtn.onClick.AddListener(() =>
            {             
                GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.EXIT);              
                Loader.LoadTweening(Loader.Scene.MenuScene);
                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });


            _backBtn.onClick.AddListener(() =>
            {               
                GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.PLAYING);
                UIGameplayManager.Instance.CloseAll();
                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });

        }

        private void OnDestroy()
        {
            _homeBtn.onClick.RemoveAllListeners();
            _backBtn.onClick.RemoveAllListeners();
        }

        private void LoadLanguague()
        {
            _pauseText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "PAUSE");
            _homeBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "HOME");
            _backBtnText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "BACK");
        }
    }
}
