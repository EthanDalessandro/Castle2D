using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class SpotsHUD : MonoBehaviour
{
    public TextMesh mainText;
    PlayerMovements player;

    void Start()
    {
        mainText = GetComponent<TextMesh>();

        player = FindObjectOfType<PlayerMovements>();

        int spotsNumber = player.pathPlayerList.Count;

        mainText.text = spotsNumber.ToString();
    }
}
