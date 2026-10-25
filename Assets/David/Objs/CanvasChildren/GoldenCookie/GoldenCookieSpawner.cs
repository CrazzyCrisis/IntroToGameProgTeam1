using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEditor.U2D.Aseprite;

public class GoldenCookieSpawner : MonoBehaviour
{
    public GameObject cookie; // This is the main cookie, dragged into the hierarchy. 
    public float minSpawnTime = 0.5f;
    public float maxSpawnTime = 10.0f;

    private CookieScore mainCookie; // This is the score component of the cookie
    private Transform canvasTransform; // This is the canvas transform, gets its x and y.
    private RectTransform canvasRect; // This is used to get the width and height of the canvas
    private long totalCookiesSpawned; // Read it.
    private float timeToNextSpawn; // Read it.


    void Start()
    {
        // I was having issues getting the right objects found. thats why these debugging statmentes are here. - DW
        Canvas myCanvas = GetComponentInParent<Canvas>();
        if (myCanvas)
            Debug.Log("Canvas object found: " + myCanvas.name);
        else
            Debug.Log("No Canvas object could be found");

        canvasTransform = myCanvas.transform;
        canvasRect = myCanvas.GetComponent<RectTransform>();

        mainCookie = (CookieScore)FindAnyObjectByType(typeof(CookieScore));
        if (mainCookie)
            Debug.Log("CookieScore object found: " + mainCookie.name);
        else
            Debug.Log("No CookieScore object could be found");

        timeToNextSpawn = Random.Range(0.5f, 2f);

        Debug.Log("canvasRect Width: " + canvasRect.rect.width);
        Debug.Log("canvasRect Height: " + canvasRect.rect.height);
        Debug.Log("canvasRect Pos X: " + canvasTransform.position.x);
        Debug.Log("canvasRect Pos y: " + canvasTransform.position.y);
    }

    void Update()
    {
        timeToNextSpawn -= Time.deltaTime;
        if (timeToNextSpawn < 0.0f)
        {
            timeToNextSpawn = Random.Range(minSpawnTime, maxSpawnTime);
            SpawnCookie();
        }
    }

    public void SpawnCookie()
    {
        ParentCookieToCanvas();
        totalCookiesSpawned += 1;
    }

    // In case you want to spawn A LOT of cookies.
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
        // this SHOULD spawn a golden cookie in the local space of the canvas - DW
        GameObject newCookie = Instantiate(cookie, canvasTransform, false);
        GoldenCookie gCookie = newCookie.GetComponent<GoldenCookie>();

        // Spawns the cookie inside the local space of the camera with some buff
        float spawnBuffer = 40.0f;
        var cookiePos = canvasTransform.position;
        var position = new Vector2(Random.Range(cookiePos.x + spawnBuffer - canvasRect.rect.width / 2, cookiePos.x - spawnBuffer + canvasRect.rect.width / 2),
                                   Random.Range(cookiePos.y + spawnBuffer - canvasRect.rect.height / 2, cookiePos.y - spawnBuffer + canvasRect.rect.height / 2)
                                   );

        newCookie.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        newCookie.GetComponent<RectTransform>().localPosition = position;

        // Subscribes the golden cookie to the ScoreInc signal. Emits the signal when it is destroyed. 
        // Is there a better way to do this? probably.
        gCookie.GoldenCookieClicked.AddListener(ScoreInc);
    }

    private void ScoreInc()
    {
        mainCookie.ScoreInc();
    }


}
