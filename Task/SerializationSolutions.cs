using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.DB;
using Task.TestHelpers;
using System.Runtime.Serialization;
using System.Linq;
using System.Collections.Generic;
using Task.ForSerialize;

namespace Task
{
	[TestClass]
	public class SerializationSolutions
	{
	    private Northwind _dbContext;

		[TestInitialize]
		public void Initialize()
		{
			_dbContext = new Northwind();
		}

		[TestMethod]
		public void SerializationCallbacks()
		{
			_dbContext.Configuration.ProxyCreationEnabled = false;

			var tester = new XmlDataContractSerializerTester<IEnumerable<Category>>(new NetDataContractSerializer(), true);
			var categories = _dbContext.Categories.Include("Products").ToList();

			var categoriesDeserialize = tester.SerializeAndDeserialize(categories);
		}

		[TestMethod]
		public void ISerializable()
		{
			_dbContext.Configuration.ProxyCreationEnabled = false;

			var tester = new XmlDataContractSerializerTester<IEnumerable<Product>>(new NetDataContractSerializer(), true);
			var products = _dbContext.Products.Include("Order_Details").Include("Category").Include("Supplier").ToList();

			var productsDeserialize = tester.SerializeAndDeserialize(products);
		}


		[TestMethod]
		public void ISerializationSurrogate()
		{
			_dbContext.Configuration.ProxyCreationEnabled = false;

			var tester = new XmlDataContractSerializerTester<IEnumerable<Order_Detail>>(new NetDataContractSerializer(), true);
			var orderDetails = _dbContext.Order_Details.Include("Order").Include("Product").ToList();

			var orderDetailsDeserialize = tester.SerializeAndDeserialize(orderDetails);
		}

		[TestMethod]
		public void IDataContractSurrogate()
		{
			_dbContext.Configuration.ProxyCreationEnabled = true;
			_dbContext.Configuration.LazyLoadingEnabled = true;

            var tester = new XmlDataContractSerializerTester<IEnumerable<Order>>(new DataContractSerializer(typeof(IEnumerable<Order>),new DataContractSerializerSettings
		    {
		        DataContractSurrogate = new OrderSurrogate()
		    }), true);
			var orders = _dbContext.Orders.ToList();
			
            var ordersDeserialize = tester.SerializeAndDeserialize(orders);
		}
	}
}
