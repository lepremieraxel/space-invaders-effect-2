using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ChangeRightButton : MonoBehaviour
{
    [SerializeField] string righttoucheSauvegarde;
    [SerializeField] Text Texte;
    [SerializeField] Button RightButton;
    [SerializeField] private InputActionReference JoyStitckR;
    [SerializeField] private InputActionAsset ActionAsset;
    private bool appuyer = false;

    void Start()
    {
        // Ajoutez un gestionnaire d'événements pour détecter les clics sur le bouton
        RightButton.onClick.AddListener(OnRightButtonClick);
    }

    private IEnumerator WaitForBooleanTrue()
    {
        appuyer = false;
        Texte.text = "Press your key";
        yield return new WaitForSeconds(0.2f);
        // Tant que le booléen n'est pas True, attendez
        while (appuyer != true)
        {
            yield return null;
            KeyCode toucheAppuyee = GetToucheAppuyee();
            if (toucheAppuyee != KeyCode.None)
            {
                // Sauvegardez la touche dans les préférences du joueur
                PlayerPrefs.SetInt(righttoucheSauvegarde, (int)toucheAppuyee);
                PlayerPrefs.Save();
                if (righttoucheSauvegarde != null)
                {
                    appuyer = true;
                }

                Debug.Log("Touche droite sauvegardée : " + toucheAppuyee);
                Texte.text = toucheAppuyee.ToString();
            }
        }

    }

    void OnRightButtonClick()
    {
        StartCoroutine(WaitForBooleanTrue());
    }


    KeyCode GetToucheAppuyee()
    {
        // Parcourez toutes les touches possibles et retournez la première qui est appuyée
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
