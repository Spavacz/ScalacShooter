using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class LobbyPlayer : MonoBehaviour
{
    public int playerNumber = 1;
    public float timeToChangeHero = 1;
    public bool joined = false;
    public HeroList HeroList = null;
    public int chosenHero = 0;
    public Text heroName;
    public Image heroImage;

    private const string INPUT_HORIZONTAL = "Horizontal Player ";
    private const string INPUT_VERTICAL = "Vertical Player ";
    private const string INPUT_CONFIRM = "Fire1Player";
    private const string INPUT_BACK = "Fire2Player";

    private string InputConfirm => INPUT_CONFIRM + (playerNumber + 1);
    private string InputBack => INPUT_BACK + (playerNumber + 1);
    private string InputHorizontal => INPUT_HORIZONTAL + (playerNumber + 1);
    private string InputVertical => INPUT_VERTICAL + (playerNumber + 1);

    private float changeHeroTimer = 1;

    void changeHero()
    { 
    var inputVector = new Vector3(Input.GetAxis(InputHorizontal), 0, Input.GetAxis(InputVertical));
        if(inputVector.x < -0.3 && changeHeroTimer <= 0)
        {
            Debug.Log("change hero left");
            if (chosenHero == 0)
            {
                chosenHero = HeroList.Heroes.Count - 1;
            }
            else
            {
                chosenHero -= 1;
            }

            changeHeroTimer = 1;

        } else if (inputVector.x > 0.3 && changeHeroTimer <= 0)
        {
            Debug.Log("change hero right");
            if(chosenHero == HeroList.Heroes.Count-1)
            {
                chosenHero = 0;
            }
            else
            {
                chosenHero += 1;
            }

            changeHeroTimer = timeToChangeHero;
        }

        heroName.text = HeroList.Heroes[chosenHero].name;
        heroImage.sprite = HeroList.Heroes[chosenHero].heroImage;
        heroImage.color = new Color(255, 255, 255, 255);
    }

    void joinGame()
    {
        bool joinGame = Input.GetButtonDown(InputConfirm);
        bool leaveGame = Input.GetButtonDown(InputBack);

        if (joinGame)
        {
            joined = true;
        }
        else if (leaveGame)
        {
            joined = false;
            heroImage.color = new Color(255, 255, 255, 0);
            heroName.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        changeHeroTimer -= Time.deltaTime;
        joinGame();
        if(joined)
        {
            changeHero();
        }
    }
}
