using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FonctionOption : MonoBehaviour
{

    [SerializeField] GameObject Menu;   // R�f�rence au canvas "Menu"
    [SerializeField] GameObject Option; // R�f�rence au canvas "Option"

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnOptionButtonClick()
    {
        // Inverse la visibilit� des canvases
        Menu.SetActive(false);
        Option.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
