using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ChangeLeftButton : MonoBehaviour
{
    [SerializeField] string lefttoucheSauvegarde;
    [SerializeField] Text Texte;
    [SerializeField] Button LeftButton;
    [SerializeField] private InputActionReference JoyStitckR;
    [SerializeField] private InputActionAsset ActionAsset;
    private bool appuyer = false;

    void Start()
    {
        // Ajoutez un gestionnaire d'�v�nements pour d�tecter les clics sur le bouton
        LeftButton.onClick.AddListener(OnLeftButtonClick);
    }

    private IEnumerator WaitForBooleanTrue()
    {
        appuyer = false;
        Texte.text = "Press your key";
        yield return new WaitForSeconds(0.2f);
        // Tant que le bool�en n'est pas True, attendez
        while (appuyer != true)
        {
            yield return null;
            KeyCode toucheAppuyee = GetToucheAppuyee();
            if (toucheAppuyee != KeyCode.None)
            {
                // Sauvegardez la touche dans les pr�f�rences du joueur
                PlayerPrefs.SetInt(lefttoucheSauvegarde, (int)toucheAppuyee);
                PlayerPrefs.Save();
                if (lefttoucheSauvegarde != null)
                {
                    appuyer = true;
                }

                Debug.Log("Touche gauche sauvegard�e : " + toucheAppuyee);
                Texte.text = toucheAppuyee.ToString();
            }
        }
        
    }

    void OnLeftButtonClick()
    {
        StartCoroutine(WaitForBooleanTrue());
    }


    KeyCode GetToucheAppuyee()
    {
        // Parcourez toutes les touches possibles et retournez la premi�re qui est appuy�e
        foreach (KeyCode touche in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(touche))
            {
                return touche;
            }
        }
        return KeyCode.None;
    }
}
