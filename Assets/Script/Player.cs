using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    private Animator animator = null;
    [SerializeField] private SceneSwitter scene;
    [SerializeField] private Transform player_pos;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();  // rigidbody‚ğæ“¾
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.zero;

        if (player_pos.position.x >= -4.0f)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector3(-10.0f, 0.0f, 0.0f); // å€¤ã‚’è¨­å®E

                Debug.Log("ï¿½ï¿½ï¿½@ï¿½ï¿½ï¿½ï¿½ï¿½Í‚ï¿½ï¿½Ä‚ï¿½");
                rb.velocity = new Vector3(-10.0f, 0.0f, 0.0f); // 
                                                               //transform.position -= speed * transform.right * Time.deltaTime;
            }
        }
        if (player_pos.position.x <= 4.0f)
        {

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector3(10.0f, 0.0f, 0.0f); // å€¤ã‚’è¨­å®E

                Debug.Log("ï¿½Eï¿½@ï¿½ï¿½ï¿½ï¿½ï¿½Í‚ï¿½ï¿½Ä‚ï¿½");
                rb.velocity = new Vector3(10.0f, 0.0f, 0.0f); // 
                                                              //transform.position += speed * transform.right * Time.deltaTime;
            }
            //rb.velocity.x = 3.0f;
        }

        Debug.Log("velo:" + player_pos.position.x);

        // ƒAƒjƒ[ƒVƒ‡ƒ“—pƒtƒ‰ƒOŠÇ—
        if (scene.IsMode == true)
        {
            animator.SetBool("IsActive", true);
        }
        else
        {
            animator.SetBool("IsActive", false);
        }
<<<<<<< Updated upstream
=======


>>>>>>> Stashed changes
    }

    private void OnTriggerEnter(Collider collision)
    {
        // ƒ^ƒO‚ªEnemyBullet‚ÌƒIƒuƒWƒFƒNƒg‚ª“–‚½‚Á‚½‚É{ }
        // “à‚Ìˆ—‚ªs‚í‚ê‚é
        if (collision.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene("GameOver");  // ƒQ[ƒ€ƒNƒŠƒA‰æ–Ê‚ÉˆÚs‚·‚é
        }
    }
}
