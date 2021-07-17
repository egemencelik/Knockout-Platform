using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Paintable : MonoBehaviour
{
    [SerializeField] private GameObject brush;
    [SerializeField] private float brushSize;
    [SerializeField] private RenderTexture renderTexture;

    [SerializeField]
    private Slider slider;

    private LevelManager levelManager;
    private Color referance;
    private bool percentageCheck;

    private GameObject go;
    private Texture2D texture2D;

    private readonly HashSet<Vector3> paintedPositions = new HashSet<Vector3>();
    private static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
    private static readonly WaitForSeconds WaitForSeconds = new WaitForSeconds(.15f);

    private void Awake()
    {
        referance = brush.GetComponentInChildren<MeshRenderer>().sharedMaterial.color;
    }

    private void Start()
    {
        
        texture2D = new Texture2D(renderTexture.width, renderTexture.height);
    }

    private bool IsPointPainted(Vector3 point)
    {
        return paintedPositions.Count(p => Vector3.Distance(p, point) < .3f) > 0;
    }

    public void Paint(Vector3 position)
    {
        if (!percentageCheck)
        {
            percentageCheck = true;
            StartCoroutine(CheckPaintPercentage());
        }

        if (IsPointPainted(position)) return;

        paintedPositions.Add(position);
        go = Instantiate(brush, position, Quaternion.Euler(-90, 0, 0), transform);
        go.transform.localScale = Vector3.one * brushSize / 1000;
    }


    private IEnumerator CheckPaintPercentage()
    {
        while (percentageCheck)
        {
            StartCoroutine(CalculatePaintPercentage());
            yield return WaitForSeconds;
        }
    }

    private IEnumerator CalculatePaintPercentage()
    {
        yield return WaitForEndOfFrame;

        RenderTexture.active = renderTexture;

        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();
        var percentage = CalculateFill(texture2D.GetPixels(), referance, 0.25f);
        slider.value = percentage;

        if (percentage >= .9882812f) // algorithm calculates max percentage as this even if all pixels are painted
        {
            percentageCheck = false;
            slider.value = 100;
            LevelManager.Instance.LevelWon();
            StopAllCoroutines();
        }
    }

    private static float CalculateFill(Color[] colors, Color reference, float tolerance)
    {
        Vector3 target = new Vector3 { x = reference.r, y = reference.g, z = reference.b };
        int numHits = 0;
        const float sqrt_3 = 1.73205080757f;
        for (var i = 0; i < colors.Length; i++)
        {
            Vector3 next = new Vector3 { x = colors[i].r, y = colors[i].g, z = colors[i].b };
            float mag = Vector3.Magnitude(target - next) / sqrt_3;
            numHits += mag <= tolerance ? 1 : 0;
        }

        return (float)numHits / colors.Length;
    }
}