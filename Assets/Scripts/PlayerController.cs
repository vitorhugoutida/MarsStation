using Unity.VisualScripting;
using UnityEngine;

// Faz com que seja OBRIGAT�RIO tem um rigidbody no Objeto
[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    //Define a velocidade do player
    public float moveSpeed = 5f;

    private Rigidbody rb;

    // Armazena a (X, Y, Z);
    private Vector3 moveDirection;
    
    void Start()
    {
        // Pega o componente do rigidbody que est� atrelado ao Objeto
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Pega a movimenta��o horizontal(A, D) do jogador
        float moveY = Input.GetAxis("Horizontal");

        // Pega a movimenta��o vertical (W, S) do jogador
        float moveX = Input.GetAxis("Vertical");

        // Calcula a movimenta��o do player
        moveDirection = transform.right * moveX + transform.forward * moveY;
    }

    void FixedUpdate()
    {
        // Realmente move o jogador fisicamente
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);   
    }
}
