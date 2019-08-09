using Lettuce.ORM.Model;
using Lettuce.ORM.Tests.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Lettuce.ORM.Tests
{
    [TestClass]
    public class DbTypeInfoCacheTest
    {
        [TestMethod]
        public void PrimaryKeyTest()
        {
            
            var info = DbTypeInfoCache.GetTypeInfo<EntityPrimaryKey>();
            Assert.IsNotNull(value: info);
            Assert.AreEqual(expected:"PK",actual:info.PrimaryKeyField.Property.Name,message:"标记PrimaryKey为PK");

            info = DbTypeInfoCache.GetTypeInfo<PrimaryKeyWithoutAttribute>();
            Assert.AreEqual(expected: "Id", actual: info.PrimaryKeyField.Property.Name, message: "没有标记PrimaryKey 优先取名称为ID的");

            info = DbTypeInfoCache.GetTypeInfo<PrimaryKeyWithoutAttributeNoIdProperty>();
            Assert.AreEqual(expected: "PK", actual: info.PrimaryKeyField.Property.Name, message: "没有标记PrimaryKey 名称也没有ID的 取第一个");

            info = DbTypeInfoCache.GetTypeInfo<MultiplePrimaryKeyAttribute>();
            Assert.AreEqual(expected: "Id", actual: info.PrimaryKeyField.Property.Name, message: "多个标记PrimaryKey  取第一个");

            info = DbTypeInfoCache.GetTypeInfo<ExtendPrimaryKey>();
            Assert.AreEqual(expected: "PK", actual: info.PrimaryKeyField.Property.Name, message: "继承 有PrimaryKey 取PrimaryKey");

            info = DbTypeInfoCache.GetTypeInfo<ExtendPrimaryKeyWithoutAttribute>();
            Assert.AreEqual(expected: "Id", actual: info.PrimaryKeyField.Property.Name, message: "继承PrimaryKey");

            info = DbTypeInfoCache.GetTypeInfo<ExtendMultiplePrimaryKeyAttributeInTwoClass>();
            Assert.AreEqual(expected: "PK_OUT", actual: info.PrimaryKeyField.Property.Name, message: "继承 都有PrimaryKey 取子类");
        }

        [TestMethod]
        public void FieldInfoTest()
        {
            var info = DbTypeInfoCache.GetTypeInfo<EntityModel>();
            Assert.IsNotNull(value: info);
            DbFieldInfo fieldInfo = info.FieldPropertyInfos.FirstOrDefault(t=>t.Property.Name == "Normal");
            Assert.IsNotNull(value:fieldInfo,message:"普通属性");

            fieldInfo = info.FieldPropertyInfos.FirstOrDefault(t => t.Property.Name == "Field");
            Assert.IsNull(value: fieldInfo, message: "不识别字段");

            fieldInfo = info.FieldPropertyInfos.FirstOrDefault(t => t.Property.Name == "PrivateGet");
            Assert.IsNull(value: fieldInfo, message: "private get ");

            fieldInfo = info.FieldPropertyInfos.FirstOrDefault(t => t.Property.Name == "PrivateSet");
            Assert.IsNull(value: fieldInfo, message: "private Set ");

            fieldInfo = info.FieldPropertyInfos.FirstOrDefault(t => t.Property.Name == "PrivateProperty");
            Assert.IsNull(value: fieldInfo, message: "private Property ");




        }

    }
}
