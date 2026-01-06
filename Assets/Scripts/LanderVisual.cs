using System;
using UnityEngine;

public class LanderVisual : MonoBehaviour
{
    [SerializeField] private ParticleSystem lThrusterParticleSystem;
    [SerializeField] private ParticleSystem UThrusterParticleSystem;
    [SerializeField] private ParticleSystem rThrusterParticleSystem;
    [SerializeField] private GameObject landerExpotionVFX;

    private Lander lander;

    private void Awake()
    {
        lander = GetComponent<Lander>();
        lander.OnUpForce += Lander_UpForce;
        lander.OnLForce += Lander_LForce;
        lander.OnRForce += Lander_RForce;
        lander.OnBeforeForce += Lander_OnBeforeForce;

        SetEnabledThusterParticle(UThrusterParticleSystem, false);
        SetEnabledThusterParticle(lThrusterParticleSystem, false);
        SetEnabledThusterParticle(rThrusterParticleSystem, false);
    }

    private void Start()
    {
        lander.OnLanded += Lander_OnLanded;
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEvenArgs e)
    {
        switch (e._landingType)
        {
            case Lander.LandingType.TooSteepAngle:
            case Lander.LandingType.TooFastLanding:
            case Lander.LandingType.NotOke:
                Instantiate(landerExpotionVFX, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
                break;
        }
    }

    private void Lander_OnBeforeForce(object sender, EventArgs e)
    {
        SetEnabledThusterParticle(UThrusterParticleSystem, false);
        SetEnabledThusterParticle(lThrusterParticleSystem, false);
        SetEnabledThusterParticle(rThrusterParticleSystem, false);
    }

    private void Lander_UpForce(object sender, EventArgs e)
    {
        SetEnabledThusterParticle(UThrusterParticleSystem, true);
        SetEnabledThusterParticle(lThrusterParticleSystem, true);
        SetEnabledThusterParticle(rThrusterParticleSystem, true);
    }

    private void Lander_LForce(object sender, EventArgs e)
    {
        SetEnabledThusterParticle(lThrusterParticleSystem, true);
    }

    private void Lander_RForce(object sender, EventArgs e)
    {
        SetEnabledThusterParticle(rThrusterParticleSystem, true);
    }

    private void SetEnabledThusterParticle(ParticleSystem particleSystem, bool enabled)
    {
        ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
        emissionModule.enabled = enabled;
    }
}
