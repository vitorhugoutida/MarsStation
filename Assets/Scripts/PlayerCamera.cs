using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float mouseSensitivy = 100f;

    // Faz com que possa rotacionar o corpo do player junto com a c�mera
    public Transform playerbody;

    private float rotation = 0f;

    void Start()
    {
        // Trava o cursor na tela
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Salva o movimento do mouse * sensibilidade
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivy * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivy * Time.deltaTime;

        // Inverte a rota��o vertical
        rotation -= mouseY;

        // Limita a rota��o vertical para o player nao conseguir virar a cabe�a em 360 graus
        rotation = Mathf.Clamp(rotation, -90f, 90f);

        // Aplica a rota��o a c�mera
        transform.localRotation = Quaternion.Euler(rotation, 0f, 0f);

        // Rotaciona o corpo do jogador conforme a movimenta��o horizontal da c�mera
        playerbody.Rotate(Vector3.up * mouseX);
    }
}
