using Cinemachine;
using UnityEngine;

namespace Fungus
{
    [CommandInfo("Camera/Cinemachine",
        "Set Virtual Camera Follow", 
        "")]
    [AddComponentMenu("")]
    public class SetVirtualCameraFollow : Command
    {
        [SerializeField] private CinemachineVirtualCameraBase virtualCamera;
        [Space]
        [SerializeField] private TransformData targetFollow;
        
        public override void OnEnter()
        {
            if (virtualCamera == null)
            {
                Debug.LogWarning("Not all requirements met to execute SetVirtualCameraFollow, skipping. See the Fungus command's summary for more info.");
                Continue();
                return;
            }

            virtualCamera.Follow = targetFollow;
            Continue();
        }

        public override string GetSummary()
        {
            if (virtualCamera == null)
                return "Error: No VirtualCamera specified";

            var follow = targetFollow.Value ? targetFollow.Value.name : "null";
            return $"Set {virtualCamera.name} follow to {follow}";
        }

        public override Color GetButtonColor()
        {
            return new Color32(216, 228, 170, 255);
        }

        public override bool HasReference(Variable variable)
        {
            if (targetFollow.transformRef == variable)
                return true;

            return false;
        }
    }
}