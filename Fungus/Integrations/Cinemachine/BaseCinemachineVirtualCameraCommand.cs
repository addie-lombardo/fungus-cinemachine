using Cinemachine;
using UnityEngine;

namespace Fungus
{
    public abstract class BaseCinemachineVirtualCameraCommand : Command
    {
        [SerializeField] protected CinemachineVirtualCamera virtualCamera;
    
        public override Color GetButtonColor()
        {
            return new Color32(216, 228, 170, 255);
        }
    }
}