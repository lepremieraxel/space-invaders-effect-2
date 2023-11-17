using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class soundslider : MonoBehaviour
{
    [SerializeField] Slider soundSlider;
    [SerializeField] AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        sound.mute = true;

        // Ajouter une écoute pour les changements de la valeur du Slider
        soundSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void OnSliderValueChanged(float value)
    {
            // Muter ou démuter en fonction de la valeur du Slider
            if (value > 0)
            {
                sound.mute = false;
            }
            else
            {
                sound.mute = true;
            }

            // Sauvegarder la nouvelle valeur du volume
            Save();
    }

    public void Load()
    {
       // Charger les valeurs du volume depuis PlayerPrefs et les appliquer aux audio sources
       float savedVolume = PlayerPrefs.GetFloat("soundVolume");
       soundSlider.value = savedVolume;
       sound.volume = savedVolume;
    }

    void Save()
    {
       // Sauvegarder la valeur actuelle du volume dans PlayerPrefs
       PlayerPrefs.SetFloat("soundVolume", soundSlider.value);
       PlayerPrefs.Save();
    }
}
