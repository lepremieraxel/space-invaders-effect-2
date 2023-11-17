using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class musicslider : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 10);
            Load();
        }

        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        music.volume = musicSlider.value;
        Save();
    }

    private void Load()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
