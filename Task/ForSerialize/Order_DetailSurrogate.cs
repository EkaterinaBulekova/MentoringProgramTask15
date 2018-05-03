using System.Reflection;
using System.Runtime.Serialization;
using Task.DB;

namespace Task.ForSerialize
{
    public class OrderDetailSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var orderDetail = (Order_Detail)obj;
            foreach (var property in typeof(Order_Detail).GetProperties(BindingFlags.Instance|BindingFlags.Public))
            {
                info.AddValue(property.Name, property.GetValue(orderDetail));
            }
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var orderDetail = (Order_Detail)obj;
            foreach (var property in typeof(Order_Detail).GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                property.SetValue(orderDetail, info.GetValue(property.Name, property.PropertyType));
            }
            return orderDetail;
        }
    }
}
