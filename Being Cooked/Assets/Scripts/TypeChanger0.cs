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

            ing.characterMove.enabled = false;
            StartCoroutine(StartChanger(ing));

        }
    }
    public void elapseIngredient(Ingredient ing)
    {
        if (!ing.isProcessed)//가공된 재료 생성, 타겟. 기존 재료 삭제
        {
            string name = ing.foodName;
            string nameIngF = "Prefabs/Ingredient/" + name + "1";


            Debug.Log("재료를 가공합니다. 가공 대상 : " + name + "가공 완료 대상 : " + nameIngF);

            Destroy(ing.gameObject);

            if (Resources.Load(nameIngF) != null)
            {
                var a = Instantiate(Resources.Load<GameObject>(nameIngF), sponPos.position, Quaternion.identity).GetComponent<Ingredient>();
                StartCoroutine(makeFollower(nameIngF, a));
                a.EnterControl(); //요거 오브젝트 똑바로 못 찾음;; 해결해야해
               
                //GameManager.inst.cameraMove.MoveTo(GameObject.Find(nameIngF).transform, ing.size);
            }
            else
                Debug.Log("가공 재료 프리팹 없음!");
            //코루틴으로 추가 팔로워들 생성
            
            //Instantiate<other.gameObject. name> 재료 생성 + 부가재료 코루틴 만들기
        }
        else //기존 재료 이동, 다시 타겟
        {
            /*other.GetComponent<Ingredient>().EnterControl();
            GameManager.inst.cameraMove.isTargeting = true;*/
            Debug.Log("가공된 재료를 보존합니다.");
            ing.gameObject.transform.position = sponPos.position;
            GameManager.inst.cameraMove.MoveTo(ing.transform, ing.size);
            ing.characterMove.enabled = true;
        }
    }

    IEnumerator StartChanger (Ingredient ing)
    {
        GameManager.inst.cameraMove.MoveTo(camPos, 0, gameObject.transform.parent.gameObject.transform);
        yield return new WaitForSeconds(1);
        GameManager.inst.cameraFollow.StopCameraForChanger(2, ing, false);
    }
    IEnumerator makeFollower (string name, Ingredient ing)
    {
        for(int i = 0; i  < ing.followerCount; i++)
        {
            var IngF = Instantiate(Resources.Load<GameObject>(name+"f"), sponPos.position, Quaternion.identity).GetComponent<Follower>();
            IngF.cMove = ing.characterMove;

            yield return new WaitForSeconds(1);
        }
    }
}
