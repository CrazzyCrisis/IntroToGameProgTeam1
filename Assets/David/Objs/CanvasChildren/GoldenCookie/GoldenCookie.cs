using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GoldenCookie : MonoBehaviour
{
    private Button newCookie;
    public UnityEvent GoldenCookieClicked;
    private float timeUntilSudoKu;
    void Awake()
    {
        newCookie = GetComponent<Button>();
        newCookie.onClick.AddListener(DestroySelf);
        timeUntilSudoKu = Random.Range(3.0f, 10.0f);
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
