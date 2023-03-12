
using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

[Serializable, VolumeComponentMenu("Post-processing/Custom/ToonShading")]
public sealed class ToonShading : CustomPostProcessVolumeComponent, IPostProcessComponent
{
    [SerializeField] private FloatParameter _posterizeAmount = new FloatParameter(1f);
    [SerializeField] private BoolParameter _isEnabled = new BoolParameter(false);

    Material m_Material;

    public bool IsActive() => m_Material != null && _isEnabled.value;

    public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.BeforePostProcess;

    const string kShaderName = "Hidden/Shader/ToonShading";

    public override void Setup()
    {
        if (Shader.Find(kShaderName) != null)
            m_Material = new Material(Shader.Find(kShaderName));
        else
            Debug.LogError(
                $"Unable to find shader '{kShaderName}'. Post Process Volume ToonShading is unable to load.");
    }

    public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
    {
        if (m_Material == null)
            return;

        m_Material.SetFloat("_PosterizeAmount", _posterizeAmount.value);

        m_Material.SetTexture("_InputTexture", source);
        HDUtils.DrawFullScreen(cmd, m_Material, destination);
    }

    public override void Cleanup()
    {
        CoreUtils.Destroy(m_Material);
    }
}