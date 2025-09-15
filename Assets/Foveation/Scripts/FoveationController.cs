using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;
using System.Collections;
using System;

public class FoveationController : MonoBehaviour
{
    private static FoveationController instance;
    public static FoveationController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<FoveationController>();

                if (instance == null)
                {
                    GameObject go = new("FoveationController");
                    instance = go.AddComponent<FoveationController>();
                }
            }

            return instance;
        }
    }

    private XRDisplaySubsystem XRDisplaySubsystem;
    private float strength = 1.0f;

    private void FetchDisplaySubsystem()
    {
        var xrDisplaySubsystems = new List<XRDisplaySubsystem>();
        SubsystemManager.GetSubsystems<XRDisplaySubsystem>(xrDisplaySubsystems);

        if (xrDisplaySubsystems.Count < 1)
        {
            Debug.LogError("No XR Display Subsystem Found");
            return;
        }

        foreach (var subsystem in xrDisplaySubsystems)
        {
            if (subsystem.running)
            {
                XRDisplaySubsystem = subsystem;
                break;
            }
        }

        // Non Eye Tracking
        XRDisplaySubsystem.foveatedRenderingFlags = XRDisplaySubsystem.FoveatedRenderingFlags.None;
    }

    public void UpdateFoveation(float foveation)
    {
        if (XRDisplaySubsystem == null)
        {
            FetchDisplaySubsystem();
        }

        if (XRDisplaySubsystem != null)
        {
            XRDisplaySubsystem.foveatedRenderingLevel = foveation;

            Debug.Log($"Foveation is set to {foveation}");

            strength = foveation;
        }
        else
        {
            Debug.LogError("XRDisplay Subsystem not found");
        }
    }
}
