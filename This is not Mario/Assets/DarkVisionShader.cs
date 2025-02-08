using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkVisionShader : MonoBehaviour {

    #region Variables

    public Shader nightVisionShader;
    public Color nightVisionColor = Color.white;
    public float contrast = 2.0f;
    public float brightness = 1.0f;
    public float distortion = 0.2f;
    public float scale = 0.8f;
    private float randomValue = 0.0f;
    public float marioX = 0f;
    public float marioY = 0f;
    private Material curMaterial;
    public GameObject mario;
    public Texture2D vignetteTexture;

    #endregion

    Material material
    {

        get
        {
            if(curMaterial==null)
            {
                curMaterial = new Material(nightVisionShader);
                curMaterial.hideFlags = HideFlags.HideAndDontSave;

            }
            return curMaterial;
        }
    }

    void Start()
    {
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }
        else
        {
            if (!nightVisionShader && !nightVisionShader.isSupported)
                enabled = false;
        }
    }

    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        material.SetFloat("_Contrast", contrast);
        material.SetColor("_NightVisionColor",nightVisionColor);
        material.SetFloat("_Brightness", brightness);
        material.SetTexture("_VignetteTex",vignetteTexture);
        material.SetFloat("_RandomValue", randomValue);
        material.SetFloat("_distortion", distortion);
        material.SetFloat("_scale", scale);
        material.SetFloat("_marioX", marioX);
        material.SetFloat("_marioY", marioY);


        Graphics.Blit(sourceTexture, destTexture, material);

    }

    void Update () {
        Camera c = Camera.main;
        Vector3 p = c.WorldToViewportPoint(new Vector3(mario.transform.position.x,mario.transform.position.y))+new Vector3(-0.5f,-0.5f,0);
        contrast = Mathf.Clamp(contrast, 0f, 4f);
        brightness = Mathf.Clamp(brightness, 0f, 2f);
        randomValue = Random.Range(-1f, 1f);
        //distortion = Mathf.Clamp(distortion, -1f, 1f);
        distortion = Mathf.Sin(Time.time);
        scale = 1-0.2f * Mathf.Sin(Time.time);
        marioX = p.x;
        marioY = p.y;
        c.GetComponent<Camera>().orthographicSize=13+ 5f*Mathf.Sin(Time.time);
    }

    /**void OnDisable()
    {
        if(nightVisionShader)
        {
            DestroyImmediate(nightVisionShader);
        }
    }**/
}
