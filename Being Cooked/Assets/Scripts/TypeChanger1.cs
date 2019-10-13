using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeChanger1 : MonoBehaviour
{
    //가공 -> 노가공 방향
    public Transform camPos;
    [HideInInspector]
    public Ingredient inIngredient; //들어온 재료

    public Transform sponPos;

    private float lastIn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient") && lastIn + 3.5f < Time.time)
        {
            lastIn = Time.time;
            var ing = other.GetComponent<Ingredient>();

            /*other.GetComponent<Ingredient>().ExitControl();
            GameManager.inst.cameraMove.isTargeting = false;*/

            ing.characterMove.enabled = false;
            StartCoroutine(StartChanger(ing, this));

        }
    }
    public void elapseIngredient(Ingredient ing)
    {
        if (ing.isProcessed)//가공되지 않은 재료 생성, 타겟. 기존 재료 삭제
        {
            string name = ing.foodName;
            string nameIng = "Prefabs/Ingredient/" + name + "0";

            Debug.Log("재료를 재조립합니다.");

            for (int i = 0; i < ing.followerCount; i++)
            {
                Destroy(ing.followers[i].gameObject);
            }
            Destroy(ing.gameObject);

            if (Resources.Load(nameIng) != null)
            {
                var a = Instantiate(Resources.Load<GameObject>(nameIng), sponPos.position, Quaternion.identity).GetComponent<Ingredient>();
                a.EnterControl();
                GameManager.inst.cameraMove.beforeHit = a.transform;
                
                //GameObject.Find(nameIng).GetComponent<Ingredient>().EnterControl(); //요거 오브젝트 똑바로 못 찾음;; 해결해야해

                //GameManager.inst.cameraMove.MoveTo(GameObject.Find(nameIngF).transform, ing.size);
            }
            else
                Debug.Log("가공 재료 프리팹 없음!");
        }
        else //기존 재료 이동, 다시 타겟
        {
            Debug.Log("재료를 보존합니다.");
            /*other.GetComponent<Ingredient>().EnterControl();
            GameManager.inst.cameraMove.isTargeting = true;*/
            ing.gameObject.transform.position = sponPos.position;
            GameManager.inst.cameraMove.MoveTo(ing.transform, ing.size);
            ing.characterMove.enabled = true;
        }
    }

    IEnumerator StartChanger(Ingredient ing, TypeChanger1 ch)
    {
        GameManager.inst.cameraMove.MoveTo(camPos, 0, gameObject.transform.parent.gameObject.transform);
        yield return new WaitForSeconds(1);
        GameManager.inst.cameraFollow.StopCameraForChanger(2, ing, true, ch);
    }
}
