using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class TileChooser : MonoBehaviour
{
    public Slider slider;

    public Text text;

    public void CoordinateTextWithSliderValue()
    {


        if (transform.name.Contains("Mountain"))
        {
            text.text = "Mountain Tiles: " + slider.value;

        }
        else if (transform.name.Contains("Water"))
        {
            text.text = "Water Tiles: " + slider.value;

        }
        else if (transform.name.Contains("City"))
        {
            text.text = "City Tiles: " + slider.value;

        }
        else if (transform.name.Contains("Woods"))
        {
            text.text = "Wood Tiles: " + slider.value;

        }
        else if (transform.name.Contains("Size"))
        {
            text.text = "Map Size: " + slider.value + " by " + slider.value;
        }




    }


}