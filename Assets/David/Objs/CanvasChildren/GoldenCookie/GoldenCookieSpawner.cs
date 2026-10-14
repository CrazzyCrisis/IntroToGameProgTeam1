using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class GoldenCookieSpawner : MonoBehaviour
{
    public GameObject cookie;

    private CookieScore mainCookie;
    private Transform canvasTransform;
    private long totalCookiesSpawned;
    private float timeToNextSpawn;


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

        timeToNextSpawn = Random.Range(0.5f, 2f);
    }

    void Update()
    {
        timeToNextSpawn -= Time.deltaTime;
        if (timeToNextSpawn < 0.0f)
        {
            timeToNextSpawn = Random.Range(0.5f, 2f);
            SpawnCookie();
        }
    }

    public void SpawnCookie()
    {
        ParentCookieToCanvas();
        totalCookiesSpawned += 1;
    }

    public void SpawnCookie(long value)
    {
        for (long i = 0; i < value; i++)
        {
            ParentCookieToCanvas();
        }
        totalCookiesSpawned += value;
    }

    private void ParentCookieToCanvas()
    {
        GameObject newCookie = Instantiate(cookie, canvasTransform);
        GoldenCookie gCookie = newCookie.GetComponent<GoldenCookie>();
        var position = new Vector2(Random.Range(30.0f, Screen.width - 30f), Random.Range(30.0f, Screen.height - 30f));

        newCookie.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        newCookie.GetComponent<RectTransform>().position = position;

        gCookie.GoldenCookieClicked.AddListener(ScoreInc);
    }

    private void ScoreInc()
    {
        mainCookie.ScoreInc();
    }


}
