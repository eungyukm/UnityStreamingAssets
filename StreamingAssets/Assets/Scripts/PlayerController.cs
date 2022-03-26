using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private float horizontal;
    [SerializeField] private float vertical;

    [SerializeField] private CharacterController characterController;

    [SerializeField] private float speed = 4f;
    [SerializeField] private float rotationSpeed = 30f;

    private void Awake()
    {
        if(FindObjectsOfType<PlayerController>().Length != 1)
        {
            Destroy(gameObject);
        }

        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Vertical");
        vertical = Input.GetAxis("Horizontal");

        PlayerMove();
    }

    private void PlayerMove()
    {
        animator.SetFloat("Speed", horizontal);

        Vector3 move = gameObject.transform.forward * horizontal * speed;
        characterController.Move(move * Time.deltaTime);
        animator.SetFloat("Direction", vertical);
        Quaternion quaternion = Quaternion.Euler(0, 15 * rotationSpeed * vertical * Time.deltaTime, 0);
        transform.rotation = transform.rotation * quaternion;
    }
}
