using UnityEngine;

public class WeakSpot : MonoBehaviour
{
    public GameObject objectToDestroy;
    public void OnTriggerEnter2D(Collider2D collision) //collision est l'élément qui est entré dans la zone
    {
        if (collision.CompareTag("Player"))
        {
            //si c'est bien le joueur qui est entré dans cette zone : 
            Destroy(objectToDestroy);
            //on va vérifier si l'élément qui est entré en collision porte le tag player
            //le tag est une option à mettre dans unity pour identifier un objet sans se baser sur son nom
            //le parent de weakspot c'est l'ennemi
            //On ne veut pas supp weakspot mais l'ennemi (et tous ces sous objet)
        }
    }
}
