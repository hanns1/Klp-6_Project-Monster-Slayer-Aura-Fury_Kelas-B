using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    private Vector3 direction;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float damage; // Damage yang diberikan musuh ke player
    [SerializeField] private float health; // Kesehatan musuh
    [SerializeField] private int experienceToGive;
    [SerializeField] private float pushTime;

    private float pushCounter;

    [SerializeField] private GameObject destroyEffect;

    void FixedUpdate()
    {
        // PENTING: Gunakan rb.velocity, bukan rb.linearVelocity
        if (PlayerControler.Instance.gameObject.activeSelf)//!= null && PlayerController.Instance
        {
            // face the player
            if (PlayerControler.Instance.transform.position.x > transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }

            // push back (Jika Anda ingin mengaktifkan push back, uncomment bagian ini)
            if (pushCounter > 0)
            {
                pushCounter -= Time.deltaTime;
                if (moveSpeed > 0)
                {
                    moveSpeed = -moveSpeed;
                }
                if (pushCounter <= 0)
                {
                    moveSpeed = Mathf.Abs(moveSpeed);
                }
            }

            // move towards the player
            direction = (PlayerControler.Instance.transform.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed); // PERBAIKAN DI SINI
        }
        else // Jika player tidak aktif atau tidak ditemukan, musuh diam
        {
            rb.linearVelocity = Vector2.zero; // PERBAIKAN DI SINI
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy collision with Player detected!"); // Untuk debugging

            // Panggil TakeDamage pada PlayerController
            PlayerControler.Instance.TakeDamage(damage);

            // Hapus baris di bawah ini jika Anda TIDAK ingin musuh langsung hancur saat menabrak player
            Destroy(gameObject); 
            Instantiate(destroyEffect, transform.position, transform.rotation); // Ini juga dihapus/dikomentari jika musuh tidak langsung hancur
        }
    }

    public void TakeDamage(float damage)
    {
        //Debug.Log("Enemy took " + damage + " damage. Current health: " + health); // Untuk debugging
        health -= damage; // AKTIFKAN: Pengurangan kesehatan musuh

        // Dikomentari: Bergantung pada 'DamageNumberController', 'PlayerController', 'AudioController'
        // DamageNumberController.Instance.CreateNumber(damage, transform.position);
        
        pushCounter = pushTime; // Untuk fitur pushback, jika diaktifkan
        
        if (health <= 0) // AKTIFKAN: Logika kematian musuh
        {
            Debug.Log("Enemy defeated!"); // Untuk debugging
            Destroy(gameObject); // AKTIFKAN: Hancurkan musuh ketika mati
            Instantiate(destroyEffect, transform.position, transform.rotation); // AKTIFKAN: Munculkan efek hancur
            PlayerControler.Instance.GetExperience(experienceToGive); // Dikomentari jika belum siap
            // AudioController.Instance.PlayModifiedSound(AudioController.Instance.enemyDie); // Dikomentari jika belum siap
        }
    }
}