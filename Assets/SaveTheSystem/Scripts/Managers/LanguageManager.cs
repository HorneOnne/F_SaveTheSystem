using UnityEngine;
using System.Collections.Generic;

namespace SaveTheSystem
{

    public class LanguageManager : MonoBehaviour
    {
        public static LanguageManager Instance { get; private set; }
        public static System.Action OnLanguageChanged;


        private Dictionary<string, string> dict = new Dictionary<string, string>()
        {
            {"PLAY", "JOUER"},
            {"RECORD", "ENREGISTRER" },
            {"SOUND","SON"},
            {"MUSIC","musique"},
            {"BACK", "DOS"},
            {"LANGUAGE","LANGUE"},
            {"PAUSE", "PAUSE"},
            {"CONTINUE", "CONTINUER"},
            {"MENU", "MENU"},
            {"GAME OVER","JEU TERMINÉ"},
            {"GAME\nOVER","JEU\nTERMINÉ"},
            {"SCORE","SCORE"},
            {"REPLAY","REJOUER"},
            {"HOME","MENU"},
            {"OPTIONS","CHOIX"},
            {"TAP TO START","APPUYEZ POUR JOUER"},
        };


        public enum Languague
        {
            English,
            France
        }

        public Languague CurrentLanguague;


        private void Awake()
        {
            // Check if an instance already exists, and destroy the duplicate
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            // Make the GameObject persist across scenes
            DontDestroyOnLoad(this.gameObject);
        }


        public void ChangeLanguague(Languague languague)
        {
            this.CurrentLanguague = languague;
            OnLanguageChanged?.Invoke();
        }

        public string GetWord(Languague type, string word)
        {
            switch (type)
            {
                default: break;
                case Languague.English:
                    if (dict.ContainsKey(word))
                    {
                        return word;
                    }
                    break;
                case Languague.France:
                    if (dict.ContainsKey(word))
                    {
                        return dict[word];
                    }
                    break;
            }
            return "";
        }
    }
}
