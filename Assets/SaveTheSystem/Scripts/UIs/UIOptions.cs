using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SaveTheSystem
{
    public class UIOptions : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _leftLanguageBtn;
        [SerializeField] private Button _rightLanguageBtn;
        [SerializeField] private Button _backBtn;


        [Header("Sliders")]
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _musicSlider;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _optionText;
        [SerializeField] private TextMeshProUGUI _soundText;
        [SerializeField] private TextMeshProUGUI _musicText;
        [SerializeField] private TextMeshProUGUI _headingLanguageText;
        [SerializeField] private TextMeshProUGUI _optionLanguageText;



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
            UpdateLanguageText();
            _soundSlider.value = SoundManager.Instance.SFXVolume;
            _musicSlider.value = SoundManager.Instance.BackgroundVolume;



            _leftLanguageBtn.onClick.AddListener(() =>
            {
                ToggleLanguage();
                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });

            _rightLanguageBtn.onClick.AddListener(() =>
            {
                ToggleLanguage();
                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });

            _backBtn.onClick.AddListener(() =>
            {
                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplayMainMenu(true);
                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });

            _soundSlider.onValueChanged.AddListener(OnSoundSliderChanged);
            _musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        }

        private void OnDestroy()
        {
            _leftLanguageBtn.onClick.RemoveAllListeners();
            _rightLanguageBtn.onClick.RemoveAllListeners();
            _backBtn.onClick.RemoveAllListeners();

            _soundSlider.onValueChanged.RemoveAllListeners();
            _musicSlider.onValueChanged.RemoveAllListeners();
        }

        private void OnSoundSliderChanged(float value)
        {
            SoundManager.Instance.SFXVolume = value;
        }

        private void OnMusicSliderChanged(float value)
        {
            SoundManager.Instance.BackgroundVolume = value;
            SoundManager.Instance.UpdateBackgroundVolume();
        }

        private void UpdateLanguageText()
        {
            if (LanguageManager.Instance.CurrentLanguague == LanguageManager.Languague.English)
            {
                _optionLanguageText.text = "ENGLISH";
            }
            else
            {
                _optionLanguageText.text = "FRANÇAIS";
            }
        }

        private void ToggleLanguage()
        {
            LanguageManager.Instance.ChangeLanguague(
                 LanguageManager.Instance.CurrentLanguague == LanguageManager.Languague.English
                ? LanguageManager.Languague.France
                : LanguageManager.Languague.English);

            UpdateLanguageText();
        }

        private void LoadLanguague()
        {
            _optionText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "OPTIONS");
            _soundText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "SOUND");
            _musicText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "MUSIC");
            _headingLanguageText.text = LanguageManager.Instance.GetWord(LanguageManager.Instance.CurrentLanguague, "LANGUAGE");  
        }
    }
}
