using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public void SwitchScene()
    {
        SceneManager.LoadScene (sceneBuildIndex:1);
    }
}
