using Cinemachine;
using DG.Tweening;
using UnityEngine;

namespace Fungus
{
    [CommandInfo("Camera/Cinemachine",
        "Set Mixing Camera Weight", 
        "")]
    [AddComponentMenu("")]
    public class SetMixingCameraWeight : Command
    {
        [SerializeField] private CinemachineMixingCamera mixingCamera;
        [Space] 
        [SerializeField] private IntegerData childVirtualCameraIndex;
        [SerializeField] private CinemachineVirtualCameraBase childVirtualCamera;
        [SerializeField] private FloatData targetWeight = new FloatData(0.5f);
        [Space]
        [Header("Tween Settings")]
        [SerializeField] protected bool useTween;
        [SerializeField] protected string tweenId;
        [SerializeField] protected FloatData tweenDuration;
        [SerializeField] protected Ease tweenEase = Ease.InOutSine;
        [SerializeField] protected bool isSpeedBased;
        [SerializeField] protected bool waitUntilComplete;
        
        public override void OnEnter()
        {
            if (mixingCamera == null || childVirtualCameraIndex < 0 || childVirtualCameraIndex > 7 || targetWeight.Value < 0)
            {
                Debug.LogWarning("Not all requirements met to execute SetMixingCameraWeights, skipping. See the Fungus command's summary for more info.");
                Continue();
                return;
            }
            
            if (!useTween)
            {
                if (childVirtualCamera)
                    mixingCamera.SetWeight(childVirtualCamera, targetWeight);
                else
                    mixingCamera.SetWeight(childVirtualCameraIndex, targetWeight);
                
                Continue();
            }
            else
            {
                 Tween tween = childVirtualCamera
                    ? DOTween.To(x => mixingCamera.SetWeight(childVirtualCamera, x), mixingCamera.GetWeight(childVirtualCamera), targetWeight, tweenDuration)
                    : DOTween.To(x => mixingCamera.SetWeight(childVirtualCameraIndex, x), mixingCamera.GetWeight(childVirtualCameraIndex), targetWeight, tweenDuration);
                
                tween.SetId(tweenId)
                    .SetEase(tweenEase)
                    .SetSpeedBased(isSpeedBased)
                    .OnComplete(() =>
                    {
                        if (waitUntilComplete)
                            Continue();
                    });
                
                if (!waitUntilComplete)
                {
                    Continue();
                }
            }
        }

        public override string GetSummary()
        {
            if (mixingCamera == null)
                return "Error: No MixingCamera specified";
            if (childVirtualCameraIndex < 0 || childVirtualCameraIndex > 7)
                return "Error: ChildVirtualCameraIndex must be between 0 and 7";
            if (targetWeight.Value < 0)
                return "Error: Weight cannot be less than 0";
            
            return "";
        }
        
        public override Color GetButtonColor()
        {
            return new Color32(216, 228, 170, 255);
        }
    }
}