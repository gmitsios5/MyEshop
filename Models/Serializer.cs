using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

namespace MyEshop.Models
{
    public static class Serializer
    {
         /*CREATE NEW SESSION FOR CART LIST*/
        public static List<MyCart>?Deserialize(string? cart)
        {
            if (cart==null)
            {
               return new List<MyCart>();
            }
            else
            {
                return JsonConvert.DeserializeObject<List<MyCart>>(cart);
            }
        }
        /*GET SESSION AND SHOW*/
        public static string Serialize(List<MyCart>? cart)
        {
            return JsonConvert.SerializeObject(cart);
        }
    }
}
