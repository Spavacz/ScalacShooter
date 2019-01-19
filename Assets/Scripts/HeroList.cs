using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroList : MonoBehaviour
{
    [System.Serializable]
    public struct HeroData
    {
        public string name;
        public Sprite heroImage;
    }

    public List<HeroData> Heroes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
