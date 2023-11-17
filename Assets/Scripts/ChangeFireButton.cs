using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ChangeFireButton : MonoBehaviour
{
    [SerializeField] string firetoucheSauvegarde;
    [SerializeField] Text Texte;
    [SerializeField] Button FireButton;
    [SerializeField] private InputActionReference JoyStitckR;
    [SerializeField] private InputActionAsset ActionAsset;
    private bool appuyer = false;

    void Start()
    {
        // Ajoutez un gestionnaire d'événements pour détecter les clics sur le bouton
        FireButton.onClick.AddListener(OnFireButtonClick);
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
                PlayerPrefs.SetInt(firetoucheSauvegarde, (int)toucheAppuyee);
                PlayerPrefs.Save();
                if (firetoucheSauvegarde != null)
                {
                    appuyer = true;
                }

                Debug.Log("Touche tiré sauvegardée : " + toucheAppuyee);
                Texte.text = toucheAppuyee.ToString();
            }
        }

    }

    void OnFireButtonClick()
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
