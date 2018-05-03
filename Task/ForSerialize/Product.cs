using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Task.DB
{
    [Serializable]
    public sealed partial class Product:ISerializable
    {
        private Product(SerializationInfo information, StreamingContext context)
        {
            var details = (List<Order_Detail>)information.GetValue("Order_Details", typeof(List<Order_Detail>));
            Order_Details = details;
            Discontinued = (bool)information.GetValue("Discontinued", typeof(bool));
            ProductID = (int)information.GetValue("ProductID", typeof(int));
            UnitsInStock = (short?)information.GetValue("UnitsInStock", typeof(short?));
            UnitsOnOrder = (short?)information.GetValue("UnitsOnOrder", typeof(short?));
            ReorderLevel = (short?)information.GetValue("ReorderLevel", typeof(short?));
            UnitPrice = (decimal?)information.GetValue("UnitPrice", typeof(decimal?));
            SupplierID = (int?)information.GetValue("SupplierID", typeof(int?));
            CategoryID = (int?)information.GetValue("CategoryID", typeof(int?));
            QuantityPerUnit = (string)information.GetValue("QuantityPerUnit", typeof(string));
            ProductName = (string)information.GetValue("ProductName", typeof(string));
            Category = (Category)information.GetValue("Category", typeof(Category));
            Supplier = (Supplier)information.GetValue("Supplier", typeof(Supplier));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Order_Details", Order_Details.ToList());
            info.AddValue("Category", Category);
            info.AddValue("Supplier", Supplier);
            info.AddValue("ProductName", ProductName);
            info.AddValue("QuantityPerUnit", QuantityPerUnit);
            info.AddValue("Discontinued", Discontinued);
            info.AddValue("ReorderLevel", ReorderLevel);
            info.AddValue("CategoryID", CategoryID);
            info.AddValue("SupplierID", SupplierID);
            info.AddValue("UnitPrice", UnitPrice);
            info.AddValue("UnitsInStock", UnitsInStock);
            info.AddValue("UnitsOnOrder", UnitsOnOrder);
            info.AddValue("ProductID", ProductID);
        }
    }
}
