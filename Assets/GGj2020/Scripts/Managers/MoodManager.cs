using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;
using Sirenix.OdinInspector;

public class MoodManager : Singleton<MoodManager>
{
    public Light AmbientLight;

    public PostProcessVolume Volume;
    Vignette vignette;

    public float speed;

    private void Start()
    {
        NormalMood();
    }

    [Button]
    public void NormalMood()
    {
        //DestroyCurrentMood();

        var vignette = ScriptableObject.CreateInstance<Vignette>();
        vignette.enabled.Override(true);
        vignette.intensity.Override(1f);

        Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, vignette);
        Volume.isGlobal = true;
        Volume.weight = 10f;
        vignette.intensity.value = 0.150f;
        CameraFollow.Instance.ShakeCamera(0.1f, new Vector2(60, 57));
        AudioManager.Instance.SetMood(0f, 0f);


        DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0.200f, 5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
    [Button]
    public void SemiDarkMood()
    {
        DestroyCurrentMood();

        var vignette = ScriptableObject.CreateInstance<Vignette>();
        vignette.enabled.Override(true);
        vignette.intensity.Override(1f);

        Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, vignette);
        Volume.isGlobal = true;
        Volume.weight = 10f;
        vignette.intensity.value = 0.150f;
        CameraFollow.Instance.ShakeCamera(0.2f, new Vector2(50, 46));
        AudioManager.Instance.SetMood(1f, 0f);

        DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0.180f, 3f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutBounce);
    }

    [Button]
    public void DarkMood()
    {
        DestroyCurrentMood();
        var vignette = ScriptableObject.CreateInstance<Vignette>();
        vignette.enabled.Override(true);
        vignette.intensity.Override(1f);

        Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, vignette);
        Volume.isGlobal = true;
        Volume.weight = 10f;
        vignette.intensity.value = 0.150f;

        AudioManager.Instance.SetMood(1f, 1f);
        CameraFollow.Instance.ShakeCamera(0.3f, new Vector2(45, 40));

        DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0.250f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutBounce);
    }


    void DestroyCurrentMood()
    {
        if(Volume != null)
            RuntimeUtilities.DestroyVolume(Volume, true, true);
    }
}
