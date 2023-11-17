using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RgbSlider : MonoBehaviour
{

    [SerializeField] Slider ColorSlider;
    [SerializeField] Image ColorPicture;
    [SerializeField] Color color;

    // Start is called before the first frame update
    void Start()
    {
        ColorSlider = this.gameObject.GetComponent<Slider>();
    }

    public void RGBSlide()
    {
        var hue = ColorSlider.value;
        ColorPicture.color = Color.HSVToRGB(hue, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
