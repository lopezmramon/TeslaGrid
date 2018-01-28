using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class DesignRandomLevelView : MonoBehaviour
{
    public InputField cashAmount;
    public Slider waterTilesSlider, woodTilesSlider, mountainTilesSlider, cityTilesSlider, tileSizeSlider;
    public void PlayRandomLevel()
    {
        if (cashAmount.text == "")
        {
            cashAmount.text = "400";
        }
        if (int.Parse(cashAmount.text) < 400)
        {
            cashAmount.text = "400";
        }
        RandomLevelRequest r = new RandomLevelRequest
            (
            new Vector2(tileSizeSlider.value, tileSizeSlider.value),
           (int)woodTilesSlider.value,
           (int)cityTilesSlider.value,
           (int)waterTilesSlider.value,
           (int)mountainTilesSlider.value,
           int.Parse(cashAmount.text)

            );
        DispatchRandomLevelRequest(r);
    }
    void DispatchRandomLevelRequest(RandomLevelRequest r)
    {
        CodeControl.Message.Send<RandomLevelRequest>(r);      

    }
}