using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFx : EntityFX
{
    [Header("After image fx")]
    [SerializeField] private GameObject afterImagePrefab;
    [SerializeField] private float colorLossRate;
    [SerializeField] private float afterImageCooldown;
    private float afterImageCooldownTimer;

    private void Update()
    {
        afterImageCooldownTimer -= Time.deltaTime;
    }
    public void CreatAftrerImage()
    {
        if (afterImageCooldownTimer < 0)
        {
            afterImageCooldownTimer = afterImageCooldown;
            GameObject newAfterImage = Instantiate(afterImagePrefab, transform.position, transform.rotation);
            newAfterImage.GetComponent<AfterImageFx>().SetupAfterImage(colorLossRate, sr.sprite);
        }
    }
}
