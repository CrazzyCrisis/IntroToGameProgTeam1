using UnityEngine;
using TMPro;

public class CookieScore : MonoBehaviour
{
    private long TotalCookies;
    public TextMeshProUGUI uiText;

    public void ScoreUp()
    {
        TotalCookies += 1;
        print(TotalCookies);
        uiText.text = TotalCookies.ToString();
    }
}
