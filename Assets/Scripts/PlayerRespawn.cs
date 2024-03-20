using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private GameObject train;

    private Vector3 respawnPoint;
    private ItemCollector ic;
    private Rigidbody2D rb;

    private bool dead;
    
    private void Start()
    {
        dead = true;
        rb = GetComponent<Rigidbody2D>();
        ic = GetComponent<ItemCollector>();
        respawnPoint = new Vector2(train.transform.position.x - 0.5f, train.transform.position.y + 0.5f);
    }


    void Update()
    {
        respawnPoint = new Vector2(train.transform.position.x - 1.25f, train.transform.position.y + 1.5f);
        if (dead)
        {
            Respawn();
        }
        IsDead();

    }

    private void Respawn()
    {
        for (int i = 0; i < ic.inventory.Length; i++)
        {
            ic.inventory[i] = 0;
        }
        ic.UpdateInventoryUI();
        transform.position = respawnPoint;
        rb.velocity = new Vector3(0,10,0);
        dead = false;
    }
    private void IsDead()
    {
        if (transform.position.y < -20)
        {
            dead = true;
        }
    }
}
