using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TrainController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float fuel = 300f;
    [SerializeField] private float fuelConsumption = 0.1f;
    [SerializeField] UnityEngine.Object MiceCart;
    [SerializeField] private float offset = 1.5f;

    private Rigidbody2D rb;
    private Animator anim;
    private Slider fuelSlider;
    private Image[] trainResourcesUI = new Image[3];
    private int resourceCount;
    private int maxCarts = 2;
    private int currentCarts;
    private UnityEngine.Object[] carts;
    private TerrainSlicer ts;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ts = GetComponent<TerrainSlicer>();
        fuelSlider = GameObject.Find("TrainFuellProgressBar").GetComponent<Slider>();
        FindUIResources();
        carts = new UnityEngine.Object[maxCarts];
        currentCarts = 0;
        resourceCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        WinCheck();
        Move();
        ProgressBarrUpdate();
        SlicerCheck();
    }

    private void SlicerCheck()
    {
        if (currentCarts > 0 && ts.lastSlicedPos >= (int)((GameObject)carts[currentCarts - 1]).transform.position.x - 1)
        {
            RemoveCart();
        }
        if (ts.lastSlicedPos >= (int)transform.position.x - 2)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GameInfo.lost = true;
        }
    }

    private void FindUIResources()
    {
        for (int i = 0; i < 3; i++)
        {
            trainResourcesUI[i] = GameObject.Find("TrainResource (" + i + ")").GetComponent<Image>();
        }
    }

    private void ProgressBarrUpdate()
    {
        if(Time.timeScale == 0)
        {
            return;
        }
        fuelSlider.value = (fuel / 300f)*0.2f+0.8f;
        for (int i = 0; i < 3; i++)
        {
            if (i < resourceCount)
            {
                trainResourcesUI[i].enabled = true;
            }
            else
            {
                trainResourcesUI[i].enabled = false;
            }
        }
    }

    private void WinCheck()
    {
        bool win = false;
        if (currentCarts == maxCarts)
        {
            win = true;
            foreach (UnityEngine.Object cart in carts)
            {
                if (cart == null)
                {
                    win = false;
                    break;
                }

                if (((GameObject)cart).GetComponent<MiceCartController>().isFull == false)
                {
                    win = false;
                    break;
                }
            }
            
        }
        if (win)
        {
            GameInfo.won = true;
        }
    }

    private void Move()
    {
        if(HasFuell())
        {
            if(Time.timeScale == 0)
            {
                return;
            }
            rb.velocity = new Vector2(speed, 0);
            fuel -= fuelConsumption;
            anim.SetBool("Moving", true);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            anim.SetBool("Moving", false);
        }
        MoveCarts();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            Rigidbody2D irb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (irb != null)
            {
                if (irb.velocity.x != 0 || irb.velocity.y != 0)
                {
                    if (collision.gameObject.CompareTag("Fuell"))
                    {
                        if (fuel < 300)
                        {
                            fuel += 100;
                            Destroy(collision.gameObject);
                        }
                    }
                    else if (collision.gameObject.CompareTag("Resource"))
                    {
                        if (resourceCount < 3 && TrainHasSpace())
                        {
                            resourceCount++;
                            Destroy(collision.gameObject);
                            if (resourceCount == 3)
                            {
                                AddCart();
                                resourceCount = 0;
                            }
                        }
                    }
                }
            }
        }
    }

    public void MoveCarts()
    {
        for (int i = 0; i < currentCarts; i++)
        {
            if ((GameObject)carts[i] != null)
            {
                MiceCartController mcc = ((GameObject)carts[i]).GetComponent<MiceCartController>();
                mcc.Move(rb.velocity);
            }
        }
    }
    

    private void AddCart()
    {
            carts[currentCarts] = Instantiate(MiceCart, new Vector3(transform.position.x - 2 * currentCarts - offset, transform.position.y, transform.position.z), Quaternion.identity);
            currentCarts++;  
    }

    private void RemoveCart()
    {
        if (currentCarts > 0)
        {
            Destroy((GameObject)carts[currentCarts-1]);
            currentCarts--;
        }
    }

    private bool TrainHasSpace()
    {
        bool ret = false;
        if (currentCarts < maxCarts)
        {
            ret = true;
        }
        return ret;
    }

    private bool HasFuell()
    {
        bool ret = false;
        if (fuel > 0)
        {
            ret = true;
        }
        return ret;
    }

    }
