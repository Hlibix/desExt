using UnityEngine;

namespace desExt.Editor.Utils
{
    public static class RectUtils
    {
        public static Rect MoveRight(this Rect rect)
        {
            return rect.MoveRight(110);
        }

        public static Rect MoveRight(this Rect rect, float moveValue)
        {
            return new Rect(rect.x + moveValue, rect.y, rect.width, rect.height);
        }

        public static Rect SetWidth(this Rect rect, float width)
        {
            return new Rect(rect.x, rect.y, width, rect.height);
        }

        public static Rect ShrinkRight(this Rect rect, float shrinkValue)
        {
            return new Rect(rect.x, rect.y, rect.width - shrinkValue, rect.height);
        }

        public static Rect ShrinkY(this Rect rect, float shrinkValue)
        {
            return new Rect(rect.x, rect.y, rect.width, rect.height / shrinkValue);
        }

        public static Rect MoveY(this Rect rect, float moveValue)
        {
            return new Rect(rect.x, rect.y + moveValue, rect.width, rect.height);
        }
    }
}