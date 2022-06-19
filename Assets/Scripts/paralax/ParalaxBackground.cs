using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxBackground : MonoBehaviour
{
    public ParalaxCamera ParalaxCamera;
    List<ParalaxLayer> ParalaxLayers = new List<ParalaxLayer>();

    void Start()
    {
        if (ParalaxCamera == null)
            ParalaxCamera = Camera.main.GetComponent<ParalaxCamera>();
        if (ParalaxCamera != null)
            ParalaxCamera.onCameraTranslate += Move;
        SetLayers();
    }

    void SetLayers()
    {
        ParalaxLayers.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            ParalaxLayer layer = transform.GetChild(i).GetComponent<ParalaxLayer>();

            if (layer != null)
            {
                layer.name = "Layer-" + i;
                ParalaxLayers.Add(layer);
            }
        }
    }
    void Move(float delta)
    {
        foreach (ParalaxLayer layer in ParalaxLayers)
        {
            layer.Move(delta);
        }
    }
}
