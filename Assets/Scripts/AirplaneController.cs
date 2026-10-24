using UnityEngine;
using UnityEngine.InputSystem;

public class AirplaneController : MonoBehaviour
{
    private Rigidbody rb; // 儲存剛體
    private float movementX;
    private float movementY;

    public float speed = 100f;
    public AudioClip crashSound;     // 撞到障礙物音效，在Inspector面板提供音效插槽
    public AudioClip passSound;      // 順利通過音效
    private AudioSource audioSource; // 喇叭
    private bool Crashed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnMove(InputValue movementValue) // 用方向鍵控制前後左右
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        if (Crashed) return; // 如果撞到，不加任何力

        Vector3 movement = new Vector3(movementX, 0.0f, movementY); // 固定高度
        rb.AddForce(movement * speed); // 施加力
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 當撞到的物體 Tag 是 Obstacle
        if (collision.gameObject.CompareTag("Obstacle") && Crashed)
        {

            // 播放撞擊音效
            if (audioSource != null && crashSound != null)
            {
                audioSource.PlayOneShot(crashSound);
            }
            Vector3 pushBack = -transform.forward * 2f; // 撞到向後彈2f
            rb.AddForce(pushBack, ForceMode.Impulse); // ForceMode.Impulse瞬間向後彈
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 當穿過的區域 Tag 是 PassTrigger
        if (other.CompareTag("PassTrigger") && !Crashed)
        {

            // 播放通關音效
            if (audioSource != null && passSound != null)
            {
                audioSource.PlayOneShot(passSound);
            }
        }
    }
}