using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cursor : MonoBehaviour
{

    public LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        // Définissez le nombre de points du trait
        lineRenderer.positionCount = 300;

        Gradient gradient = new Gradient();

        List<GradientColorKey> colorKeys = new List<GradientColorKey>();
        colorKeys.Add(new GradientColorKey(Color.red, 0.0f));
        colorKeys.Add(new GradientColorKey(Color.clear, 1.0f));

        List<GradientAlphaKey> alphaKeys = new List<GradientAlphaKey>();
        alphaKeys.Add(new GradientAlphaKey(1.0f, 0.0f));
        alphaKeys.Add(new GradientAlphaKey(0.0f, 1.0f));

        gradient.SetKeys(colorKeys.ToArray(), alphaKeys.ToArray());
        lineRenderer.colorGradient = gradient;
    }

    void Update()
    {
        // Mettez à jour la position du dernier point du trait en fonction de la position de la souris
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, mousePosition);


        for (int i = 0; i < lineRenderer.positionCount - 1; i++)
        {
            lineRenderer.SetPosition(i, lineRenderer.GetPosition(i + 1));
        }

    }
}
