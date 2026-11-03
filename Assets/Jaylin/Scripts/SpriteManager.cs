using UnityEngine;

public enum SpaceType
{
    empty, player, computer
}

public class SpriteChanger : MonoBehaviour
{
    private SpriteRenderer renderSprite;
    public Sprite playerSprite;
    public Sprite computerSprite;
    public GameplayManager gameplayManager;
    public SpaceType spaceType;

    void Start()
    {
        renderSprite = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (gameplayManager.inPlay == true)
        {
            if (renderSprite.sprite == null) //can be changed
            {
                if (gameplayManager.playerTurn)
                {
                    gameplayManager.PlaySpaceSelectSound();
                    renderSprite.sprite = playerSprite;
                    spaceType = SpaceType.player;

                    gameplayManager.CheckForWin();

                    if (gameplayManager.inPlay)
                        gameplayManager.CheckForDraw();

                    if (gameplayManager.inPlay)
                    {
                        gameplayManager.playerTurn = false;
                        gameplayManager.StartCoroutine(gameplayManager.ComputerTurn());
                    }
                }
            }
            else
            {
                Debug.LogWarning("Neither!");
                Debug.LogWarning(spaceType);
                return; //Don't change anything
            }
        }
    }

    public void MakeComputerMove()
    {
        gameplayManager.PlaySpaceSelectSound();
        renderSprite.sprite = computerSprite;
        spaceType = SpaceType.computer;

        gameplayManager.CheckForWin();

        if (gameplayManager.inPlay) gameplayManager.CheckForDraw();

        if (gameplayManager.inPlay) gameplayManager.playerTurn = true;
    }
}
