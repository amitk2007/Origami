using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DrawScript : MonoBehaviour
{
    public GameControllerScript.origamiState state;
    List<GameObject> points = new List<GameObject>();
    public GameObject dot;
    public Camera cam;

    bool canDraw = true;
    bool currentMove = true;
    int currentPoint = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayButtonScript.move == true && currentMove == true)
        {
            canDraw = false;

            try
            {
                float dist = Vector3.Distance(points[1].transform.position, transform.position);
                LookAt2D(this.gameObject, points[1]);
                transform.position = Vector3.MoveTowards(transform.position, points[1].transform.position, Time.deltaTime * PlayButtonScript.speed);

                if (dist <= PlayButtonScript.reachDist)
                {
                    Destroy(points[0]);
                    points.RemoveAt(0);
                    DrawLine();
                }
                if (points.Count == 0)
                {
                    currentMove = false;
                }
            }
            catch (System.Exception)
            {
            }
        }
    }

    #region moving the player
    private void OnMouseDown()
    {
        DeleteLineAndPoints();
    }

    private void OnMouseDrag()
    {
        if (Input.touches[0].phase == TouchPhase.Moved && canDraw == true)
        {
            points.Add((GameObject)Instantiate(dot, cam.ScreenToWorldPoint(new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, 10)), Quaternion.identity));
            DrawLine();
        }
    }

    private void OnMouseUp()
    {
        DrawLine();
        ClearDots();
    }

    public void DrawLine()
    {
        LineRenderer lineRenderer = this.gameObject.GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(points.Count);
        lineRenderer.SetWidth(0.05f, 0.05f);

        Color color = GetComponent<SpriteRenderer>().color;
        color.a = 0.5314159f;
        //color = new Color(0.9333f, 0.26275f, 0.5938f, 0.26275f);
        lineRenderer.SetColors(color, color);

        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, points[i].transform.position);
        }
    }

    public void ClearDots()
    {
        foreach (GameObject dot in points)
        {
            dot.GetComponent<Renderer>().enabled = false;
        }
    }

    public void LookAt2D(GameObject looking, GameObject at)
    {
        var dir = at.transform.position - looking.transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        looking.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    public void DeleteLineAndPoints()
    {
        foreach (GameObject p in points)
        {
            Destroy(p);
        }
        points.Clear();
        DrawLine();
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Finish")
        {
            DestroyGameObject(this.gameObject);
        }
        else if (collision.transform.tag == "Player")
        {
            if (GameControllerScript.OrigamiStateCollide(state, collision.gameObject.GetComponent<DrawScript>().state))
            {
                this.gameObject.GetComponent<Collider2D>().isTrigger = true;
            }
            else
            {
                print("dead");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            this.gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
    }

    public void DestroyGameObject(GameObject gameObject)
    {
        string tag = gameObject.transform.tag;
        DeleteLineAndPoints();
        Destroy(gameObject);
        print(GameObject.FindGameObjectsWithTag(tag).Length + " tag = " + tag);
        if (GameObject.FindGameObjectsWithTag(tag).Length <= 1)
        {
            Application.LoadLevel(Application.loadedLevel + 1);
        }
    }
}
