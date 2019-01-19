using System.Collections.Generic;
using UnityEngine;

public class HeroList : MonoBehaviour
{
    [System.Serializable]
    public struct HeroData
    {
        public string name;
        public Sprite heroImage;
    }

    public List<HeroData> Heroes;
}
