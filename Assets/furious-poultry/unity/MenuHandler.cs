using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace com.github.UnityWorkshop.furious_poultry.unity
{
    public class MenuHandler : MonoBehaviour
    {
        private int _scenes;

        //private Button _currentButton;
    
        [SerializeField] private GameObject buttonPrefab;

        public void Start()
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

        void LevelButtonClicked(int lvl)
        {
            SceneManager.LoadScene(lvl);
        }
    }
}
