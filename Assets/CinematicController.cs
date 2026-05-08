using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CinematicController : MonoBehaviour
{
    [Header("Player References")]
    [SerializeField] CharacterController playerController;
    [SerializeField] TeleportationProvider teleportProvider;
    [SerializeField] ContinuousMoveProviderBase moveProvider;
    [SerializeField] ActionBasedSnapTurnProvider turnProvider; 
    
    [Header("Cinematic Settings")]
    [SerializeField] FadeManager fadeManager;
    [SerializeField] List<GameObject> positionSessions = new List<GameObject>();

    private Vector3 originalPlayerPosition;
    private Quaternion originalPlayerRotation;

    void Start()
    {
        if (playerController != null)
        {
            originalPlayerPosition = playerController.transform.position;
            originalPlayerRotation = playerController.transform.rotation;
        }
    }

    public void SwitchPositionSession(int idx)
    {
        if (idx >= 0 && idx < positionSessions.Count)
        {
            StartCoroutine(MoveToCinematicRoutine(idx));
        }
    }

    public void SwitchToXrRigPosition()
    {
        StartCoroutine(ReturnToOriginalRoutine());
    }

    private IEnumerator MoveToCinematicRoutine(int idx)
    {
        originalPlayerPosition = playerController.transform.position;
        originalPlayerRotation = playerController.transform.rotation;

        if (teleportProvider != null) teleportProvider.enabled = false;
        if (moveProvider != null) moveProvider.enabled = false;
        if (turnProvider != null) turnProvider.enabled = false;

        yield return StartCoroutine(fadeManager.FadeOut());

        playerController.enabled = false;
        playerController.transform.position = positionSessions[idx].transform.position;
        playerController.transform.rotation = positionSessions[idx].transform.rotation; 
        
        yield return new WaitForSeconds(0.1f);
        yield return StartCoroutine(fadeManager.FadeIn());
    }

    private IEnumerator ReturnToOriginalRoutine()
    {
        yield return StartCoroutine(fadeManager.FadeOut());

        playerController.transform.position = originalPlayerPosition;
        playerController.transform.rotation = originalPlayerRotation;
        playerController.enabled = true;

        yield return new WaitForSeconds(0.1f);
        yield return StartCoroutine(fadeManager.FadeIn());

        if (teleportProvider != null) teleportProvider.enabled = true;
        if (moveProvider != null) moveProvider.enabled = true;
        if (turnProvider != null) turnProvider.enabled = true;
    }
}