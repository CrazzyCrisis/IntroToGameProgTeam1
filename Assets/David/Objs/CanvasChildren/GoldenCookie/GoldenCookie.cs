using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GoldenCookie : MonoBehaviour
{
    private Button newCookie;
    public UnityEvent GoldenCookieClicked;
    void Awake()
    {
        newCookie = GetComponent<Button>();
        newCookie.onClick.AddListener(DestroySelf);
    }

    void DestroySelf()
    {
        GoldenCookieClicked.Invoke();
        newCookie.onClick.RemoveListener(DestroySelf);
        Destroy(gameObject);
    }
}
