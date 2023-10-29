namespace Tarefas.Domain.Utils
{
    public static class Util
    {
        public static Guid ToGuid(this Guid? source)
        {
            return source ?? Guid.Empty;
        }

        public static string EntityToString(this object Entity)
        {
            string[] stringArray = Entity.ToString().Split(".");
            return stringArray[stringArray.Length - 1].ToString();
        }

        public static bool HasNullElements(object[] elements)
        {
            bool HasNullElement = false;

            List<object> NullElements = new List<object>();
            foreach (object x in elements)
            {
                if (x == null)
                {
                    //NullElements.Add(x);
                    HasNullElement = true;
                    break;
                }
            }
            return HasNullElement;
        }

    }
}
