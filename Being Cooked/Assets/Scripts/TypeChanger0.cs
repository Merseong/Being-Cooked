using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeChanger0 : MonoBehaviour
{
    // 노가공 -> 가공

    public Transform camPos; 
    [HideInInspector]
    public Ingredient inIngredient; //들어온 재료

    public Transform sponPos;

    private float lastIn;
    //colider에 들어오면 카메라 이동 후 고정, 재료 변환 && 이동

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient") && lastIn + 3.5f < Time.time)
        {
            lastIn = Time.time;
            var ing = other.GetComponent <Ingredient>();

            /*other.GetComponent<Ingredient>().ExitControl();
            GameManager.inst.cameraMove.isTargeting = false;*/

            StartCoroutine(StartChanger(ing));

        }
    }
    public void elapseIngredient(Ingredient ing)
    {
        if (!ing.isProcessed)//가공된 재료 생성, 타겟. 기존 재료 삭제
        {
            Destroy(ing.gameObject);
            //Instantiate<other.gameObject. name> 재료 생성 + 부가재료 코루틴 만들기
        }
        else //기존 재료 이동, 다시 타겟
        {
            /*other.GetComponent<Ingredient>().EnterControl();
            GameManager.inst.cameraMove.isTargeting = true;*/
            ing.gameObject.transform.position = sponPos.position;
            GameManager.inst.cameraMove.MoveTo(ing.transform, ing.size);
            ing.canControl = true;
        }
    }

    IEnumerator StartChanger (Ingredient ing)
    {
        GameManager.inst.cameraMove.MoveTo(camPos, 0, gameObject.transform.parent.gameObject.transform);
        yield return new WaitForSeconds(2);
        GameManager.inst.cameraFollow.StopCameraForChanger(2, ing);
    }
}
