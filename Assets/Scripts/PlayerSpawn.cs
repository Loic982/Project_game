using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position; //on va d�placer la joueur � la position de la transforme actuel (l'objet qui poss�de le script)
    }
}
