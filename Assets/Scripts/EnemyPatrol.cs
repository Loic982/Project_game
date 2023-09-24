using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed; //vitesse de l'ennemi
    public Transform[] waypoints; //on utilise un sys de waypoints pour que l'ennei se d�place d'un pt a � un pt b (donc cr�ation d'une array)

    public int damageOnCollision=20; //les d�gats au moment de la collision

    public SpriteRenderer graphics;
    private Transform target; //la cible vers laquelle l'ennemi va se d�placer (l'un des waypoints)
    private int destPoint = 0; //point de destination 

    void Start()
    {
        target = waypoints[0]; //on initialise la cible au 1er emplacement du tableau
    }
    void Update()
    {
        Vector3 dir = target.position - transform.position; 
        //un vecteur direction qui prend la position de la cible vers laquelle on veut aller et on va soustraire la position actuelle de l'ennemi
        //permet de savoir dans quelle direction aller pour se rendre au prochain waypoint
        transform.Translate(speed * Time.deltaTime * dir.normalized, Space.World);
        //normalis� pour que le vecteur soit de longueur 1 (unitaire), pour que le vecteur ai tjrs "la m�me taille"
        //Space.World pour dire que le perso va se d�placer par rapport au monde
        //si on arr�te ici, le d�placement fonctionnerait qu'une fois !!! On n'a pas le sys qui va changer la target

        if (Vector3.Distance(transform.position, target.position) < 0.3f)
            // si la distance entre la position de la cible et sa destination est < 0.3 (s�curit� pour ne pas mettre 0)
        {
            destPoint = (destPoint + 1) % waypoints.Length;
            // "%" permet de r�cup�rer le reste d'une division. Ici �a va nous renvoyer le prochain point o� on veut se d�placer
            // si on n'ajoute pas "% waypoints.Length", une fois arriver au dernier point, target ne pourra plus faire "destPoint + 1"
            // car il n'existe pas -> erreur
            // pour revenir au point initial (donc 0), on ajoute cette commande et avec le "%", le resultat = 0 donc on reviendra au point initiale (principe de boucle)
            target = waypoints[destPoint];
            graphics.flipX = !graphics.flipX;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) //Le "2D" est important car si on le met pas, �a sera pour des collider 3D
    {
        if (collision.transform.CompareTag("Player")) //si l'ennemie entre en contact avec le joueur
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>(); //ne sera lu que si le joueur entre en contact avec l'ennemi -> CompareTag("Player")
            playerHealth.TakeDamage(damageOnCollision);
        }
    }
}
