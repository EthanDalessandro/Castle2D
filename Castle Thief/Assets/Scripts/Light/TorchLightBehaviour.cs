using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class TorchLightBehaviour : MonoBehaviour
{
    List<GameObject> lights;

    [SerializeField] private int lightCut = 6;
    [SerializeField] private float maxLightSpeedFluctuation;
    [SerializeField] private float minLightSpeedFluctuation;

    private void Awake()
    {
        lights = new List<GameObject>(GameObject.FindGameObjectsWithTag("Torch"));
    }
    private void Update()
    {
        for(int i = 0; i < lights.Count; i++)
        {
            Light2D currentLight = lights[i].GetComponent<Light2D>();

            if(currentLight.intensity >= maxLightSpeedFluctuation)
            {
                currentLight.intensity = minLightSpeedFluctuation;
            }
            if(lightCut <= 1)
            {
                lightCut = 2;
            }
            currentLight.intensity += (maxLightSpeedFluctuation - minLightSpeedFluctuation / lightCut) * Time.deltaTime;
        }
    }
}
