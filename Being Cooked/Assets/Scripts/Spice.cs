using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spice : Ingredient
{
    [Header("Spice only Settings")]
    public GameObject emptySpice;
    public Transform lidTr;
    public ParticleSystem particle;

    private Rigidbody rb;
    private Vector3 originPos;
    private Quaternion originRot;

    protected override void AfterStart()
    {
        rb = GetComponent<Rigidbody>();
        type = IngredientType.Spice;
        originPos = transform.position;
        originRot = transform.rotation;
    }

    public override void IntoPot()
    {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        
        GameManager.inst.cameraFollow.StopAndResetCamera(3);
        StartCoroutine(OpenAndPourCoroutine());
    }

    public GameObject GetSpiceObj()
    {
        var obj = Instantiate(emptySpice, GameManager.inst.pot.transform);
        obj.transform.position = GameManager.inst.pot.spicePos.position;
        var spiceInst = obj.GetComponent<Ingredient>();
        System.Type type = spiceInst.GetType();
        // Copied fields can be restricted with BindingFlags
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(spiceInst, field.GetValue(this as Ingredient));
        }
        UIManager.inst.GenerateFoodBar(spiceInst);

        return obj;
    }
    
    IEnumerator OpenAndPourCoroutine()
    {
        float timer = 0;
        if (particle != null) particle.Play();
        while (timer < 2f)
        {
            // open lid, enable particle
            transform.position = Vector3.Lerp(transform.position, GameManager.inst.pot.spicePos.position, timer * 0.5f);
            if (lidTr != null)
            {
                lidTr.localPosition = Vector3.Lerp(Vector3.zero, new Vector3(0.05f, 0, 0.1f), Mathf.Min(timer, 1));
                lidTr.localRotation = Quaternion.Slerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 90, 0), Mathf.Min(timer, 1));
            }
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
