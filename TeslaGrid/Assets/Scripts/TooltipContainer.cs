using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TooltipContainer : MonoBehaviour
{
    public Text currentSignalText, typeText, locationText, objectiveText;
    public Image typeImage;
    public Sprite[] typeSprites;
    private void Awake()
    {
        CodeControl.Message.AddListener<ActivateTooltipRequest>(OnActivateTooltipRequest);
    }

    private void OnActivateTooltipRequest(ActivateTooltipRequest obj)
    {

        currentSignalText.text = string.Format("Current Signal: {0}", obj.tile.GetSignal().ToString());
        typeText.text = obj.tile.type.ToString();

        switch (obj.tile.type)
        {
            case TileType.Plain:
                typeImage.sprite = typeSprites[0];
                break;
            case TileType.City:
                typeImage.sprite = typeSprites[1];
                break;
            case TileType.Water:
                typeImage.sprite = typeSprites[2];
                break;
            case TileType.Mountain:
                typeImage.sprite = typeSprites[3];
                break;
            case TileType.Woods:
                typeImage.sprite = typeSprites[4];
                break;
        }

        locationText.text = string.Format("Tile {0}, {1}", obj.tile.x, obj.tile.y);
        objectiveText.gameObject.SetActive(obj.tile.isObjective);
    }
}