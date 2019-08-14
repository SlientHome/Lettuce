using Lettuce.ORM.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lettuce.ORM.Tests.Model
{


    #region PrimaryKeyTestModel
    public class EntityPrimaryKey
    {
        public string Id { get; set; }
        [PrimaryKey]
        public string PK { get; set; }
    }
    public class PrimaryKeyWithoutAttribute
    {
        public string PK { get; set; }
        public string Id { get; set; }
    }
    public class PrimaryKeyWithoutAttributeNoIdProperty
    {
        public string PK { get; set; }
        public string Name { get; set; }
    }
    public class PrimaryKeyStart
    {
        public string PrimaryKeyStartId { get; set; }
        public string Name { get; set; }
    }
    public class MultiplePrimaryKeyAttribute
    {
        [PrimaryKey]
        public string Id { get; set; }
        [PrimaryKey]
        public string PK { get; set; }
    }
    public class ExtendPrimaryKey : EntityPrimaryKey
    {
        public string Id2 { get; set; }
    }
    public class ExtendPrimaryKeyWithoutAttribute : PrimaryKeyWithoutAttribute
    {
        public string Id2 { get; set; }
    }
    public class ExtendMultiplePrimaryKeyAttributeInTwoClass:EntityPrimaryKey
    {
        [PrimaryKey]
        public string PK_OUT { get; set; }
    }
    #endregion

    public class EntityModel
    {
        public string Normal { get; set; }
        public string Field;
        public string PrivateGet { private get; set; }
        public string PrivateSet { get; private set; }
        private string PrivateProperty { get; set; }

    }


}
