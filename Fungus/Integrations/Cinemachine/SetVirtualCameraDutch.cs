using DG.Tweening;
using UnityEngine;

namespace Fungus
{
    [CommandInfo("Camera/Cinemachine",
        "Set Virtual Camera Dutch", 
        "")]
    [AddComponentMenu("")]
    public class SetVirtualCameraDutch : BaseTweenableCinemachineVirtualCameraComponent
    {
        [Space]
        [Tooltip("-180,180")]
        [SerializeField] private FloatData targetDutch;
        
        public override void OnEnter()
        {
            if (virtualCamera == null || targetDutch.Value > 180 || targetDutch < -180)
            {
                Debug.LogWarning("Not all requirements met to execute SetVirtualCameraDutch, skipping. See the Fungus command's summary for more info.");
                Continue();
                return;
            }

            if (!useTween)
            {
                virtualCamera.m_Lens.Dutch = targetDutch;
                Continue();
            }
            else
            {
                DOTween.To(x => virtualCamera.m_Lens.Dutch = x, virtualCamera.m_Lens.Dutch, targetDutch, tweenDuration)
                    .SetId(tweenId)
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
            if (virtualCamera == null)
                return "Error: No VirtualCamera specified";
            if (targetDutch.Value > 180 || targetDutch < -180)
                return "Error: TargetDutch must be between -180 and 180";
                
            var summary = useTween ? "Tween " : "Set ";
            return $"{summary}{virtualCamera.name} dutch to {targetDutch.Value}";
        }

        public override bool HasReference(Variable variable)
        {
            if (targetDutch.floatRef == variable)
                return true;

            return false;
        }
    }
}