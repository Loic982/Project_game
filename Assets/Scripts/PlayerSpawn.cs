using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position; //on va déplacer la joueur à la position de la transforme actuel (l'objet qui possède le script)
    }
}
