using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FonctionHighscore : MonoBehaviour
{

    [SerializeField] GameObject Menu;   // Référence au canvas "Menu"
    [SerializeField] GameObject Highscore; // Référence au canvas "Highscore"

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnHighscoreButtonClick()
    {
        // Inverse la visibilité des canvases
        Menu.SetActive(false);
        Highscore.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
