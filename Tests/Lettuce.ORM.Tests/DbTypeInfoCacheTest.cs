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
            Assert.AreEqual(expected:"PK",actual:info.PrimaryKeyField.Property.Name,message:"���PrimaryKeyΪPK");

            info = DbTypeInfoCache.GetTypeInfo<PrimaryKeyWithoutAttribute>();
            Assert.AreEqual(expected: "Id", actual: info.PrimaryKeyField.Property.Name, message: "û�б��PrimaryKey ����ȡ����ΪID��");

            info = DbTypeInfoCache.GetTypeInfo<PrimaryKeyWithoutAttributeNoIdProperty>();
            Assert.AreEqual(expected: "PK", actual: info.PrimaryKeyField.Property.Name, message: "û�б��PrimaryKey ����Ҳû��ID�� ȡ��һ��");

            info = DbTypeInfoCache.GetTypeInfo<MultiplePrimaryKeyAttribute>();
            Assert.AreEqual(expected: "Id", actual: info.PrimaryKeyField.Property.Name, message: "������PrimaryKey  ȡ��һ��");

            info = DbTypeInfoCache.GetTypeInfo<ExtendPrimaryKey>();
            Assert.AreEqual(expected: "PK", actual: info.PrimaryKeyField.Property.Name, message: "�̳� ��PrimaryKey ȡPrimaryKey");

            info = DbTypeInfoCache.GetTypeInfo<ExtendPrimaryKeyWithoutAttribute>();
            Assert.AreEqual(expected: "Id", actual: info.PrimaryKeyField.Property.Name, message: "�̳�PrimaryKey");

            info = DbTypeInfoCache.GetTypeInfo<ExtendMultiplePrimaryKeyAttributeInTwoClass>();
            Assert.AreEqual(expected: "PK_OUT", actual: info.PrimaryKeyField.Property.Name, message: "�̳� ����PrimaryKey ȡ����");
        }

        [TestMethod]
        public void FieldInfoTest()
        {
            var info = DbTypeInfoCache.GetTypeInfo<EntityModel>();
            Assert.IsNotNull(value: info);
            DbFieldInfo fieldInfo = info.FieldPropertyInfos.FirstOrDefault(t=>t.Property.Name == "Normal");
            Assert.IsNotNull(value:fieldInfo,message:"��ͨ����");

            fieldInfo = info.FieldPropertyInfos.FirstOrDefault(t => t.Property.Name == "Field");
            Assert.IsNull(value: fieldInfo, message: "��ʶ���ֶ�");

            fieldInfo = info.FieldPropertyInfos.FirstOrDefault(t => t.Property.Name == "PrivateGet");
            Assert.IsNull(value: fieldInfo, message: "private get ");

            fieldInfo = info.FieldPropertyInfos.FirstOrDefault(t => t.Property.Name == "PrivateSet");
            Assert.IsNull(value: fieldInfo, message: "private Set ");

            fieldInfo = info.FieldPropertyInfos.FirstOrDefault(t => t.Property.Name == "PrivateProperty");
            Assert.IsNull(value: fieldInfo, message: "private Property ");




        }

    }
}
