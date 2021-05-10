using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   float _baseSpeed = 10.0f;
   float _gravidade = 10.0f;

   CharacterController characterController;
   CapsuleCollider playerCol;
   float originalHeight;
   public float reducedHeight;
   GameManager gm;

   //Referência usada para a câmera filha do jogador
   GameObject playerCamera;
   //Utilizada para poder travar a rotação no angulo que quisermos.
   float cameraRotation;
   private float verticalVelocity;
   private float jumpForce = 1.0f;

   public AudioSource OpenDoor;

   void Start()
   {
       gm = GameManager.GetInstance();
       characterController = GetComponent<CharacterController>();
       playerCamera = GameObject.Find("Main Camera");
       cameraRotation = 0.0f;
       playerCol = GetComponent<CapsuleCollider>();
       originalHeight = characterController.height;
       reducedHeight = originalHeight / 4; 

       
   }

   void Update()
   {    
       if (gm.gameState != GameManager.GameState.GAME && gm.gameState != GameManager.GameState.PAUSE) return;
       
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        //Verificando se é preciso aplicar a gravidade
        if (characterController.isGrounded){
            verticalVelocity = -_gravidade * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space)){
                verticalVelocity = jumpForce;
            }
            else if (Input.GetKeyDown(KeyCode.LeftControl)){
                Crouch();
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl)){
                ResetCrouch();
            }
        }
        else {
            verticalVelocity -= _gravidade * Time.deltaTime;
        }
        
        
        Vector3 direction = transform.right * x + transform.up * verticalVelocity + transform.forward * z;

        characterController.Move(direction * _baseSpeed * Time.deltaTime);

            //Tratando movimentação do mouse
        float mouse_dX = Input.GetAxis("Mouse X");
        float mouse_dY = Input.GetAxis("Mouse Y");
        
            //Tratando a rotação da câmera
        cameraRotation += mouse_dY;
        Mathf.Clamp(cameraRotation, -75.0f, 75.0f);
        
        characterController.Move(direction * _baseSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, mouse_dX);


        playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation, 0.0f, 0.0f);

        if ( gm.AudioDoor){
            OpenDoor.mute = false;
            OpenDoor.Play();
            gm.AudioDoor = false;
        }

        if(Input.GetKeyDown(KeyCode.Escape) && gm.gameState == GameManager.GameState.GAME) {
                gm.ChangeState(GameManager.GameState.PAUSE);
        }
        

   }

   void LateUpdate()

    {
    RaycastHit hit;
    Debug.DrawRay(playerCamera.transform.position, transform.forward*10.0f, Color.magenta);

    Vector3 rayOrigin = playerCamera.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f,0.5f,0f));

	//if raycast hits a collider on the rayLayerMask
	if (Physics.Raycast(rayOrigin,playerCamera.transform.forward, out hit,2.0f))
	{
        if (hit.collider.name == "Notepad"){
            if (Input.GetKeyUp(KeyCode.E))
					{
						gm.ChangeState(GameManager.GameState.LIVRO1);
					}
        }
        if (hit.collider.name == "Notepad2"){
            if (Input.GetKeyUp(KeyCode.E))
					{
						gm.ChangeState(GameManager.GameState.LIVRO2);
					}
        }
        if (hit.collider.name == "Fechadura"){
            if (Input.GetKeyUp(KeyCode.E))
					{
						gm.ChangeState(GameManager.GameState.FECHADURA);
					}
        }
        if (hit.collider.name == "key_silver"){
            if (Input.GetKeyUp(KeyCode.G))
					{
						gm.ChaveQuarto = true;
                        Destroy(hit.transform.gameObject);
					}
        }
        if (hit.collider.name == "key_silver2"){
            if (Input.GetKeyUp(KeyCode.G))
					{
						gm.saida = true;
                        Destroy(hit.transform.gameObject);
					}
        }
    }
    }


    // private void setupGui()
	// {
	// 	guiStyle = new GUIStyle();
	// 	guiStyle.fontSize = 16;
	// 	guiStyle.fontStyle = FontStyle.Bold;
	// 	guiStyle.normal.textColor = Color.white;
	// 	msg = "Press E to Open";
	// }


    void Crouch(){
        characterController.height = reducedHeight;
        playerCol.height = reducedHeight;
    }

    void ResetCrouch(){
        characterController.height = originalHeight;
        playerCol.height = originalHeight;
    }
}