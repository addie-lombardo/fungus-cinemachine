using Cinemachine;
using UnityEngine;

namespace Fungus
{
    [CommandInfo("Camera/Cinemachine",
        "Set Virtual Camera Look At", 
        "")]
    [AddComponentMenu("")]
    public class SetVirtualCameraLookAt : Command
    {
        [SerializeField] private CinemachineVirtualCameraBase virtualCamera;
        [Space]
        [SerializeField] private TransformData targetLookAt;
        
        public override void OnEnter()
        {
            if (virtualCamera == null)
            {
                Debug.LogWarning("Not all requirements met to execute SetVirtualCameraLookAt, skipping. See the Fungus command's summary for more info.");
                Continue();
                return;
            }

            virtualCamera.LookAt = targetLookAt;
            Continue();
        }

        public override string GetSummary()
        {
            if (virtualCamera == null)
                return "Error: No VirtualCamera specified";

            return $"Set {virtualCamera.name} look at to {targetLookAt.Value}";
        }

        public override Color GetButtonColor()
        {
            return new Color32(216, 228, 170, 255);
        }

        public override bool HasReference(Variable variable)
        {
            if (targetLookAt.transformRef == variable)
                return true;

            return false;
        }
    }
}