using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour {

    public List<Material> materials;
    public SpriteRenderer playerRenderer;
    public Image hpImage;
    public MeshRenderer meshRenderer;

    public void Init(GameState.PlayerHero hero) {
        playerRenderer.sprite = hero.heroImage;
        meshRenderer.material = materials[hero.playerNumber];
    }

    public void SetHp(float hpPercent) {
        hpImage.fillAmount = hpPercent;
    }

    private void Update() {
        transform.rotation = Quaternion.identity;
    }
}
