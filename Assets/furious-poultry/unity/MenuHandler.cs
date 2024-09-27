using com.github.UnityWorkshop.furious_poultry.unity.authoring;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace com.github.UnityWorkshop.furious_poultry.unity
{
    public class MenuHandler : MonoBehaviour
    {
        private WarthogAuthoring[] warthogs;
        const int Scenes = 3;

        [SerializeField] public bool menuScene = true;
    
        [SerializeField] private GameObject buttonPrefab;

        public void Start()
        {
            if (menuScene)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                for (int i = 1; i < Scenes; i++)
                {
                    var i1 = i;
            
                    GameObject buttonObject = Instantiate(buttonPrefab, transform);
                    Button currentButton = buttonObject.GetComponent<Button>();
                    currentButton.onClick.AddListener(() => LevelButtonClicked(i1));
            
                    TMP_Text buttText = buttonObject.transform.GetChild(0).GetComponent<TMP_Text>();
                    buttText.SetText(i.ToString());
                }
            }
        }

        public void Update()
        {
            UpdateWarthogAmount();
            if (!menuScene && warthogs.Length == 0)
                SceneManager.LoadScene(0);
        }

        void LevelButtonClicked(int lvl)
        {
            SceneManager.LoadScene(lvl);
        }

        public void UpdateWarthogAmount()
        {
            warthogs = FindObjectsOfType<WarthogAuthoring>();
        }
    }
}
