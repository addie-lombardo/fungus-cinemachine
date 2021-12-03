using Cinemachine;
using UnityEngine;

namespace Fungus
{
    [CommandInfo("Camera/Cinemachine",
        "Set Virtual Camera Priority", 
        "")]
    [AddComponentMenu("")]
    public class SetVirtualCameraPriority : Command
    {
        [SerializeField] private CinemachineVirtualCameraBase virtualCamera;
        [Space]
        [SerializeField] private IntegerData priority;
        
        public override void OnEnter()
        {
            if (virtualCamera == null)
            {
                Debug.LogWarning("Not all requirements met to execute SetVirtualCameraPriority, skipping. See the Fungus command's summary for more info.");
                Continue();
                return;
            }

            virtualCamera.Priority = priority;
            Continue();
        }

        public override string GetSummary()
        {
            if (virtualCamera == null)
                return "Error: No VirtualCamera specified";

            return $"Set {virtualCamera.name} priority to {priority.Value}";
        }

        public override Color GetButtonColor()
        {
            return new Color32(216, 228, 170, 255);
        }

        public override bool HasReference(Variable variable)
        {
            if (priority.integerRef == variable)
                return true;

            return false;
        }
    }
}