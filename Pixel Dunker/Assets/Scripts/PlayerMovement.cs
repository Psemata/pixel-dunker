using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    int hp = 3;
    public float runSpeed = 40f;
    private float timeUnconscious = 1f;
    private int nbArmor = 0;
    private bool isUnconscious = false;
    float horizontalMove = 0f;
    bool jump = false;
    bool dashUsed = false;

    public ChargeBar chargeBar;
    public GameObject chargeBarGO;

    public HealthPlayers healthPlayers;
    public Transform ballHolder;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float attackRate = 1f;
    float nextAttackTime = 0f;
    public LayerMask enemyLayers;
    public int team;
    bool hold = false;
    float charge = 0f;
    bool updown = true;
    GameObject go = null;

    private Vector2 orientationInput;
    private bool isCharging = false;
    private Vector2 orientationShoot;
    private Vector2 previousOrientationShoot = Vector2.zero;
    private Vector2 finalOrientationShoot;
    private Vector2 mouseFinalOrientationShoot;
    private const float TIMECOOLDOWN = 0.1f;
    private float cooldown = TIMECOOLDOWN;  
    private float cooldown2 = TIMECOOLDOWN;    
    private float deltaPosition = 0.1f;
    private bool isCenterJoystick = false;

    public GameObject point;
    GameObject[] points;
    public int numberOfPoints;
    public float spaceBetweenPoints;

    //Utils power up
    public int powerUp;

    // Start is called before the first frame update
    void Start()
    {
        points = new GameObject[numberOfPoints];
        for(int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, ballHolder.position, Quaternion.identity);
            DontDestroyOnLoad(points[i]);
            points[i].transform.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    /// <summary>
    /// Function that returns the position of the point
    /// </summary>
    /// <param name="t">The number of the point in the array, can also be interpreted as time</param>
    /// <param name="force">The vector force that we are using</param>
    /// <returns>The position of the point</returns>
    Vector2 PointPosition(float t, Vector2 force)
    {
        Vector2 position = (Vector2)ballHolder.position + (force.normalized * 50f * t);
        return position;
    }

    /// <summary>
    /// Event called when the player moves with w,a,s,d or left joystick
    /// </summary>
    /// <param name="context"></param>
    public void OnMove(InputAction.CallbackContext context){
        if (!isUnconscious && !isCharging)
        {
            float mouvementInput = context.ReadValue<float>();
            horizontalMove = mouvementInput * runSpeed;
            
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }
    }

    /// <summary>
    /// Event called when player is pressing w,a,s,d or left joystick to know where he wants to go
    /// Used to know the dash orientation
    /// </summary>
    /// <param name="context"></param>
    public void OnOrientation(InputAction.CallbackContext context){
        orientationInput = context.ReadValue<Vector2>();         
    }

    /// <summary>
    /// Event called when player hits space key or 'a'/'x' on a controller (xboxone/ps4)
    /// </summary>
    /// <param name="context"></param>
    public void OnJump(InputAction.CallbackContext context){
        if (!isUnconscious)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            float jumpInput = context.ReadValue<float>();
            if (jumpInput == 1)
            {
                jump = true;
                animator.SetBool("IsJumping", true);
            }
        }
    }

    /// <summary>
    /// Event called when player uses right button of mouse or triggers on controller
    /// </summary>
    /// <param name="context"></param>
    public void OnDash(InputAction.CallbackContext context){
        if (!isUnconscious)
        {
            float dashInput = context.ReadValue<float>();
            if (dashInput == 1 && !dashUsed && !hold)
            {
                //If we are moving only on the x-axis
                if (orientationInput.x != 0.0f && orientationInput.y == 0.0f)
                {
                    transform.GetComponent<Rigidbody2D>().velocity = orientationInput * 100f;
                }
                //If we are at least moving on y-axis 
                else
                {
                    transform.GetComponent<Rigidbody2D>().velocity = orientationInput * 40f;
                }
                dashUsed = true;
            }
        }
    }

    /// <summary>
    /// Event called when player uses left click on mouse
    /// </summary>
    /// <param name="context"></param>
    public void OnShootMouse(InputAction.CallbackContext context){
        //If the player is not unconscious and is not moving
        if (!isUnconscious && horizontalMove == 0)
        {
            //Debug.Log("ShootMouse");
            float shootInput = context.ReadValue<float>();

            //If user is holding the ball
            if (hold)
            {
                //If click is pressed
                if (shootInput == 1)
                {
                    isCharging = true;
                    //Setup to get the vector the player is aiming
                    Camera cam = Camera.main;
                    Vector2 mousePoint = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                    Vector2 position = go.GetComponent<Rigidbody2D>().position;
                    mouseFinalOrientationShoot = new Vector2(mousePoint.x - position.x, mousePoint.y - position.y);
                    //Showing the points at their correct position to help player know where he shoots
                    for (int i = 0; i < numberOfPoints; i++)
                    {
                        points[i].transform.position = PointPosition(i * spaceBetweenPoints, mouseFinalOrientationShoot);
                        points[i].transform.GetComponent<SpriteRenderer>().enabled = true;
                    }
                }
                //If left click is released and we were charging the shoot
                else if (shootInput == 0 && isCharging == true)
                {
                    isCharging = false;
                    //Setup to let the ball have physics again
                    go.transform.parent = null;
                    go.GetComponent<Rigidbody2D>().isKinematic = false;
                    go.GetComponent<CircleCollider2D>().isTrigger = false;
                    animator.SetBool("HasBall", false);
                    animator.SetBool("Charging", false);

                    chargeBarGO.SetActive(false);
                    //force.Normalize();
                    //Force the ball gets depending on the vector the player used and the charge
                    go.GetComponent<Rigidbody2D>().AddForce(mouseFinalOrientationShoot.normalized * charge * 500f);
                    hold = false;
                    go = null;
                    charge = 0f; 
                    //Hiding the points
                    for (int i = 0; i < numberOfPoints; i++)
                    {
                        points[i].transform.GetComponent<SpriteRenderer>().enabled = false;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Event called when player moves mouse. Used to know where he is currently aiming to move points
    /// </summary>
    /// <param name="context"></param>
    public void OnMouseMove(InputAction.CallbackContext context)
    {
        if (isCharging)
        {
            //Setup to get the vector the player is aiming
            Camera cam = Camera.main;
            Vector2 mousePoint = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 position = go.GetComponent<Rigidbody2D>().position;
            mouseFinalOrientationShoot = new Vector2(mousePoint.x - position.x, mousePoint.y - position.y);
            //Showing the points at their correct position to help player know where he shoots
            for (int i = 0; i < numberOfPoints; i++)
            {
                points[i].transform.position = PointPosition(i * spaceBetweenPoints, mouseFinalOrientationShoot);
                points[i].transform.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    /// <summary>
    /// Used on Unity editor to see attack range easily
    /// </summary>
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    /// <summary>
    /// Event called when player is moving right joystick
    /// </summary>
    /// <param name="context"></param>
    public void OnShootController(InputAction.CallbackContext context){
        //If player is not unconscious and is not moving
        if (!isUnconscious && horizontalMove == 0)
        {
            Vector2 shootInput = context.ReadValue<Vector2>();
            //If player is holding the ball
            if (hold)
            {
                //If the joystick is not on the center, it means that the player is aiming
                if (shootInput.x != 0.0f || shootInput.y != 0.0f)
                {
                    isCharging = true;
                    orientationShoot = shootInput;
                    isCenterJoystick = false;
                    //Showing the points at their correct position to help player know where he shoots
                    for (int i = 0; i < numberOfPoints; i++)
                    {
                        points[i].transform.position = PointPosition(i * spaceBetweenPoints, orientationShoot);
                        points[i].transform.GetComponent<SpriteRenderer>().enabled = true;
                    }
                }
                //If the joystick comes back to the center it means player wants to shoot
                else
                {
                    if (isCharging)
                    {
                        isCenterJoystick = true;
                        isCharging = false;
                    }
                }
                previousOrientationShoot = shootInput;
            }
        }
    }

    /// <summary>
    /// Event called when player uses left click or x/b on xbox or square and circle on ps
    /// </summary>
    /// <param name="context"></param>
    public void OnPunch(InputAction.CallbackContext context)
    {
        if (!isUnconscious)
        {
            if (!hold)
            {
                //So player has to wait before attacking again
                if (Time.time >= nextAttackTime)
                {
                    animator.SetTrigger("Attack");
                    animator.SetBool("IsAttacking", true);

                    //Gets all enemis that are in range and in the specified layer
                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

                    foreach (Collider2D enemy in hitEnemies)
                    {
                        enemy.GetComponent<PlayerMovement>().TakeDamage(1);
                    }
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }
    }

    public void OnUtils(InputAction.CallbackContext context)
    {
        float utilsInput = context.ReadValue<float>();
        if(utilsInput == 1 && !isUnconscious){
            GameObject player = GameObject.Find("PlayerBlue");
            if(GameObject.Find("PlayerBlue").name == gameObject.name){
                player = GameObject.Find("PlayerPurple");
            }
            if(powerUp == 1){
                gameObject.GetComponent<FreezePlayer>().StartFreeze(player);                                
            }else if(powerUp == 2){                
                GameObject.Find("Ball").GetComponent<ExplosionBall>().StartExplosion(player);                
            }
            powerUp = 0;
        }
    }

    public void OnGoingThroughDown(InputAction.CallbackContext context)
    {
        GameObject g = GameObject.Find("Ground_Wood");
        if (g != null)
        {
            BoxCollider2D[] boxs = g.GetComponents<BoxCollider2D>();
            CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();
            float down = context.ReadValue<float>();
            if (down == 1)
            {
                for (int i = 0; i < boxs.Length; i++)
                {
                    Physics2D.IgnoreCollision(capsule, boxs[i]);
                }
            }
            else
            {
                for (int i = 0; i < boxs.Length; i++)
                {
                    Physics2D.IgnoreCollision(capsule, boxs[i], false);
                }
            }
        }
    }

    /// <summary>
    /// Function called when player takes damage
    /// </summary>
    public void TakeDamage(int nbTimes)
    {
        //If he has health points
        if (hp > 0)
        {
            //If he does not have armor
            if(nbArmor < nbTimes){
                //if he's holding the ball, he has to let it go
                if (hold)
                {
                    go.transform.parent = null;
                    go.GetComponent<Rigidbody2D>().isKinematic = false;
                    go.GetComponent<CircleCollider2D>().isTrigger = false;
                    animator.SetBool("HasBall", false);
                    animator.SetBool("Charging", false); 
                    go.GetComponent<SpriteRenderer>().enabled = true;
                    Vector2 force = new Vector2(1,1);
                    go.GetComponent<Rigidbody2D>().AddForce(force * 2000f);
                    hold = false;
                    go = null;
                    charge = 0f;
                    chargeBarGO.SetActive(false);
                }

                animator.SetTrigger("Hit");                

                //UI health 
                if(healthPlayers != null){
                    hp -= nbTimes;
                    if(hp < 0){
                        hp = 0;
                    }
                    if (team == 0)
                    {
                        healthPlayers.healthLossBlue(hp);
                    }
                    else
                    {
                        healthPlayers.healthLossViolet(hp);
                    }
                    //If player has no more health points
                    if (hp <= 0)
                    {
                        animator.SetBool("Unconscious", true);
                        isUnconscious = true;

                    }
                }
            
                isCharging = false;

                //Hiding the points
                for (int i = 0; i < numberOfPoints; i++)
                {
                    points[i].transform.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
            else{                
                nbArmor -= nbTimes;
                if (team == 0) {
                    healthPlayers.armorLossBlue(nbArmor);
                } else {
                    healthPlayers.armorLossViolet(nbArmor);
                }
            }

            FindObjectOfType<AudioManager>().Play("oof");
        }
    }

    /// <summary>
    /// Function called in update, restrictions are made so things happen only when player is charging the shoot
    /// </summary>
    void Charging(){
        if (!isUnconscious && horizontalMove == 0)
        {
            if (isCharging)
            {
                //Shows the ball
                go.GetComponent<SpriteRenderer>().enabled = true;
                //Animation charging
                animator.SetBool("Charging", true);
                //Shows charge bar
                chargeBarGO.SetActive(true);
                //Used for controller to know if the vector used is valid
                ValidShoot();
                //Changing charge value based on time
                if (updown)
                {
                    charge += Time.fixedDeltaTime * 4f;
                    if (charge >= 10f)
                    {
                        updown = false;
                    }
                }
                else
                {
                    charge -= Time.fixedDeltaTime * 4f;
                    if (charge <= 0f)
                    {
                        updown = true;
                    }
                }
                chargeBar.SetCharge(charge);
            }
        }
    }

    /// <summary>
    /// Function used for controller to know if shoot is valid or not
    /// User has to be on the approximatively same spot to validate the shoot
    /// The reason is that when user releases the joystick, there can be misinterpretations due to the joystick not going exactly to (0,0) first
    /// </summary>
    void ValidShoot(){
        //If we have a previous vector that we were aiming at
        if(previousOrientationShoot != Vector2.zero){
            //If our current vector is close to the previous vector, we are considering them the same
            if(orientationShoot.x > previousOrientationShoot.x - deltaPosition && orientationShoot.x < previousOrientationShoot.x + deltaPosition &&
                orientationShoot.y > previousOrientationShoot.y - deltaPosition && orientationShoot.y < previousOrientationShoot.y + deltaPosition){               
                cooldown -= Time.deltaTime;
                //If player has waited enough time on the same spot
                if(cooldown <= 0){         
                    //We regard it as his final vector for the moment in case he releases the shoot
                    finalOrientationShoot = orientationShoot;
                    cooldown = TIMECOOLDOWN; 
                }
            //If not we reset the cooldown to validate final vector
            }else{
               cooldown = TIMECOOLDOWN; 
            }
        //If not we reset the cooldown to validate final vector
        }else{
            cooldown = TIMECOOLDOWN;
        }
    }

    /// <summary>
    /// Function called in update, restrictions are made so tings only happen when player has released joystick
    /// </summary>
    void CenterJoystick(){
        if(isCenterJoystick && go.transform.parent != null){
            //Cooldown used to be sure that the joystick is in the center enough time to be sure that player wants to shoot
            cooldown2 -= Time.deltaTime;
            if(cooldown2 <= 0){              
                //We release the ball
                go.transform.parent = null;
                go.GetComponent<Rigidbody2D>().isKinematic = false;
                go.GetComponent<CircleCollider2D>().isTrigger = false;
                animator.SetBool("HasBall", false);
                animator.SetBool("Charging", false);

                //Add a force to it
                if(finalOrientationShoot != null){
                    finalOrientationShoot.Normalize();
                }
                
                go.GetComponent<Rigidbody2D>().AddForce(finalOrientationShoot * charge * 500f);
                hold = false;
                go = null;
                charge = 0f;
                cooldown2 = TIMECOOLDOWN;
                isCenterJoystick = false;
                chargeBarGO.SetActive(false);
                //Hide the points
                for (int i = 0; i < numberOfPoints; i++)
                {
                    points[i].transform.GetComponent<SpriteRenderer>().enabled = false;
                }
            }                            
        }
    }

    /// <summary>
    /// Function called in update, restrictions are made so tings only happen when player is unconscious
    /// </summary>
    private void Unconscious()
    {
        if (isUnconscious)
        {
            //Time to wait before player is not unconscious anymore
            timeUnconscious -= Time.deltaTime;
            if (timeUnconscious <= 0)
            {
                timeUnconscious = 1f;
                isUnconscious = false;
                animator.SetBool("Unconscious", false);
                //Recover all health points
                hp = 3;
                if (team == 0)
                {
                    healthPlayers.recoverBlue();
                }
                else
                {
                    healthPlayers.recoverViolet();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Charging();
        CenterJoystick();
        Unconscious();
    }

    /// <summary>
    /// Event called when player lands on the ground
    /// </summary>
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    /// <summary>
    /// Event called when player collides with something else
    /// </summary>
    /// <param name="collision">Object of the collision</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isUnconscious)
        {
            //If we collide with ground
            if (collision.collider != null && collision.collider.tag.Equals("Ground"))
            {
                //Dash reset
                dashUsed = false;
            }
            //If we collide with ball
            else if (collision.collider != null && collision.collider.tag.Equals("Ball"))
            {
                //We hold the ball
                go = collision.collider.gameObject;
                hold = true;
                animator.SetBool("HasBall", true);
                go.GetComponent<SpriteRenderer>().enabled = false;
                go.transform.parent = ballHolder;
                go.transform.position = ballHolder.position;
                go.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                go.GetComponent<Rigidbody2D>().isKinematic = true;
                go.GetComponent<CircleCollider2D>().isTrigger = true;

            }
        }
    }

    public int GetNbArmor(){
        return nbArmor;
    }

    public void SetNbArmor(int nb){
        nbArmor = nb;
        if(nbArmor > 3) {
            nbArmor = 3;
        }else if(nbArmor > hp){
            nbArmor = hp;
        }
        if (team == 0) {
            healthPlayers.armorGainBlue(hp-nbArmor);
        } else {
            healthPlayers.armorGainViolet(hp-nbArmor);
        }
    }
}
