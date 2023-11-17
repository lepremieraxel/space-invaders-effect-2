using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScript : MonoBehaviour
{
    [SerializeField] string sceneName;
    public void OnButtonClick()
    {
        SceneManager.LoadScene(2);
    }
}
