using DG.Tweening;
using UnityEngine;

namespace Fungus
{
    [CommandInfo("Camera/Cinemachine",
        "Set Virtual Camera Ortho Size", 
        "")]
    [AddComponentMenu("")]
    public class SetVirtualCameraOrthoSize : BaseTweenableCinemachineVirtualCameraComponent
    {
        [Space]
        [SerializeField] private FloatData targetOrthoSize;
        
        public override void OnEnter()
        {
            if (virtualCamera == null)
            {
                Debug.LogWarning("Not all requirements met to execute SetVirtualCameraOrthoSize, skipping. See the Fungus command's summary for more info.");
                Continue();
                return;
            }

            if (!useTween)
            {
                virtualCamera.m_Lens.OrthographicSize = targetOrthoSize;
                Continue();
            }
            else
            {
                DOTween.To(x => virtualCamera.m_Lens.OrthographicSize = x, virtualCamera.m_Lens.OrthographicSize, targetOrthoSize, tweenDuration)
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

            return targetOrthoSize.Value.ToString();
        }

        public override bool HasReference(Variable variable)
        {
            if (targetOrthoSize.floatRef == variable)
                return true;

            return false;
        }
    }
}