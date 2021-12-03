using Cinemachine;
using UnityEngine;

namespace Fungus
{
    [CommandInfo("Camera/Cinemachine",
        "Set Cinemachine Brain Default Blend", 
        "")]
    [AddComponentMenu("")]
    public class SetCinemachineBrainDefaultBlend : Command
    {
        [SerializeField] private CinemachineBrain brain;
        [SerializeField] private CinemachineBlendDefinition.Style style = CinemachineBlendDefinition.Style.EaseInOut;
        [Tooltip("Curve MUST be normalized, i.e. time range [0...1], value range [0...1].")]
        [SerializeField] private AnimationCurve customCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        [SerializeField] private FloatData duration = new(2f);
        
        public override void OnEnter()
        {
            if (brain == null)
            {
                Debug.LogWarning("Not all requirements met to execute SetCinemachineBrainDefaultBlend, skipping. See the Fungus command's summary for more info.");
                Continue();
                return;
            }

            brain.m_DefaultBlend = new CinemachineBlendDefinition(style, duration);
            if (style == CinemachineBlendDefinition.Style.Custom)
                brain.m_DefaultBlend.m_CustomCurve = customCurve;
            
            Continue();
        }

        public override string GetSummary()
        {
            if (brain == null)
                return "Error: No Brain specified";

            return $"{style} for {duration.Value} seconds(s)";
        }

        public override Color GetButtonColor()
        {
            return new Color32(216, 228, 170, 255);
        }

        public override bool HasReference(Variable variable)
        {
            if (duration.floatRef == variable)
                return true;

            return false;
        }
    }
}