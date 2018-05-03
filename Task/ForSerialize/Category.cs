using System.Linq;

namespace Task.DB
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Threading;

    [Serializable]
    public partial class Category
    {
        [OnSerializing]
        public void OnSerializing(StreamingContext context)
        {
            var slot = Thread.AllocateNamedDataSlot("Products" + CategoryID);
            Thread.SetData(slot, Products.ToList());
        }

        [OnSerialized]
        public void OnSerialized(StreamingContext context)
        {
            var slot = Thread.GetNamedDataSlot("Products" + CategoryID);
            Products = (List<Product>)Thread.GetData(slot);
        }


        [OnDeserialized]
        public void OnDeserialized(StreamingContext context)
        {
            Products = Products;
        }
    }
}
