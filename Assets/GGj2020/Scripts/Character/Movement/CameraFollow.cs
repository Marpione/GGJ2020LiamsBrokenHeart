using UnityEngine;
using DG.Tweening;

public class CameraFollow : Singleton<CameraFollow>
{

    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private Vector3 velocity = Vector3.zero;


    Camera camera;

    Camera Camera { get { return (camera == null) ? camera = GetComponentInChildren<Camera>() : camera; } }

    Tween shakeTween;
    Tween fovTween;


    public void ShakeCamera(float shake, Vector2 fov)
    {
        KillShake();
        Sequence sequence = DOTween.Sequence();

        sequence.Append(Camera.DOFieldOfView(fov.x, 3f))
            .Join(Camera.transform.DOLocalMove(Vector2.zero, 3f))
            .OnComplete(()=> {
                shakeTween = Camera.transform.DOShakePosition(5f, shake, 1, 5).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
                fovTween = Camera.DOFieldOfView(fov.y, 7f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutFlash);
            });
        
        shakeTween.SetTarget(0);
        fovTween.SetTarget(1);
    }



    void KillShake()
    {
        if (shakeTween == null || fovTween == null)
            return;

        shakeTween.Kill();
        fovTween.Kill();
    }

    void LateUpdate()
    {
        //Vector3 targetPosition = target.TransformPoint(new Vector3(0, 5, -10));

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }

}   