using UnityEngine;
using TMPro;

public class SkinDropdown : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    public static int selectedSkin = 0;

    void Start()
    {
        dropdown.onValueChanged.AddListener(ChangeSkin);

        // Save the current dropdown selection
        selectedSkin = dropdown.value;
    }

    void ChangeSkin(int index)
    {
        selectedSkin = index;
    }
}
