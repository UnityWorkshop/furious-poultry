using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    private int _scenes;
    
    [SerializeField] private GameObject buttonPrefab;

    public void Start()
    {
        _scenes = EditorBuildSettings.scenes.Length;

        for (int i = 1; i < _scenes + 1; i++)
        {
            GameObject button;
            button = Instantiate(buttonPrefab,this.transform);
            TMP_Text buttText = button.transform.GetChild(0).GetComponent<TMP_Text>();
            
            buttText.SetText(i.ToString());
        }
    }
}
