using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LaserLogic : MonoBehaviour
{
    public ActionBasedController controller = null; // glisser le controler ici
    [SerializeField] private LineRenderer _beam;
    [SerializeField] private Transform _muzzlePoint;
    [SerializeField] private float _maxLength;

    [SerializeField] private RaycastAim raycastAim;

    /*[SerializeField] private Camera mainCam;
    private Vector3 mousePos;*/

    private float rotationX;
    private float rotationY;

    [SerializeField] private ParticleSystem _spiraleParticles;
    [SerializeField] private ParticleSystem _hitParticles;

    private Vector3 spiralLastPosition;

    private float triggerButtonThreshold = 0.9f;
    private bool isTriggered = false;

    private bool activation = false;

    private void Awake()
    {
        _beam.enabled = false;
        activation = false;
    }

    private void Activate()
    {
        _beam.enabled = true;
        _spiraleParticles.Play();
        _hitParticles.Play();

        var main = _spiraleParticles.main;
        main.simulationSpace = ParticleSystemSimulationSpace.Local;

        _spiraleParticles.transform.localScale = new Vector3(1, 1, 1);

        activation = true;

        //Debug.Log("Activation");
        //Debug.Log(_spiraleParticles.isPlaying);
        //Debug.Log("Nmb de particules " + _hitParticles.particleCount);
    }

    private void Deactivate()
    {
        _beam.enabled = false;

        _beam.SetPosition(0, _muzzlePoint.position);
        _beam.SetPosition(1, _muzzlePoint.position);

        _spiraleParticles.Pause();
        _hitParticles.Stop();

        var main = _spiraleParticles.main;
      
        //main.simulationSpace = ParticleSystemSimulationSpace.World;

        _spiraleParticles.transform.LookAt(spiralLastPosition);

        _spiraleParticles.transform.localScale = new Vector3(0, 0, 0);

        activation = false;

        //Debug.Log("Deactivation");
    }

    private void Update()
    {
        //Activate();

        float triggerValue = controller.activateActionValue.action.ReadValue<float>();

        if ((triggerValue > triggerButtonThreshold) && !isTriggered)
        {
            Activate();
            //Debug.Log("triggerButton");
            isTriggered = true;
        }
        else if ((triggerValue <= triggerButtonThreshold) && isTriggered)
        {
            Deactivate();
            //Debug.Log("triggerButtonStop");
            isTriggered = false;
        }

        //Debug.Log(isTriggered);
    }


    private void FixedUpdate()
    {
        Ray ray = new Ray(_muzzlePoint.position, raycastAim.hitPoint);
        bool cast = Physics.Raycast(ray, out RaycastHit hit, _maxLength);

        Vector3 hitPosition = cast ? hit.point : _muzzlePoint.position + _muzzlePoint.forward * _maxLength;

        _beam.SetPosition(0, _muzzlePoint.position);
        _beam.SetPosition(1, raycastAim.hitPoint);

        _hitParticles.transform.position = raycastAim.hitPoint;

        if (activation)
        {
            raycastAim.GetHitPoint();
        }
        else if (!activation)
        {
            return;
        }

        Quaternion rotation = Quaternion.LookRotation(_spiraleParticles.transform.position, raycastAim.hitPoint);
        _spiraleParticles.transform.LookAt(raycastAim.hitPoint);

        spiralLastPosition = raycastAim.hitPoint;
    }
}
