using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    protected SpriteRenderer sr;

    [Header("PopUP Text")]
    [SerializeField] private GameObject popUpTextPrefab;




    [Header("Flash FX")]
    public float flashDuration;
    public Material hitMat;
    Material originalMat;

    [Header("Ailment colors")]
    [SerializeField] private Color[] chillcolor;
    [SerializeField] private Color[] igniteColor;
    [SerializeField] private Color[] shockColor;

    [Header("Ailment particles")]
    [SerializeField] private ParticleSystem igniteFx;
    [SerializeField] private ParticleSystem chillFx;
    [SerializeField] private ParticleSystem shockFx;

    [Header("Hit Fx")]
    [SerializeField] private GameObject hitFX;
    [SerializeField] private GameObject hitFXcrit;

    private GameObject myHealthBar;
    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;

        myHealthBar = GetComponentInChildren<UI_HealthBar>().gameObject;
    }

    public void CreatePoPupUpText(string _text)
    {
        float randomX = Random.Range(-1, 1);
        float randomY = Random.Range(1.5f, 3f);
        Vector3 positionOffset = new Vector3(randomX, randomY, 0);
        GameObject newText = Instantiate(popUpTextPrefab, transform.position + positionOffset, Quaternion.identity);
        newText.GetComponent<TextMeshPro>().text = _text;
    }


    public void MakeTransprent(bool _transprent)
    {
        if (_transprent)
        {
            myHealthBar.SetActive(false);
            sr.color = Color.clear;
        }
        else
        {
            myHealthBar.SetActive(true);
            sr.color = Color.white;
        }
    }
    IEnumerator FlashFX()
    {
        sr.material = hitMat;

        Color currentColor = sr.color;
        sr.color = Color.white;

        yield return new WaitForSeconds(flashDuration);

        sr.color = currentColor;
        sr.material = originalMat;
    }
    void RedColorBlink()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;
        else sr.color = Color.red;
    }
    void CancelColorChange()
    {
        CancelInvoke();
        sr.color = Color.white;

        igniteFx.Stop();
        chillFx.Stop();
        shockFx.Stop();
    }
    public void IgniteFxFor(float _seconds)
    {
        igniteFx.Play();
        InvokeRepeating("IgniteColorFx", 0, 0.3f);
        Invoke("CancelColorChange", _seconds);
    }
    public void ShockeFxFor(float _seconds)
    {
        shockFx.Play();
        InvokeRepeating("ShockColorFx", 0, 0.3f);
        Invoke("CancelColorChange", _seconds);
    }
    public void ChillFxFor(float _seconds)
    {
        chillFx.Play();
        InvokeRepeating("ChillColorFx", 0, 0.3f);
        Invoke("CancelColorChange", _seconds);
    }
    private void IgniteColorFx()
    {
        if (sr.color != igniteColor[0])
            sr.color = igniteColor[0];
        else
            sr.color = igniteColor[1];
    }
    private void ShockColorFx()
    {
        if (sr.color != shockColor[0])
            sr.color = shockColor[0];
        else
            sr.color = shockColor[1];
    }
    private void ChillColorFx()
    {
        if (sr.color != chillcolor[0])
            sr.color = chillcolor[0];
        else
            sr.color = chillcolor[1];
    }

    public void CreateHitFx(Transform _target, bool _crital)
    {
        float zRotation = Random.Range(-90, 90);
        float xPosition = Random.Range(-0.5f, 0.5f);
        float yPosition = Random.Range(-0.5f, 0.5f);

        Vector3 hitFXRotation = new Vector3(0, 0, zRotation);

        GameObject hitprefab = hitFX;

        if (_crital)
        {
            hitprefab = hitFXcrit;

            float yRotation = 0;
            zRotation = Random.Range(-45, 45);

            if (GetComponent<Entity>().facingDir == -1)
                yRotation = 180;

            hitFXRotation = new Vector3(0, yRotation, zRotation);
        }
        GameObject newhitFx = Instantiate(hitprefab, _target.position + new Vector3(xPosition, yPosition), Quaternion.identity);

        newhitFx.transform.Rotate(hitFXRotation);

        Destroy(newhitFx, 0.5f);
    }

}
