using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ntw.CurvedTextMeshPro;

namespace SaveTheSystem
{
    public class UIMainMenu : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _playBtn;
        [SerializeField] private Button _optionsBtn;

        [Header("Texts")]
        [SerializeField] private GameObject _tapToPlayText01_Eng;
        [SerializeField] private GameObject _tapToPlayText02_Eng;
        [SerializeField] private GameObject _tapToPlayText01_Franc;
        [SerializeField] private GameObject _tapToPlayText02_Franc;



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

            _playBtn.onClick.AddListener(() =>
            {
                Loader.LoadTweening(Loader.Scene.GameplayScene);
                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });

            _optionsBtn.onClick.AddListener(() =>
            {
                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplayOptionsMenu(true);
                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });
        }

        private void OnDestroy()
        {
            _playBtn.onClick.RemoveAllListeners();
            _optionsBtn.onClick.RemoveAllListeners();
        }

      

        private void LoadLanguague()
        {
            if (LanguageManager.Instance.CurrentLanguague == LanguageManager.Languague.English)
            {
                _tapToPlayText01_Eng.SetActive(true);
                _tapToPlayText02_Eng.SetActive(true);

                _tapToPlayText01_Franc.SetActive(false);
                _tapToPlayText02_Franc.SetActive(false);
            }
            else
            {
                _tapToPlayText01_Eng.SetActive(false);
                _tapToPlayText02_Eng.SetActive(false);

                _tapToPlayText01_Franc.SetActive(true);
                _tapToPlayText02_Franc.SetActive(true);
            }
        }
    }
}
