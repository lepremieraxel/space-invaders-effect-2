using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFonctionnement : MonoBehaviour
{
    [SerializeField] GameObject Menu;   // Référence au canvas "Menu"
    [SerializeField] GameObject Option; // Référence au canvas "Option"

    // Start is called before the first frame update
    void Start()
    {
      
    }

    public void OnOptionButtonClick()
    {
        // Inverse la visibilité des canvases
        Menu.SetActive(false);
        Option.SetActive(true);
        Debug.Log("Bouton Cliqué");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
