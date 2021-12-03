using Cinemachine;
using DG.Tweening;
using UnityEngine;

namespace Fungus
{
    [CommandInfo("Camera/Cinemachine",
        "Set Tracked Dolly Path Position", 
        "")]
    [AddComponentMenu("")]
    public class SetTrackedDollyPathPosition : BaseTweenableCinemachineVirtualCameraComponent
    {
        [Space]
        [SerializeField] private FloatData targetPosition;
        [SerializeField] private CinemachinePathBase.PositionUnits positionUnits = CinemachinePathBase.PositionUnits.PathUnits;
        
        public override void OnEnter()
        {
            if (virtualCamera == null)
            {
                Debug.LogWarning("Not all requirements met to execute SetTrackedDollyPathPosition, skipping. See the Fungus command's summary for more info.");
                Continue();
                return;
            }
            
            var trackedDolly = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
            if (trackedDolly == null)
            {
                Debug.LogWarning("Virtual camera does not contain a CinemachineTrackedDolly component, skipping.");
                Continue();
                return;
            }

            trackedDolly.m_PositionUnits = positionUnits;
            
            if (!useTween)
            {
                trackedDolly.m_PathPosition = targetPosition;
                Continue();
            }
            else
            {
                DOTween.To(x => trackedDolly.m_PathPosition = x, trackedDolly.m_PathPosition, targetPosition, tweenDuration)
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

            return $"{targetPosition.Value} {positionUnits}";
        }

        public override bool HasReference(Variable variable)
        {
            if (targetPosition.floatRef == variable || tweenDuration.floatRef == variable)
                return true;

            return false;
        }
    }
}