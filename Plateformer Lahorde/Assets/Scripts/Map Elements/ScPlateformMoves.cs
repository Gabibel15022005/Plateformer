using System.Collections.Generic;
using UnityEngine;

public class ScPlateformMoves : MonoBehaviour
{
    [SerializeField] private List<Transform> _positions;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _delayBetweenMove = 1f;
    private int _nextPos;
    private bool _wait = false;

    void Start()
    {
        if (_positions.Count < 2)
        {
            Debug.LogError("Il faut au moins deux positions pour déplacer la plateforme !");
            return;
        }

        _nextPos = 1;
        transform.position = _positions[0].position;
    }

    void Update()
    {
        if (_positions.Count < 2) return;

        if (Vector3.Distance(transform.position, _positions[_nextPos].position) < 0.05f && _wait == false)
        {
            _wait = true;
            StartCoroutine(WaitFor());
        }
        
        Vector3 direction = new Vector3(_positions[_nextPos].position.x, _positions[_nextPos].position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, direction, _speed * Time.deltaTime);

    }

    private System.Collections.IEnumerator WaitFor()
    {
        yield return new WaitForSeconds(_delayBetweenMove);
        _nextPos = (_nextPos + 1) % _positions.Count;
        _wait = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("CollisionEnter");
        if (collision.gameObject.CompareTag("Player"))
        collision.gameObject.transform.SetParent(transform);
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("CollisionExit");
        if (collision.gameObject.CompareTag("Player"))
        collision.gameObject.transform.SetParent(null);
    }



}
