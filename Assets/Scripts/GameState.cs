using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [System.Serializable]
    public struct PlayerHero
    
    {
        public string heroName;
        public Sprite heroImage;
        public int playerNumber;
    }
    public static GameState i;

    public List<PlayerHero> Players;

    public void addHero(string name, Sprite image, int playerNumber)
    {
        PlayerHero newHero;
        newHero.heroName = name;
        newHero.heroImage = image;
        newHero.playerNumber = playerNumber;

        Players.Add(newHero);
    }

    public void removeHero(string name)
    {
        Players.RemoveAt(searchHeroes(name));
    }

    private int searchHeroes(string name)
    {
        int index = -1;
        for (int i = 0; i < Players.Count; i++)
        {
            if (Players[i].heroName == name)
            {
                index = i;
            }
        }

        return index;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Awake()
    {
        if (i == null)
        {
            i = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
