using UnityEngine;
using TMPro;
//using System;

public class CookieScore : MonoBehaviour
{
    public long TotalCookies;
    public long IncAmount;
    public TextMeshProUGUI uiText;

    void Start()
    {
        uiText = GetComponent<TextMeshProUGUI>();
        IncAmount = 1;
    }

    private void UpdateText()
    {
        uiText.text = $"Cookies: {TotalCookies.ToString()}";
    }

    public void ScoreInc()
    {
        TotalCookies += IncAmount;
        UpdateText();

    }

    public void ScoreDec()
    {
        TotalCookies -= IncAmount;
        UpdateText();
    }

    public void ScoreInc(long value)
    {
        TotalCookies += IncAmount + value;
        UpdateText();

    }

    public void ScoreDec(long value)
    {
        TotalCookies -= IncAmount + value;
        UpdateText();
    }

    public long getScore()
    {
        return TotalCookies;
    }


}
