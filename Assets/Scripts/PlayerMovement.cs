using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed; //fixe la vitesse
    public float jumpForce; //fixe la force de saut (sur y)

    public bool isJumping; //bool�an, saute ou pas
    public bool isGrounded; // permet d'�viter de sauter si on est d�j� en l'air

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayer;

    public Rigidbody2D rb; //fait ref au rigidvody du player. On passe par l� pour d�placer le player
    public Animator animator; //on importe nos animations
    public SpriteRenderer spriteRenderer;

    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;


    void Update()
    {

        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        //permettra apr�s de savoir si on va vers la gauche ou droite
        //on multiplie par la vitesse
        //on multiplie par le temps (permettra d'ex�cuter l'action au fil du temps)
        // -> aussi longtemps qu'on appuie sur la touche (irl), le perso bougera
        //Ici on a calcul� le mvt mais on va l'appliquer

        if (Input.GetButtonDown("Jump") && isGrounded) //si le joueur appuie sur la touche de saut
        //"Jump" correspond � la barre d'espace et est d'office pr�sente quand on utilise Unity
        {
            isJumping = true;
            // maintenant on sait si le perso veut sauter mais il faut ajouter le mvt
        }

        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x); //renvoie tjr une valeur positive
        animator.SetFloat("Speed", characterVelocity); //connaitre la vitesse
        // Si on utilise "rb.velocity.x"probl�me, si on se d�place en arri�re, vitesse n�gative?
        // Donc on la rend absolue avec "characterVelocity"

    }

    void FixedUpdate() //on veut calculer le mvt du perso. Ttes les op�rations de physique se font dans FixedUpdate mais on ne r�cup�re pas les Input dedans ! (on le fait dans la fonction update)
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);

        //                      Fig.1
        //                    ---------
        //                    -   P   -
        //                    -   L   -
        //                    -   A   -
        //                    -   Y   -
        //                    -   E   -    Circle (OverlapCircle)
        //                    -   R   -   /
        //                    -   *   -  /
        //                    -*-----*- /
        //         ----------*---------*----------
        //         -         *         *         -
        //         -           *     *           -
        //         -              *              -
        //         -            Ground           -
        //         -                             -
        //         -                             -
        //         -------------------------------
        //
        //quand le cercle rentre en contact avec la collision du sol, ok!
        //Probl�me : il y a d�j� une collision dans le cercle, celle du player
        //On va donc utiliser le principe de "Layer : Player" qui permet d'isoler ces collision et r�gler le probl�me



        //~~~~~~~~~~~~~~~~~~~~
        // !! Update ep.7 !!
        //~~~~~~~~~~~~~~~~~~~~
        //isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        // cr�e une zone entre le 1er �l�ment et le 2eme �lement. Cr�e une boite de collision
        // si la boite de collision entre en contact avec quelque chose, on va renvoy� "true"
        // donc "isGrounded" = true
        // sinon, renvoie false
        // permet de savoir si le perso est au sol ou pas

        MovePlayer(horizontalMovement); //App�lation d'une m�thode
    }



    public void MovePlayer(float _horizontalMovement) //Cr�ation d'une m�thode
    //"_horizontalMovement" est une convention de nommage car cette variable est un 
    //param�tre
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        // La direction o� on va se move va � bas� sur :
        // _horizontalMovement (axe x)
        // Mais vu que je d�finit un nouveau vector2, je dois pr�ciser sur l'axe y
        // et on lui laisse la valeur par d�faut
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
        if (isJumping == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            //ici on ajoute une force sur un vecteur 2D (x et y)
            //on ajoute pas de force sur l'axe x (donc 0f)
            //on modifie la force en hauteur avec jumpForce d�clar� plus haut
            isJumping = false;
        }
    }
    void Flip(float _velocity) //m�thode pour "retourner" le joueur quand il va � gauche ou � droite
    {
        if (_velocity > 0.1f) //si sa vitesse est > � 0.1, il se met � courrir de droite � gauche (car vitesse +)
        {
            spriteRenderer.flipX = false; //alors on flippe pas sur l'axe x
        }else if(_velocity < -0.1f) //si sa vitesse est < � -0.1, il se met � courrir de gauche � droite (car vitesse -)
        {
            spriteRenderer.flipX = true; //alors on flippe sur l'axe x
        }
    }

    private void OnDrawGizmos()  //cr�ation d'un gizmo pour Fig.1
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius); 
    }
}
