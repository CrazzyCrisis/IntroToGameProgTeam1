
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public bool playerTurn = true;
    public bool inPlay = false;
    public bool isThinking = false;

    public List<GameObject> spaces;

    [Header("Game Panels")]
    public GameObject startPanel;
    public GameObject betPanel;
    public GameObject gameBoard;
    public GameObject cookieGamePanel;

    [Header("Result Popups")]
    public GameObject winPopUp;
    public GameObject drawPopUp;
    public GameObject losePopUp;

    [Header("Betting System")]
    public CookieScore mainCookie;
    public TMP_InputField betInput;
    public TextMeshProUGUI cookieBalanceText;
    public TextMeshProUGUI betMessageText;

    private long currentBet = 0;
    private bool hasActiveBet = false;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip happyClickSound;
    public AudioClip melancholyClickSound;
    public AudioClip spaceSelectSound;

    private List<int[]> winningCombinations = new List<int[]>
    {
        new int[] { 0, 1, 2 },
        new int[] { 3, 4, 5 },
        new int[] { 6, 7, 8 },

        new int[] { 0, 3, 6 },
        new int[] { 1, 4, 7 },
        new int[] { 2, 5, 8 },

        new int[] { 0, 4, 8 },
        new int[] { 2, 4, 6 }
    };

    // INITIALIZE GAME
    private void Start()
    {
        // Show only the start screen.
        startPanel.SetActive(true);

        // Hide all other panels.
        betPanel.SetActive(false);
        gameBoard.SetActive(false);

        winPopUp.SetActive(false);
        drawPopUp.SetActive(false);
        losePopUp.SetActive(false);

        inPlay = false;
        playerTurn = true;
        isThinking = false;

        currentBet = 0;
        hasActiveBet = false;

        betInput.text = "";
        betMessageText.text = "Place your bet to play!";

        UpdateCookieDisplay();
    }

    // START BUTTON
    public void StartGame()
    {
        PlayHappySound();

        startPanel.SetActive(false);
        betPanel.SetActive(true);
        gameBoard.SetActive(false);

        betInput.text = "";
        betMessageText.text = "Place your bet to play!";
    }

    // PLAYER PLACES A BET
    public void PlaceBet()
    {
        if (hasActiveBet || inPlay)
            return;

        if (mainCookie == null)
        {
            betMessageText.text = "Cookie system not connected!";
            return;
        }

        long betAmount;

        if (!long.TryParse(betInput.text, out betAmount) || betAmount <= 0)
        {
            betMessageText.text = "Enter a valid bet!";
            return;
        }

        if (betAmount > mainCookie.TotalCookies)
        {
            betMessageText.text = "Not enough cookies!";
            return;
        }

        // Deduct the wager.
        mainCookie.TotalCookies -= betAmount;

        currentBet = betAmount;
        hasActiveBet = true;

        playerTurn = true;
        inPlay = true;
        isThinking = false;

        ClearBoard();

        betPanel.SetActive(false);
        gameBoard.SetActive(true);

        betMessageText.text = "";

        UpdateCookieDisplay();
    }

    // UPDATE COOKIE DISPLAY
    private void UpdateCookieDisplay()
    {
        if (mainCookie == null)
            return;

        mainCookie.RefreshCookieDisplay();

        if (cookieBalanceText != null)
        {
            cookieBalanceText.text =
                "Cookies: " + mainCookie.TotalCookies.ToString();
        }
    }

    // SETTLE BET
    private void SettleBet(string result)
    {
        if (!hasActiveBet || mainCookie == null)
            return;

        if (result == "win")
        {
            // Pay twice the original wager.
            mainCookie.TotalCookies += currentBet * 2;
        }
        else if (result == "draw")
        {
            // Refund the original wager.
            mainCookie.TotalCookies += currentBet;
        }

        // On a loss, the wager remains deducted.

        hasActiveBet = false;
        currentBet = 0;

        UpdateCookieDisplay();
    }

    // CHECK FOR WIN
    public void CheckForWin()
    {
        if (!inPlay)
            return;

        foreach (int[] combination in winningCombinations)
        {
            SpaceType firstSpace = spaces[combination[0]]
                .GetComponent<SpriteChanger>().spaceType;

            if (firstSpace != SpaceType.empty &&
                firstSpace == spaces[combination[1]]
                    .GetComponent<SpriteChanger>().spaceType &&
                firstSpace == spaces[combination[2]]
                    .GetComponent<SpriteChanger>().spaceType)
            {
                Debug.LogWarning(firstSpace + " wins!");

                inPlay = false;
                isThinking = false;

                if (firstSpace == SpaceType.player)
                {
                    SettleBet("win");
                    winPopUp.SetActive(true);
                }
                else
                {
                    SettleBet("lose");
                    losePopUp.SetActive(true);
                }

                return;
            }
        }
    }

    // CHECK FOR DRAW
    public void CheckForDraw()
    {
        if (!inPlay)
            return;

        foreach (GameObject space in spaces)
        {
            if (space.GetComponent<SpriteChanger>().spaceType
                == SpaceType.empty)
            {
                return;
            }
        }

        Debug.LogWarning("Draw!");

        inPlay = false;
        isThinking = false;

        SettleBet("draw");
        drawPopUp.SetActive(true);
    }

    // CLEAR BOARD
    private void ClearBoard()
    {
        foreach (GameObject space in spaces)
        {
            space.GetComponent<SpriteRenderer>().sprite = null;
            space.GetComponent<SpriteChanger>().spaceType = SpaceType.empty;
        }
    }

    // RETURN TO BETTING SCREEN
    public void RestartGame()
    {
        if (inPlay)
            return;

        PlayHappySound();

        ClearBoard();

        playerTurn = true;
        inPlay = false;
        isThinking = false;

        winPopUp.SetActive(false);
        drawPopUp.SetActive(false);
        losePopUp.SetActive(false);

        currentBet = 0;
        hasActiveBet = false;

        betInput.text = "";
        betMessageText.text = "Place a new bet to play!";

        gameBoard.SetActive(false);
        betPanel.SetActive(true);

        UpdateCookieDisplay();
    }

    // RETURN TO START SCREEN
    public void ReturnToStartScreen()
    {
        if (inPlay)
            return;

        startPanel.SetActive(true);
        betPanel.SetActive(false);
        gameBoard.SetActive(false);

        winPopUp.SetActive(false);
        drawPopUp.SetActive(false);
        losePopUp.SetActive(false);
    }

    // MAIN MENU
    public void GoToMainMenu()
    {
        StartCoroutine(MainMenuDelay());
    }

    private IEnumerator MainMenuDelay()
    {
        PlayMelancholySound();

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(0);
    }

    // COMPUTER TURN
    public IEnumerator ComputerTurn()
    {
        isThinking = true;

        yield return new WaitForSeconds(1f);

        if (!inPlay)
        {
            isThinking = false;
            yield break;
        }

        List<GameObject> emptySpaces = new List<GameObject>();

        foreach (GameObject space in spaces)
        {
            if (space.GetComponent<SpriteChanger>().spaceType
                == SpaceType.empty)
            {
                emptySpaces.Add(space);
            }
        }

        if (emptySpaces.Count == 0)
        {
            isThinking = false;
            yield break;
        }

        GameObject chosenSpace = FindWinningMove(SpaceType.computer);

        if (chosenSpace == null)
            chosenSpace = FindWinningMove(SpaceType.player);

        if (chosenSpace == null)
        {
            int randomIndex = Random.Range(0, emptySpaces.Count);
            chosenSpace = emptySpaces[randomIndex];
        }

        isThinking = false;

        if (inPlay)
            chosenSpace.GetComponent<SpriteChanger>().MakeComputerMove();
    }

    // FIND WINNING OR BLOCKING MOVE
    private GameObject FindWinningMove(SpaceType target)
    {
        foreach (int[] combination in winningCombinations)
        {
            int targetCount = 0;
            GameObject emptySpace = null;

            foreach (int index in combination)
            {
                SpriteChanger cell = spaces[index]
                    .GetComponent<SpriteChanger>();

                if (cell.spaceType == target)
                {
                    targetCount++;
                }
                else if (cell.spaceType == SpaceType.empty)
                {
                    emptySpace = spaces[index];
                }
            }

            if (targetCount == 2 && emptySpace != null)
                return emptySpace;
        }

        return null;
    }

    // AUDIO METHODS
    public void PlayHappySound()
    {
        if (audioSource != null && happyClickSound != null)
            audioSource.PlayOneShot(happyClickSound);
    }

    public void PlayMelancholySound()
    {
        if (audioSource != null && melancholyClickSound != null)
            audioSource.PlayOneShot(melancholyClickSound);
    }

    public void PlaySpaceSelectSound()
    {
        if (audioSource != null && spaceSelectSound != null)
            audioSource.PlayOneShot(spaceSelectSound);
    }

    public void CloseTicTacToe()
    {
        // Don't allow closing during an active round.
        if (inPlay)
            return;

        // Hide all Tic-Tac-Toe UI.
        startPanel.SetActive(false);
        betPanel.SetActive(false);
        gameBoard.SetActive(false);

        winPopUp.SetActive(false);
        drawPopUp.SetActive(false);
        losePopUp.SetActive(false);

        // Re-enable the cookie-clicker interface.
        // Add your cookie UI GameObject reference below.
        cookieGamePanel.SetActive(true);

        // Reset the betting system.
        currentBet = 0;
        hasActiveBet = false;

        PlayHappySound();
    }
}
