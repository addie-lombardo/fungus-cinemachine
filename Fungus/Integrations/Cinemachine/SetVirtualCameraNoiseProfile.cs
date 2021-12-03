using Cinemachine;
using UnityEngine;

namespace Fungus
{
    [CommandInfo("Camera/Cinemachine",
        "Set Virtual Camera Noise Profile", 
        "")]
    [AddComponentMenu("")]
    public class SetVirtualCameraNoiseProfile : BaseCinemachineVirtualCameraCommand
    {
        [Space]
        [SerializeField] private NoiseSettings targetProfile;
        
        public override void OnEnter()
        {
            if (virtualCamera == null)
            {
                Debug.LogWarning("Not all requirements met to execute SetVirtualCameraOrthoSize, skipping. See the Fungus command's summary for more info.");
                Continue();
                return;
            }

            var noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (noise == null)
                noise = virtualCamera.AddCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            noise.m_NoiseProfile = targetProfile;
        }

        public override string GetSummary()
        {
            if (virtualCamera == null)
                return "Error: No VirtualCamera specified";

            return targetProfile != null ? targetProfile.name : "none";
        }
    }
}