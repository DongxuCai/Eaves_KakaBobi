using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpirit : MonoBehaviour
{
    public GameObject element_Prefab;
    public Animator animator;

    public void GetElement() { StartCoroutine(IE_GetElement()); }
    public IEnumerator IE_GetElement()
    {
        animator.SetBool("Vanish", true);
        yield return new WaitForSeconds(1f);

        GameObject element_Instance = Instantiate(element_Prefab, SpiritSoilManager.Instance.elementContainer);
        element_Instance.transform.position = transform.position;
    }
    public void Vanish() { Destroy(gameObject); }
}
