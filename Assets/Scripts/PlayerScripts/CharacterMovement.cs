using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    [Header("Movement Attribute")]
    [SerializeField]private float speed = 5;

    [Header("Reference")]
    [SerializeField] private CharacterController controller;
    
    [Header("Body parts reference")]
    
    [SerializeField]private GameObject m_inpul_left;


    [Header("Platform")]
    public bool PC = true;

    private Vector3 _move;
    private MobileInputController _mobileInputController;
	void Start () {
        controller = GetComponent<CharacterController>();
        _mobileInputController = m_inpul_left.GetComponent<MobileInputController>();
	}
	
	void Update () {
		
        if (!PC)
        {
	        _move = Vector3.forward * (_mobileInputController.Vertical * Time.deltaTime * speed)
	                 + Vector3.right * (_mobileInputController.Horizontal * Time.deltaTime * speed);
	        controller.Move(_move);
        }
        else
        {
	        _move = Vector3.forward * (Input.GetAxisRaw("Vertical") * Time.deltaTime * speed) +
		        Vector3.right * (Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed);
	        controller.Move(_move);
        }
	}

	public bool isMove()
	{
		return _move != Vector3.zero;
	}

	public Vector3 GetMove()
	{
		return _move;
	}

}
