using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player; //défini un joueur (à ajouter après avec le compenent)
    public float timeOffset; //définit un temps de décallage entre le mvt du joueur et le mvt de la cam
    public Vector3 posOffset; //définit un vecteur (x,y,z) de position de la cam

    private Vector3 velocity; //pour en faire une ref après (c'est comme ça que fonctionne vs)
    void Update() //métode
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOffset, ref velocity, timeOffset);
        // la position de la cam(transform.position) c'est un déplacement "smooth" sur un vecteur x,y et z
        // en prenant en compte la position de la cam
        // la position du joueur (par rapport à la cam) / le décallage 
        // une réf de vitesse (par défaut)
        // un temps de décallage
    }
}
