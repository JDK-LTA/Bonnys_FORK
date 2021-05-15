using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessManager : Singleton<PostProcessManager>
{
    [SerializeField] private Volume postProcessingVolume;

    private Bloom bloom;
    private Vignette vignette;
    private DepthOfField depthOfField;
    private ColorAdjustments colorAdjustments;
    private LensDistortion lensDistorsion;

    private float timeToNextColor = 0.6f;

    private float timer1;

    // Start is called before the first frame update
    void Start()
    {
        postProcessingVolume.profile.TryGet<Bloom>(out bloom);
        postProcessingVolume.profile.TryGet<Vignette>(out vignette);
        postProcessingVolume.profile.TryGet<DepthOfField>(out depthOfField);
        postProcessingVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments);
        postProcessingVolume.profile.TryGet<LensDistortion>(out lensDistorsion);
        //WaitTimeForParty();
    }

    // Update is called once per frame
    void Update()
    {
        //if (timer1 <= 0)
        //{
        //    StartCoroutine("WaitTimeForParty");
        //    timer1 = timeToNextColor;
        //}
        //timer1 -= Time.deltaTime;
    }

    public void TurnMagent()
    {
        colorAdjustments.colorFilter.value = Color.magenta;
        vignette.intensity.value = 0.9f;
        lensDistorsion.intensity.value = -0.6f;
        lensDistorsion.scale.value = 2f;
        UIManager.Instance.currentScreenColorMode = UIManager.ScreenColorMode.MAGENT;
    }

    public void TurnCyan()
    {
        colorAdjustments.colorFilter.value = Color.cyan;
        vignette.intensity.value = 0.9f;
        lensDistorsion.intensity.value = -0.6f;
        lensDistorsion.scale.value = 2f;
        UIManager.Instance.currentScreenColorMode = UIManager.ScreenColorMode.CYAN;
    }

    public void TurnYellow()
    {
        colorAdjustments.colorFilter.value = Color.yellow;
        vignette.intensity.value = 0.9f;
        lensDistorsion.intensity.value = -0.6f;
        lensDistorsion.scale.value = 2f;
        UIManager.Instance.currentScreenColorMode = UIManager.ScreenColorMode.YELLOW;
    }

    public void TurnNormal()
    {
        colorAdjustments.colorFilter.value = Color.white;
        vignette.intensity.value = 0.25f;
        lensDistorsion.intensity.value = 0f;
        lensDistorsion.scale.value = 1f;
        UIManager.Instance.currentScreenColorMode = UIManager.ScreenColorMode.NORMAL;
    }

    private IEnumerator WaitTimeForParty()
    {
        TurnCyan();
        yield return new WaitForSeconds(0.2f);
        TurnMagent();
        yield return new WaitForSeconds(0.2f);
        TurnYellow();
        yield return new WaitForSeconds(0.2f);
    }
}
