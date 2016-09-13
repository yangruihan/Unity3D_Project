using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class AlphaMapper : BaseMeshEffect
{
    public Sprite AlphaSprite;
    private Vector2[] AlphaUV;
    private int[] IndexMap = new int[] { 3, 0, 2, 2, 1, 3 };

    void Start()
    {
        AlphaUV = AlphaSprite.uv;
    }

    public override void ModifyMesh(VertexHelper vh)
    {
        List<UIVertex> list = new List<UIVertex>();
        vh.GetUIVertexStream(list);
        vh.Clear();
        for (int i = 0; i < list.Count; i++)
        {
            var v = list[i];
            v.uv1 = AlphaUV[IndexMap[i]];

            print(v.uv1);

            list[i] = v;
        }
        vh.AddUIVertexTriangleStream(list);
    }
}