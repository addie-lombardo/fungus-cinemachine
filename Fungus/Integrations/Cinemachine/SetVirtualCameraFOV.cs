using DG.Tweening;
using UnityEngine;

namespace Fungus
{
    [CommandInfo("Camera/Cinemachine",
        "Set Virtual Camera FOV", 
        "")]
    [AddComponentMenu("")]
    public class SetVirtualCameraFOV : BaseTweenableCinemachineVirtualCameraComponent
    {
        [Space]
        [Tooltip("1,179")]
        [SerializeField] private FloatData targetFOV = new FloatData(60f);
        
        public override void OnEnter()
        {
            if (virtualCamera == null || targetFOV.Value > 179 || targetFOV < 1)
            {
                Debug.LogWarning("Not all requirements met to execute SetVirtualCameraFOV, skipping. See the Fungus command's summary for more info.");
                Continue();
                return;
            }

            if (!useTween)
            {
                virtualCamera.m_Lens.FieldOfView = targetFOV;
                Continue();
            }
            else
            {
                DOTween.To(x => virtualCamera.m_Lens.FieldOfView = x, virtualCamera.m_Lens.FieldOfView, targetFOV, tweenDuration)
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
            if (targetFOV.Value > 179 || targetFOV < 1)
                return "Error: TargetDutch must be between 1 and 179";

            var summary = useTween ? "Tween " : "Set ";
            return $"{summary}{virtualCamera.name} FOV to {targetFOV.Value}";
        }

        public override bool HasReference(Variable variable)
        {
            if (targetFOV.floatRef == variable)
                return true;

            return false;
        }
    }
}