using UnityEngine;
using TMPro;
//using System;

public class CookieScore : MonoBehaviour
{
    public long TotalCookies;
    public long IncAmount = 1;
    public TextMeshProUGUI uiText;
    public ParticleSystem cookieParticle;

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
        TotalCookies += IncAmount;
        UpdateText();
        if (cookieParticle != null)
            cookieParticle.Emit((int)IncAmount);

    }

    public void ScoreDec()
    {
        TotalCookies -= IncAmount;
        UpdateText();
    }

    public void ScoreInc(long value)
    {
        TotalCookies += IncAmount + value;
        if (cookieParticle != null)
            cookieParticle.Emit((int)(IncAmount + value));
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
