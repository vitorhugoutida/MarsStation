using UnityEngine;

public class AreaDetector : MonoBehaviour
{
    public float snapDistance = 1f;

    public Transform snapRotationReference;

    public bool IsObjectInside(GameObject grababble)
    {
        // Salva a distância em que o player pode agarrar o objeto
        float distance = Vector3.Distance(grababble.transform.position, transform.position);

        return distance <= snapDistance; 
    }

    // O método OnTriggerEnter serve para quando temos um collider
    // com isTrigger ativado, que detecta outro Collider na área.
    private void OnTriggerEnter(Collider other)
    {
        //Verifica se o objeto que entrou na área possui a tag indicada
        if (other.CompareTag("Grababble"))
        {
            GrabbableObject grabbable = other.GetComponent<GrabbableObject>();

            if (grabbable != null)
            {
                grabbable.NotifyEnterArea(this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Verifica se o objeto que saiu aa área possui a tag indicada
        if (other.CompareTag("Grababble"))
        {
            GrabbableObject grabbable = other.GetComponent<GrabbableObject>();

            if (grabbable != null)
            {
                grabbable.NotifyEnterArea(this);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
