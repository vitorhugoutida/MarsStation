using System.Collections;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    private Rigidbody rb;
    private Transform holdPoint;

    private bool isHold = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private AreaDetector currentArea;

    public float moveSpeed = 10f;
    public float snapSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHold && holdPoint != null)
        {
            // Lerp -> Adiciona uma suaviza��o na anima��o, interpola o objeto entre a PosA e PosB
            transform.position = Vector3.Lerp(transform.position, holdPoint.position, Time.deltaTime * moveSpeed);
        }
    }

    public void Grab(Transform newHoldPoint)
    {
        isHold = true;
        rb.useGravity = false;
        rb.isKinematic = true;

        holdPoint = newHoldPoint;
    }

    // Fun��o que auxilia o grabSystem para soltar o objeto.
    public void Release()
    {
        isHold = false;
        rb.useGravity = true;
        rb.isKinematic = false;

        holdPoint = null;

        if(currentArea != null && currentArea.IsObjectInside(gameObject))
        {
            //Auxilia a anima��o de encaixe do objeto no local correto 
            StartCoroutine(SnapToArea(currentArea.transform));
        }
        else
        {
            // Se o objeto n�o estiver na �rea avlida, volta para a posi��o/rota��o
            StartCoroutine(ResetPosition());
        }
    }

    private IEnumerator SnapToArea(Transform snapTarget)
    {
        float tempo = 0f;
        Vector3 startPosition = transform.position;
        Quaternion currentRotation = transform.rotation;

        while (tempo < 1f)
        {
            tempo += Time.deltaTime * snapSpeed;

            transform.position = Vector3.Lerp(startPosition, snapTarget.position, tempo);
            transform.rotation = currentRotation;

            yield return null;
        }

        transform.position = snapTarget.position;
        transform.rotation = currentRotation;

        rb.useGravity = false;
        rb.isKinematic = true;
    }

    private IEnumerator ResetPosition()
    {
        float t = 0f;
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        while (t < 1f)
        {
            t += Time.deltaTime * snapSpeed;

            transform.position = Vector3.Lerp(startPosition, originalPosition, t);
            transform.rotation = Quaternion.Lerp(startRotation, originalRotation, t);

            yield return null;
        }

        transform.position = originalPosition;
        transform.rotation = originalRotation;

        rb.useGravity = false;
        rb.isKinematic = true;
    }

    public void NotifyEnterArea (AreaDetector area)
    {
        currentArea = area;
    }

    public void NotifyExitArea (AreaDetector area)
    {
        if (currentArea == area)
        {
            currentArea = null;
        }
    }
}
