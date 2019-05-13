using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Visuals del manageo de HP
public class UIHealthManager : MonoBehaviour
{
    public static UIHealthManager instance;

    PlayerHealthManager healthManager;
    public Image CurrentHP;
    float originalWidth;

    private void Awake()
    {
        instance = this;
    }

    public void Setup(PlayerHealthManager healthManager)
    {
        this.healthManager = healthManager;
        originalWidth = CurrentHP.rectTransform.rect.width;
        healthManager.OnDamaged += PlayerHealthManager_OnDamaged;
        healthManager.OnHeal += PlayerHealthManager_OnHeal;
    }

    private void PlayerHealthManager_OnHeal(object sender, EventArgs e)
    {
        updateValue();
    }

    private void PlayerHealthManager_OnDamaged(object sender, EventArgs e)
    {
        updateValue();
        if (healthManager.GetCurrentHP() <= 0) Debug.Log("you died");
    }

    private void updateValue()
    {
        CurrentHP.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalWidth * 
            ((float)healthManager.GetCurrentHP() / (float)healthManager.GetMaxHP()));
    }
}
