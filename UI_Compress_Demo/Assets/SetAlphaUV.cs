using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class SetAlphaUV : BaseMeshEffect
{
    private Sprite AlphaSprite;
    private Vector2[] RGBUV;
    private Vector2[] AlphaUV;
    private int[] IndexMap = new int[] { 3, 0, 2, 2, 1, 3 };

    void Start()
    {
        AlphaSprite = GetComponent<SpriteRenderer>().sprite;

        RGBUV = new Vector2[AlphaSprite.uv.Length];
        AlphaUV = new Vector2[AlphaSprite.uv.Length];

        float _maxHeight = 0f;
        float _minHeight = float.MaxValue;

        for (int i = 0; i < AlphaUV.Length; i++)
        {
            RGBUV[i] = AlphaSprite.uv[i];
            AlphaUV[i] = AlphaSprite.uv[i];
            
            if (_maxHeight < AlphaUV[i].y)
            {
                _maxHeight = AlphaUV[i].y;
            }

            if (_minHeight > AlphaUV[i].y)
            {
                _minHeight = AlphaUV[i].y;
            }
        }

        for (int i = 0; i < AlphaUV.Length; i++)
        {
            if (AlphaUV[i].y == _minHeight)
            {
                AlphaUV[i].y = (_minHeight + _maxHeight) / 2f;
            }

            if (RGBUV[i].y == _maxHeight)
            {
                RGBUV[i].y = (_minHeight + _maxHeight) / 2f;
            }
        }
    }

    public override void ModifyMesh(VertexHelper vh)
    {
        List<UIVertex> list = new List<UIVertex>();
        vh.GetUIVertexStream(list);
        vh.Clear();
        for (int i = 0; i < list.Count; i++)
        {
            var v = list[i];
            v.uv0 = RGBUV[IndexMap[i]];
            v.uv1 = AlphaUV[IndexMap[i]];
            list[i] = v;
        }
        vh.AddUIVertexTriangleStream(list);
    }
}
