using System.Net;
using UnityEngine;

public class GoldenCookieSpawner : MonoBehaviour
{
    public GameObject cookie;

    private CookieScore mainCookie;
    private Transform canvasTransform;
    private long totalCookiesSpawned;
    private long currentCookiesSpawned;
    private long timeToNextSpawn;


    void Start()
    {
        Canvas myCanvas = GetComponentInParent<Canvas>();
        if (myCanvas)
            Debug.Log("Canvas object found: " + myCanvas.name);
        else
            Debug.Log("No Canvas object could be found");

        canvasTransform = myCanvas.transform;

        mainCookie = (CookieScore)FindAnyObjectByType(typeof(CookieScore));
        if (mainCookie)
            Debug.Log("CookieScore object found: " + mainCookie.name);
        else
            Debug.Log("No CookieScore object could be found");
    }

    public void SpawnCookie()
    {
        ParentCookieToCanvas();
        totalCookiesSpawned += 1;
        print(totalCookiesSpawned);
    }

    public void SpawnCookie(long value)
    {
        for (long i = 0; i < value; i++)
        {
            ParentCookieToCanvas();
        }
        totalCookiesSpawned += value;
        print(totalCookiesSpawned);
    }

    private void ParentCookieToCanvas()
    {
        GameObject newCookie = Instantiate(cookie, canvasTransform);
        GoldenCookie gCookie = newCookie.GetComponent<GoldenCookie>();
        var position = new Vector2(Random.Range(30.0f, 600.0f), Random.Range(-30.0f, 300.0f));

        newCookie.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        newCookie.GetComponent<RectTransform>().position = position;

        gCookie.GoldenCookieClicked.AddListener(ScoreInc);
    }

    private void ScoreInc()
    {
        mainCookie.ScoreInc();
    }


}
