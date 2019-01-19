using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour {
    public SpriteRenderer playerRenderer;
    public Image hpImage;

    public void Init(Sprite sprite) {
        playerRenderer.sprite = sprite;
    }

    public void SetHp(float hpPercent) {
        hpImage.fillAmount = hpPercent;
    }
}
