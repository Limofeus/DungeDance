using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public Animator OldLocationAnim;
    public Animator NewLocationAnim;
    public GameObject BGPrefab;
    [SerializeField] private ParallaxLayer[] parallaxLayers;
    private bool moveAsLinked = false;
    private Transform moveAsLinkedTarget;
    private void Update()
    {
        if (moveAsLinked && parallaxLayers.Length >= 0)
        {
            foreach (var layer in parallaxLayers)
            {
                layer.transform.position = new Vector3((moveAsLinkedTarget.position.x * layer.parallaxFactor) - layer.currOffset, layer.transform.position.y, layer.transform.position.z);
            }
        }
    }
    public void SkipStart()
    {
        OldLocationAnim.SetTrigger("Skipstart");
    }
    public void Move()
    {
        if(parallaxLayers != null && parallaxLayers.Length > 0)
        {
            moveAsLinkedTarget = OldLocationAnim.transform;
            foreach (var layer in parallaxLayers)
            {
                layer.currOffset = moveAsLinkedTarget.position.x - layer.transform.position.x;
            }
            moveAsLinked = true;
        }

        OldLocationAnim.SetTrigger("Disap");
        NewLocationAnim = Instantiate(BGPrefab, transform).GetComponent<Animator>();
        StartCoroutine(AfterAnimation());
    }

    IEnumerator AfterAnimation()
    {
        yield return new WaitForSeconds(2);
        moveAsLinked = false;

        Destroy(OldLocationAnim.gameObject);
        OldLocationAnim = NewLocationAnim;
    }
}
