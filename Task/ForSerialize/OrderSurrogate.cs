using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Task.DB;

namespace Task.ForSerialize
{
    public class OrderSurrogate: IDataContractSurrogate
    {
        public Type GetDataContractType(Type type)
        {
                return type;
        }

        public object GetObjectToSerialize(object obj, Type targetType)
        {
            if (obj is Order order)
            {
                var result = GetClone(order);
                result.Customer = GetClone(order.Customer);
                result.Employee = GetClone(order.Employee);
                result.Shipper = GetClone(order.Shipper);
                result.Order_Details = new List<Order_Detail>(order.Order_Details.Select(GetClone).ToList());
                return result;
            }

            return obj;

        }

        public object GetDeserializedObject(object obj, Type targetType)
        {
            return obj;
        }

        #region NotImplemented
        public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public object GetCustomDataToExport(Type clrType, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
        {
            throw new NotImplementedException();
        }

        public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
        {
            throw new NotImplementedException();
        }

        public CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit)
        {
            throw new NotImplementedException();
        }
        #endregion

        private T GetClone<T>(T obj) where T : new()
        {
            var result = new T();
            typeof(T).GetProperties(BindingFlags.Instance|BindingFlags.Public)
                .Where(_=>!_.GetGetMethod().IsVirtual).ToList()
                .ForEach(_=>_.SetValue(result, _.GetValue(obj)));
            return result;
        }
    }
}
