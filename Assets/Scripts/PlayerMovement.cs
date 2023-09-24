using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed; //fixe la vitesse
    public float jumpForce; //fixe la force de saut (sur y)

    public bool isJumping; //booléan, saute ou pas
    public bool isGrounded; // permet d'éviter de sauter si on est déjà en l'air

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayer;

    public Rigidbody2D rb; //fait ref au rigidvody du player. On passe par là pour déplacer le player
    public Animator animator; //on importe nos animations
    public SpriteRenderer spriteRenderer;

    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;


    void Update()
    {

        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        //permettra après de savoir si on va vers la gauche ou droite
        //on multiplie par la vitesse
        //on multiplie par le temps (permettra d'exécuter l'action au fil du temps)
        // -> aussi longtemps qu'on appuie sur la touche (irl), le perso bougera
        //Ici on a calculé le mvt mais on va l'appliquer

        if (Input.GetButtonDown("Jump") && isGrounded) //si le joueur appuie sur la touche de saut
        //"Jump" correspond à la barre d'espace et est d'office présente quand on utilise Unity
        {
            isJumping = true;
            // maintenant on sait si le perso veut sauter mais il faut ajouter le mvt
        }

        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x); //renvoie tjr une valeur positive
        animator.SetFloat("Speed", characterVelocity); //connaitre la vitesse
        // Si on utilise "rb.velocity.x"problème, si on se déplace en arrière, vitesse négative?
        // Donc on la rend absolue avec "characterVelocity"

    }

    void FixedUpdate() //on veut calculer le mvt du perso. Ttes les opérations de physique se font dans FixedUpdate mais on ne récupère pas les Input dedans ! (on le fait dans la fonction update)
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
        //Problème : il y a déjà une collision dans le cercle, celle du player
        //On va donc utiliser le principe de "Layer : Player" qui permet d'isoler ces collision et régler le problème



        //~~~~~~~~~~~~~~~~~~~~
        // !! Update ep.7 !!
        //~~~~~~~~~~~~~~~~~~~~
        //isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        // crée une zone entre le 1er élément et le 2eme élement. Crée une boite de collision
        // si la boite de collision entre en contact avec quelque chose, on va renvoyé "true"
        // donc "isGrounded" = true
        // sinon, renvoie false
        // permet de savoir si le perso est au sol ou pas

        MovePlayer(horizontalMovement); //Appélation d'une méthode
    }



    public void MovePlayer(float _horizontalMovement) //Création d'une méthode
    //"_horizontalMovement" est une convention de nommage car cette variable est un 
    //paramètre
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        // La direction où on va se move va ê basé sur :
        // _horizontalMovement (axe x)
        // Mais vu que je définit un nouveau vector2, je dois préciser sur l'axe y
        // et on lui laisse la valeur par défaut
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
        if (isJumping == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            //ici on ajoute une force sur un vecteur 2D (x et y)
            //on ajoute pas de force sur l'axe x (donc 0f)
            //on modifie la force en hauteur avec jumpForce déclaré plus haut
            isJumping = false;
        }
    }
    void Flip(float _velocity) //méthode pour "retourner" le joueur quand il va à gauche ou à droite
    {
        if (_velocity > 0.1f) //si sa vitesse est > à 0.1, il se met à courrir de droite à gauche (car vitesse +)
        {
            spriteRenderer.flipX = false; //alors on flippe pas sur l'axe x
        }else if(_velocity < -0.1f) //si sa vitesse est < à -0.1, il se met à courrir de gauche à droite (car vitesse -)
        {
            spriteRenderer.flipX = true; //alors on flippe sur l'axe x
        }
    }

    private void OnDrawGizmos()  //création d'un gizmo pour Fig.1
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius); 
    }
}
