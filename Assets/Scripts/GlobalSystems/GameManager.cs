using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Settings")]
    [SerializeField] private List<GameObject> floorParents;
    [SerializeField] private Transform chestHandleTargetParent, chestHandleTargetPosition;
    [SerializeField] private MeshRenderer goUpButton, goDownButton, goInButton, goOutButton;
    [SerializeField] private Material deactivatedButtonMaterial;
    [SerializeField] private GameObject rat, smell, electricity;
    [SerializeField] private Material topLightMaterialEnd;
    [SerializeField] private MeshRenderer topLightMesh;
    [SerializeField] private Animator cardsPanelAnim;
    [SerializeField] private CinematicaFinal cinematicaCamara;
    [SerializeField] private Animator menuAnim;
    [Header("DEBUG")]
    [SerializeField] private bool debug = false;
    public bool Debug { get => debug; }

    private int floor = 0;
    private bool inside = false;
    private bool[] canGoInside = { false, false };
    private bool[] canBeOutside = { true, false };
    private bool aDragIsMoving = false;
    private bool isWoodPieceInPlace = false;

    private bool mouseOut = false; //Aqui deberia modificarse si se puede usar los switches/ palancas o no
    [SerializeField] private bool switch1On = false;
    private bool switch2On = false;
    private bool switch3On = false;
    private bool hasSpyglass = false;

    private CameraMovement mainCameraMov;

    private Material giOriMat, goOriMat, guOriMat, gdOriMat;

    public List<GameObject> FloorParents { get => floorParents; }
    public int Floor { get => floor; set => floor = value; }
    public bool Inside { get => inside; }
    public bool[] CanGoInside { get => canGoInside; set => canGoInside = value; }
    public bool[] CanBeOutside { get => canBeOutside; set => canBeOutside = value; }
    public Transform ChestHandleTargetParent { get => chestHandleTargetParent; }
    public Transform ChestHandleTargetPosition { get => chestHandleTargetPosition; }
    public bool ADragIsMoving { get => aDragIsMoving; set => aDragIsMoving = value; }
    public CameraMovement MainCameraMov { set => mainCameraMov = value; }
    public bool HasSpyglass { get => hasSpyglass; set => hasSpyglass = value; }
    public bool IsWoodPieceInPlace { get => isWoodPieceInPlace; set => isWoodPieceInPlace = value; }
    public bool MouseOut { get => mouseOut; set => mouseOut = value; }
    public bool Switch1On { get => switch1On; set => switch1On = value; }
    public bool Switch2On { get => switch2On; set => switch2On = value; }
    public bool Switch3On { get => switch3On; set => switch3On = value; }

    private void Start()
    {
        mainCameraMov = FindObjectOfType<CameraMovement>();
        giOriMat = goInButton.material;
        goOriMat = goOutButton.material;
        guOriMat = goUpButton.material;
        gdOriMat = goDownButton.material;
        UpdateUpDownButtonsMats();

        goInButton.material = deactivatedButtonMaterial;
        goOutButton.material = deactivatedButtonMaterial;
        goOutButton.gameObject.SetActive(false);
        menuAnim.SetTrigger("HasToInit");
    }
    private void Update()
    {
        bool a = floor == 0 && inside;
        rat.SetActive(!a);
        smell.SetActive(!a);
        electricity.SetActive(!a);

        if (switch1On && switch2On && switch3On && !CamerasManager.Instance.CanCardsCamActivate)
        {
            CamerasManager.Instance.CanCardsCamActivate = true;
            cardsPanelAnim?.SetTrigger("Open");
        }
    }

    public void ToggleInsideOutside()
    {
        if (canGoInside[floor])
        {
            inside = !inside;
            TransparenceManager.Instance.TransparentTop(inside);
            TransparenceManager.Instance.ResetTransparence();
            mainCameraMov.Transparenting();
            UpdateInOutButtonsMats();
            UpdateUpDownButtonsMats();
        }

    }

    public void UpdateInOutButtonsMats()
    {
        goInButton.GetComponent<Collider>().enabled = !inside;
        goOutButton.GetComponent<Collider>().enabled = inside;
        goInButton.material = !inside ? giOriMat : deactivatedButtonMaterial;
        goOutButton.material = inside ? goOriMat : deactivatedButtonMaterial;

        goInButton.gameObject.SetActive(!inside);
        goOutButton.gameObject.SetActive(inside);
    }
    public void UpdateUpDownButtonsMats()
    {
        if (canBeOutside[1] && floor == 0 && !inside)
        {
            goUpButton.GetComponent<Collider>().enabled = true;
            goUpButton.material = guOriMat;
        }
        else
        {
            goUpButton.GetComponent<Collider>().enabled = false;
            goUpButton.material = deactivatedButtonMaterial;
        }

        if (canBeOutside[0] && floor == 1 && !inside)
        {
            goDownButton.GetComponent<Collider>().enabled = true;
            goDownButton.material = gdOriMat;
        }
        else
        {
            goDownButton.GetComponent<Collider>().enabled = false;
            goDownButton.material = deactivatedButtonMaterial;
        }
    }

    public void SetButtonInside(int floor)
    {
        canGoInside[floor] = true;
        UpdateInOutButtonsMats();
    }

    public void ManualFullUpdateTransparences()
    {
        TransparenceManager.Instance.ResetTransparence();
        TransparenceManager.Instance.UpdateMeshesSpritesAndMats();
        mainCameraMov.Transparenting();
    }
    public void ManualSimpleResetTransparences()
    {
        TransparenceManager.Instance.ResetTransparence();
        mainCameraMov.Transparenting();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            Clickable[] clickables = FindObjectsOfType<Clickable>();
            foreach (Clickable click in clickables)
            {
                click.UnClick();
            }
        }
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Clickable[] clickables = FindObjectsOfType<Clickable>();
            foreach (Clickable click in clickables)
            {
                click.UnClick();
            }
        }
    }

    public void EndGame()
    {
        CardsManager.Instance.SetClickersActive(true);
        CamerasManager.Instance.SetCardsCamActive(false);
        cinematicaCamara.gameObject.SetActive(true);
        mainCameraMov.GetComponent<CameraClicker>().enabled = false;
        topLightMesh.materials[1] = topLightMaterialEnd;
    }
}
