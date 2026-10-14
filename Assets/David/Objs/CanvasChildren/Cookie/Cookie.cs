using UnityEngine;
using TMPro;
//using System;

public class CookieScore : MonoBehaviour
{
    private long TotalCookies;
    private TextMeshProUGUI uiText;

    void Start()
    {
        uiText = GetComponent<TextMeshProUGUI>();
    }

    private void UpdateText()
    {
        uiText.text = $"Cookies: {TotalCookies.ToString()}";
    }

    public void ScoreInc()
    {
        TotalCookies += 1;
        UpdateText();

    }

    public void ScoreDec()
    {
        TotalCookies -= 1;
        UpdateText();
    }

    public void ScoreInc(long value)
    {
        TotalCookies += value;
        UpdateText();

    }

    public void ScoreDec(long value)
    {
        TotalCookies -= value;
        UpdateText();
    }

    public long getScore()
    {
        return TotalCookies;
    }


}
