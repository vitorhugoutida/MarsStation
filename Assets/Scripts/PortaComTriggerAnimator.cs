
//using UnityEngine;

//public class PortaComTriggerAnimator : MonoBehaviour
//{
//    public Animator animatorPorta;
//    public string nomeTrigger = "Abrir";
//    public float tempoDeEspera = 10f;

//    private bool jaAtivou = false;

//    private void OnTriggerEnter(Collider other)
//    {
//        if (!jaAtivou && other.CompareTag("Player"))
//        {
//            jaAtivou = true;
//            Invoke("AtivarPorta", tempoDeEspera);
//        }
//    }

//    private void AtivarPorta()
//    {
//        if (animatorPorta != null)
//        {
//            animatorPorta.SetTrigger(nomeTrigger);
//        }
//    }
//}

using UnityEngine;

public class PortaComTriggerAnimator : MonoBehaviour
{
    public Animator AnimatorPorta;
    public string NomeTrigger = "Abrir";
    public float TempoDeEspera = 2f;
    private bool podeAbrir = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && podeAbrir)
        {
            podeAbrir = false;
            Invoke(nameof(AtivarPorta), TempoDeEspera);
        }
    }

    void AtivarPorta()
    {
        if (AnimatorPorta != null)
        {
            AnimatorPorta.SetTrigger(NomeTrigger);
        }

        // Espera um tempo e permite reabrir depois
        Invoke(nameof(ResetarPorta), 1f); // ou o tempo da animação de fechar
    }

    void ResetarPorta()
    {
        podeAbrir = true;
    }
}
