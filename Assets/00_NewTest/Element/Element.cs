using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    public GameObject element;
    public RectTransform label;
    public Animator animator;

    public void MoveToTripod() { StartCoroutine(IE_MoveToTripod()); }

    public IEnumerator IE_MoveToTripod()
    {
        while (Vector3.Distance(element.transform.position, Tripod.Instance.elementTarget.position) > 0.2f)
        {
            yield return new WaitForEndOfFrame();
            element.transform.position = Vector3.Lerp(element.transform.position, Tripod.Instance.elementTarget.position, 0.2f);
        }
        Collect();
    }
    public void Collect()
    {
        animator.SetBool("Tripod", true);
        label.position = Camera.main.WorldToScreenPoint(Tripod.Instance.elementTarget.position);
    }
    public void Vanish()
    {
        Destroy(gameObject);
    }
}
