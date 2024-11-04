using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    [SerializeField]
    protected float moveSpeed;

    [SerializeField]
    protected float dashSpeed;
    [SerializeField]
    protected float dashLength = .5f;
    [SerializeField]
    protected float dashCooldown = 1f;

    private Rigidbody2D rb2d;

    private Vector2 moveInput;

    private float activeMoveSpeed;
    private float dashCounter;
    private float dashCooldownCounter;

    private WeaponStats weaponStats;

    private bool dashing;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        activeMoveSpeed = moveSpeed;
        rb2d = GetComponent<Rigidbody2D>();

        weaponStats = transform.Find("Aim").GetChild(0).GetComponent<WeaponStats>();

        animator = GetComponent<Animator>();
    }

    public void setMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
        activeMoveSpeed = moveSpeed;
    }

    public float getMoveSpeed()
    {
        return this.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Health>().isDead())
        {
            rb2d.velocity = Vector2.zero;
            return;
        }

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        rb2d.velocity = moveInput * activeMoveSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCooldownCounter <= 0 && dashCounter <= 0 && !weaponStats.Reloading())
            {
                animator.SetTrigger("Sprint");

                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;

                dashing = true;
            }
        }

        if (weaponStats.Reloading())
        {
            activeMoveSpeed = moveSpeed - 1.5f;
        }
        if (!dashing && !weaponStats.Reloading())
        {
            activeMoveSpeed = moveSpeed;
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCooldownCounter = dashCooldown;
                dashing = false;
            }
        }

        if (dashCooldownCounter > 0)
        {
            dashCooldownCounter -= Time.deltaTime;
        }
    }
}
