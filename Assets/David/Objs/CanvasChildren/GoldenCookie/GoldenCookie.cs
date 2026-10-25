using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
//using UnityEngine.UIElements;

public class GoldenCookie : MonoBehaviour
{
    private Button newCookie;
    public UnityEvent GoldenCookieClicked;

    public float minLifetime = 3.0f;
    public float maxLifetime = 10.0f;
    private float timeUntilSudoKu;
    void Awake()
    {
        newCookie = GetComponent<Button>();
        newCookie.onClick.AddListener(DestroySelf);
        timeUntilSudoKu = Random.Range(minLifetime, maxLifetime);
    }

    void Update()
    {
        timeUntilSudoKu -= Time.deltaTime;
        if (timeUntilSudoKu <= 0.0f)
        {
            CommitSudoku();
        }
    }

    void DestroySelf()
    {
        GoldenCookieClicked.Invoke();
        newCookie.onClick.RemoveListener(DestroySelf);
        Destroy(gameObject);
    }

    void CommitSudoku()
    {
        newCookie.onClick.RemoveListener(DestroySelf);
        Destroy(gameObject);
    }
}
