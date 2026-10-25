using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    public Image targetImage;
    public Sprite[] skins;

    void Start()
    {
        ChangeImage(SkinDropdown.selectedSkin);
    }

    public void ChangeImage(int index)
    {
        if (index >= 0 && index < skins.Length)
        {
            targetImage.sprite = skins[index];
        }
    }
}