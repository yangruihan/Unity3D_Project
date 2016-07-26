using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour
{

    public float speed = 2.0f;

    [SyncVar]
    public Color playerColor = Color.white;
    [SyncVar]
    public string playerName;

    protected Rigidbody2D _rigidbody;

    protected float _horizontal = 0;
    protected float _vertical = 0;
    protected float _inverseMoveTime = 0.1f;
    protected Vector2 _targetPos;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        Renderer[] rends = GetComponentsInChildren<Renderer>();
        foreach (var r in rends)
        {
            r.material.color = playerColor;
        }
    }

    [ClientCallback]
    void Update()
    {
        _horizontal = 0;
        _vertical = 0;

        if (!isLocalPlayer)
            return;

        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
    }

    [ClientCallback]
    void FixedUpdate()
    {
        if (!hasAuthority)
        {
            return;
        }

        _rigidbody.MovePosition(Vector2.Lerp(transform.position, _targetPos, _inverseMoveTime * Time.deltaTime));

        if (_horizontal != 0 || _vertical !=0)
        {
            _targetPos += new Vector2(_horizontal, _vertical);
        }
    }
}
