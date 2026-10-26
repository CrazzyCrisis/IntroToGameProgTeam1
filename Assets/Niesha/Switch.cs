using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    public Animator Cookieanimator;
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

    public void PlayCookieAnimation(int index)
    {
        if (index == 0)
        {
            Cookieanimator.Play("BounceAnimation");
        }else
        {
            Cookieanimator.Play("BounceAnimation2");
        }
    }
}