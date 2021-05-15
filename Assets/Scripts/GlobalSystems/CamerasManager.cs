using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasManager : Singleton<CamerasManager>
{
    [SerializeField] private Camera mainCamera, chestCam, gramophoneCam, rudderCam, chestFeedbackCam, skullCam, cardsCam, woodAndDoorCam, hammerCam;
    [SerializeField] private GameObject chestColliderAux, chestLockReceiverAux;
    [SerializeField] private float timeToDeactivateOverlays = 1.5f;

    private CameraClicker mainClicker;

    private bool chestCamActive = false, canChestCamActivate = false;
    private bool cardsCamActive = false, canCardsCamActivate = false;

    public Camera ChestCam { get => chestCam; }
    public bool ChestCamActive { get => chestCamActive; }
    public bool CanChestCamActivate { get => canChestCamActivate; set => canChestCamActivate = value; }
    public bool CardsCamActive { get => cardsCamActive; }
    public bool CanCardsCamActivate { get => canCardsCamActivate; set => canCardsCamActivate = value; }

    private void Start()
    {
        mainClicker = mainCamera.GetComponent<CameraClicker>();
    }

    public void SetChestCamActive(bool active)
    {
        if (canChestCamActivate)
        {
            mainCamera.GetComponent<CameraMovement>().enabled = !active;
            mainCamera.GetComponent<CameraZoom>().enabled = !active;
            chestCam.gameObject.SetActive(active);
            mainClicker.enabled = !active;
            chestCamActive = active;
        }
    }
    public void SetCardsCamActive(bool active)
    {
        if (canCardsCamActivate)
        {
            mainCamera.GetComponent<CameraMovement>().enabled = !active;
            mainCamera.GetComponent<CameraZoom>().enabled = !active;
            cardsCam.gameObject.SetActive(active);
            mainClicker.enabled = !active;
            cardsCamActive = active;
        }
    }
    public void SetGramCamActive(bool active)
    {
        if (active)
        {
            gramophoneCam.gameObject.SetActive(true);
            SetGramCamActive(false);
        }
        else
            Invoke("DeactivateGramCam", timeToDeactivateOverlays);
    }
    private void DeactivateGramCam()
    {
        gramophoneCam.gameObject.SetActive(false);
    }
    public void SetWoodAndDoorCamActive(bool active)
    {
        if (active)
        {
            woodAndDoorCam.gameObject.SetActive(true);
            SetGramCamActive(false);
        }
        else
            Invoke("DeactivateWoodAndDoorCam", timeToDeactivateOverlays);
    }
    private void DeactivateWoodAndDoorCam()
    {
        gramophoneCam.gameObject.SetActive(false);
    }
    public void SetHammerCamActive(bool active)
    {
        if (active)
        {
            hammerCam.gameObject.SetActive(true);
            SetGramCamActive(false);
        }
        else
            Invoke("DeactivateHammerCam", timeToDeactivateOverlays);
    }
    private void DeactivateHammerCam()
    {
        hammerCam.gameObject.SetActive(false);
    }
    public void SetRudderCamActive(bool active)
    {
        if (active)
        {
            SetGramCamActive(false);
            rudderCam.gameObject.SetActive(true);
        }
        else
            Invoke("DeactivateRudderCam", timeToDeactivateOverlays);
    }
    private void DeactivateRudderCam()
    {
        rudderCam.gameObject.SetActive(false);
    }
    public void SetChestFeedbackCamActive(bool active)
    {
        if (active)
        {
            SetGramCamActive(false);
            chestFeedbackCam.gameObject.SetActive(true);
        }
        else
            Invoke("DeactivateChestFeedbackCam", timeToDeactivateOverlays);
    }
    private void DeactivateChestFeedbackCam()
    {
        chestFeedbackCam.gameObject.SetActive(false);
    }
    public void SetSkullCamActive(bool active)
    {
        if (active)
        {
            SetGramCamActive(false);
            skullCam.gameObject.SetActive(true);
        }
        else
            Invoke("DeactivateSkullCam", timeToDeactivateOverlays);
    }
    private void DeactivateSkullCam()
    {
        skullCam.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (GameManager.Instance.Debug && chestCamActive && Input.GetKeyDown(KeyCode.Escape))
        {
            DeactivateChestVision();
        }
    }

    public void DeactivateChestVision()
    {
        SetChestCamActive(false);
        chestColliderAux.SetActive(false);
        chestLockReceiverAux.SetActive(false);
    }
}
