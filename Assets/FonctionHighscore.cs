using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FonctionHighscore : MonoBehaviour
{

    [SerializeField] GameObject Menu;   // R�f�rence au canvas "Menu"
    [SerializeField] GameObject Highscore; // R�f�rence au canvas "Highscore"

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnHighscoreButtonClick()
    {
        // Inverse la visibilit� des canvases
        Menu.SetActive(false);
        Highscore.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
