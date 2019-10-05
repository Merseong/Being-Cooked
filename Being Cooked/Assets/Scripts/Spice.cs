using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spice : Ingredient
{
    [Header("Spice only Settings")]
    public Transform lidTr;
    public ParticleSystem particle;

    private Rigidbody rb;
    private Vector3 originPos;
    private Quaternion originRot;

    protected override void AfterStart()
    {
        rb = GetComponent<Rigidbody>();
        originPos = transform.position;
        originRot = transform.rotation;
    }

    public override void AfterPot()
    {
        throw new System.NotImplementedException();
    }

    public override void IntoPot()
    {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        
        GameManager.inst.cameraFollow.StopAndResetCamera(3);
        StartCoroutine(OpenAndPourCoroutine());
    }
    
    IEnumerator OpenAndPourCoroutine()
    {
        float timer = 0;
        while (timer < 2f)
        {
            // open lid, enable particle
            transform.position = Vector3.Lerp(transform.position, GameManager.inst.pot.spicePos.position, timer * 0.5f);
            if (lidTr != null)
            {
                lidTr.localPosition = Vector3.Lerp(Vector3.zero, new Vector3(0.05f, 0, 0.1f), timer * 0.5f);
                lidTr.localRotation = Quaternion.Slerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 90, 0), timer * 0.5f);
            }
            if (particle != null) particle.Play();
            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        while (timer < 3f)
        {
            // off particle, back to original pos, close lid
            if (lidTr != null)
            {
                lidTr.localPosition = Vector3.Lerp(lidTr.localPosition, Vector3.zero, timer - 2);
                lidTr.localRotation = Quaternion.Slerp(lidTr.localRotation, Quaternion.Euler(0, 0, 0), timer - 2);
            }
            transform.position = Vector3.Lerp(transform.position, originPos, timer - 2);
            transform.rotation = Quaternion.Slerp(transform.rotation, originRot, timer - 2);
            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        rb.isKinematic = false;
        characterMove.isControlled = false;
        characterMove.enabled = false;
    }
}
