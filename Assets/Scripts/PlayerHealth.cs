using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; //par défaut, le joueur aura 100 pv
    public int currentHealth; //si le joueur est blessé par la suite, il aura - de 100 pv

    public float invincibilityTimeAfterHit = 3f;
    public float invincibilityFlashDelay = 0.15f;
    public bool isInvicible = false ; //l'invicibilité quand on se fait toucher par un mob

    public SpriteRenderer graphics; //nous permet d'accéder au sprite rederer, puis à color et puis à alpha (transparence)
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth; //on initialise la vie actuelle, pour que le joueur commence avec 100 pv
        healthBar.SetMaxHealth(maxHealth); //permet de correspondre les pv à la barre de vie graphique
        // "SetMaxHealth" est la maéthode dans le fichier "HealthBar.cs"
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        //si j'appuie sur la touche "H", j'appelle la méthode TakeDamage qui va retirer 20 de pv
        {
            TakeDamage(20);
        }
        //test de la barre de vie sans avoir à développer des ennemis
    }

    public void TakeDamage(int damage) //permet de prendre des dégats (teste de la barre de vie)
    {
        if (!isInvicible)
        {
            currentHealth -= damage; //on retire des pv à la vie actuelle
            //ici on modifie les pv mais pas l'affichage ! donc qd on modifie les pv, tjrs faire un appel à la barre de vie
            healthBar.SetHealth(currentHealth); //affichage 
            isInvicible = true;
            StartCoroutine(InvincibilityFlash()); //on appel la coroutine (timer)
            StartCoroutine(HandleInvincibilityDelay());
        }
    }
    
    public IEnumerator InvincibilityFlash()
    {
        while (isInvicible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f); //transparent
            //4 paramètres : R,G,B et A
            //1f correspond à la valeur maximale (couleur R pour red va de 0 à 255 avec 255 qui est la couleur du perso)
            //et 1f correspond à 255 
            yield return new WaitForSeconds(invincibilityFlashDelay); //délais de 0.15s dans le clignotement
            graphics.color = new Color(1f, 1f, 1f, 1f); //non transparent
            yield return new WaitForSeconds(invincibilityFlashDelay); //délais de 0.15s dans le clignotement pour le retour de boucle
        }
    }

    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(invincibilityTimeAfterHit);
        isInvicible = false;
    }
}
