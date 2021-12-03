using DG.Tweening;
using UnityEngine;

namespace Fungus
{
    public abstract class BaseTweenableCinemachineVirtualCameraComponent : BaseCinemachineVirtualCameraCommand
    {
        [Header("Tween Settings")]
        [SerializeField] protected bool useTween;
        [SerializeField] protected string tweenId;
        [SerializeField] protected FloatData tweenDuration;
        [SerializeField] protected Ease tweenEase = Ease.InOutSine;
        [SerializeField] protected bool isSpeedBased;
        [SerializeField] protected bool waitUntilComplete;
    }
}