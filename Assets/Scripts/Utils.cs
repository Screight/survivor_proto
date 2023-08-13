using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPointBetween2Rectangles
{
    // Outside Rectangle has to be bigger in order to work correctly
    public static Vector2 GetRandomPoint(Vector2 p_centerPos, Vector2 p_insideRectangleSize, Vector2 p_outsideRectangleSize)
    {
        // Create all 4 rectangles where the points might be formed
        List<Rectangle> rectList = new List<Rectangle>();

        Vector2 rectangleSize;
        rectangleSize.x = p_insideRectangleSize.x;
        rectangleSize.y = (p_outsideRectangleSize.y - p_insideRectangleSize.y) / 2;
        Vector2 centerPos = p_centerPos + Vector2.up * (p_insideRectangleSize.y / 2 + rectangleSize.y / 2);

        rectList.Add(new Rectangle(centerPos, rectangleSize));

        centerPos = p_centerPos - Vector2.up * (p_insideRectangleSize.y / 2 + rectangleSize.y / 2);
        rectList.Add(new Rectangle(centerPos, rectangleSize));

        rectangleSize.x = (p_outsideRectangleSize.x - p_insideRectangleSize.x) / 2;
        rectangleSize.y = p_outsideRectangleSize.y;

        centerPos = p_centerPos + Vector2.right * (p_insideRectangleSize.x / 2 + rectangleSize.x / 2);
        rectList.Add(new Rectangle(centerPos, rectangleSize));

        centerPos = p_centerPos - Vector2.right * (p_insideRectangleSize.x / 2 + rectangleSize.x / 2);
        rectList.Add(new Rectangle(centerPos, rectangleSize));

        // Randomly select one of them while taking into account their areas to make the random dsitribution uniform

        float randomNumber = Random.Range(0.0f, 1.0f);
        float addedChance = 0;
        float totalArea = 0;

        Rectangle selectedRect = rectList[0];

        rectList.ForEach((Rectangle rect) =>
        {
            totalArea += rect.Area();
        });

        foreach(Rectangle rect in rectList)
        {
            addedChance += rect.Area() / totalArea;
            if(randomNumber <= addedChance) { selectedRect = rect; break; }
        }

        return selectedRect.GetRandomPoint();
    }

    struct Rectangle
    {
        Vector2 m_center;
        Vector2 m_size;

        public Rectangle(Vector2 p_center, Vector2 p_size)
        {
            m_center = p_center;
            m_size = p_size;
        }

        public float Area() { return m_size.x * m_size.y; }
        public Vector2 GetRandomPoint() {
            return new Vector2(Random.Range(m_center.x - m_size.x / 2, m_center.x + m_size.x / 2), Random.Range(m_center.y - m_size.y / 2, m_center.y + m_size.y / 2));
        }
    }
}