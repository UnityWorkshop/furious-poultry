using System;
using com.github.UnityWorkshop.furious_poultry.unity.authoring;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace com.github.UnityWorkshop.furious_poultry.unity
{
    public class MenuHandler : MonoBehaviour
    {
        private WarthogAuthoring[] warthogs;
        private int _scenes;

        [SerializeField] public bool menuScene;
    
        [SerializeField] private GameObject buttonPrefab;

        public void Start()
        {
            if (menuScene)
            {
                _scenes = EditorBuildSettings.scenes.Length;

                for (int i = 1; i < _scenes; i++)
                {
                    var i1 = i;
            
                    GameObject buttonObject = Instantiate(buttonPrefab, transform);
                    Button currentButton = buttonObject.GetComponent<Button>();
                    currentButton.onClick.AddListener(() => LevelButtonClicked(i1));
            
                    TMP_Text buttText = buttonObject.transform.GetChild(0).GetComponent<TMP_Text>();
                    buttText.SetText(i.ToString());
                }
            }
            else
            {
                UpdateWarthogAmount();
            }
        }

        public void Update()
        {
            if (!menuScene && warthogs.Length == 1)
                SceneManager.LoadScene(0);
        }

        void LevelButtonClicked(int lvl)
        {
            SceneManager.LoadScene(lvl);
        }

        public void UpdateWarthogAmount()
        {
            warthogs = FindObjectsOfType<WarthogAuthoring>();
            Debug.Log(warthogs.Length + " ");
            foreach (var warthogAuthoring in warthogs)
            {
                Debug.Log(warthogAuthoring, warthogAuthoring);
            }
        }
    }
}
