using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FonctionMenuOption : MonoBehaviour
{
    [SerializeField] GameObject Menu;   // R�f�rence au canvas "Menu"
    [SerializeField] GameObject Option; // R�f�rence au canvas "Option"

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnMenuButtonClick()
    {
        // Inverse la visibilit� des canvases
        Menu.SetActive(true);
        Option.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
