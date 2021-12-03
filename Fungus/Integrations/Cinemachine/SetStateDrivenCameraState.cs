using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Fungus
{
    [CommandInfo("Camera/Cinemachine",
        "Set State Driven Camera State", 
        "")]
    [AddComponentMenu("")]
    public class SetStateDrivenCameraState : Command
    {
        [SerializeField] private CinemachineStateDrivenCamera stateDrivenCamera;
        [Space]
        [SerializeField] private StringData animatorState;
        [SerializeField] private bool waitUntilComplete;
        
        public override void OnEnter()
        {
            if (stateDrivenCamera == null || stateDrivenCamera.m_AnimatedTarget == null ||
                string.IsNullOrWhiteSpace(animatorState))
            {
                Debug.LogWarning("Not all requirements met to execute SetStateDrivenCameraState, skipping. See the Fungus command's summary for more info.");
                Continue();
                return;
            }

            stateDrivenCamera.m_AnimatedTarget.Play(animatorState);
            if (waitUntilComplete)
            {
                StartCoroutine(WaitForBlendingComplete());
            }
            else
            {
                Continue();
            }
        }

        private IEnumerator WaitForBlendingComplete()
        {
            yield return new WaitUntil(() => stateDrivenCamera.IsBlending);
            yield return new WaitWhile(() => stateDrivenCamera.IsBlending);
            Continue();
        }

        public override string GetSummary()
        {
            if (stateDrivenCamera == null)
                return "Error: No StateDrivenCamera specified";
            if (stateDrivenCamera.m_AnimatedTarget == null)
                return "Error: No AnimatedTarget specified on StateDrivenCamera";
            if (string.IsNullOrWhiteSpace(animatorState))
                return "Error: No AnimatorState specified";

            return animatorState.Value;
        }

        public override Color GetButtonColor()
        {
            return new Color32(216, 228, 170, 255);
        }
    }
}