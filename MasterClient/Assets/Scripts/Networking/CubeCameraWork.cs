﻿using UnityEngine;
using System.Collections;


/// <summary>
/// Camera work. Follow a target
/// </summary>
public class CubeCameraWork : MonoBehaviour
{
    #region Private Fields


    [Tooltip("Set this as false if a component of a prefab being instanciated by Photon Network, and manually call OnStartFollowing() when and if needed.")]
    [SerializeField]
    private bool followOnStart = false;


    // cached transform of the target
    Transform cameraTransform;


    // maintain a flag internally to reconnect if target is lost or camera is switched
    bool isFollowing;


    #endregion


    #region MonoBehaviour Callbacks


    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during initialization phase
    /// </summary>
    void Start()
    {
        // Start following the target if wanted.
        if (followOnStart)
        {
            OnStartFollowing();
        }


    }


    /// <summary>
    /// MonoBehaviour method called after all Update functions have been called. This is useful to order script execution. For example a follow camera should always be implemented in LateUpdate because it tracks objects that might have moved inside Update.
    /// </summary>
    void LateUpdate()
    {
        // The transform target may not destroy on level load,
        // so we need to cover corner cases where the Main Camera is different everytime we load a new scene, and reconnect when that happens
        if (cameraTransform == null && isFollowing)
        {
            OnStartFollowing();
        }
        // only follow is explicitly declared
        if (isFollowing)
        {
            Apply();
        }
    }


    #endregion


    #region Public Methods


    /// <summary>
    /// Raises the start following event.
    /// Use this when you don't know at the time of editing what to follow, typically instances managed by the photon network.
    /// </summary>
    public void OnStartFollowing()
    {
        cameraTransform = Camera.main.transform;
        isFollowing = true;
        // we don't smooth anything, we go straight to the right camera shot
        Apply();
    }


    #endregion


    #region Private Methods


    /// <summary>
    /// Follow the target smoothly
    /// </summary>
    void Apply()
    {
        Vector3 playPos = transform.position;
        playPos[1] += 1;
        playPos[2] -= 4;
        cameraTransform.position = playPos;
    }

    #endregion
}
