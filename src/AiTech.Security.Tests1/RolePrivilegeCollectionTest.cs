// <copyright file="RolePrivilegeCollectionTest.cs">Copyright ©  2017</copyright>
using System;
using AiTech.Security;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AiTech.Security.Tests
{
    /// <summary>This class contains parameterized unit tests for RolePrivilegeCollection</summary>
    [PexClass(typeof(RolePrivilegeCollection))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class RolePrivilegeCollectionTest
    {
        /// <summary>Test stub for LoadItemsFromDb()</summary>
        [PexMethod]
        public void LoadItemsFromDbTest([PexAssumeUnderTest]RolePrivilegeCollection target)
        {
            target.LoadItemsFromDb();
            // TODO: add assertions to method RolePrivilegeCollectionTest.LoadItemsFromDbTest(RolePrivilegeCollection)
        }
    }
}
