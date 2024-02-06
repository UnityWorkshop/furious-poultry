using System;
using System.Collections;
using System.Collections.Generic;
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

        for (int i = 0; i < _scenes; i++)
        {
            GameObject button;
            button = Instantiate(buttonPrefab,this.transform);
        }
    }
}
