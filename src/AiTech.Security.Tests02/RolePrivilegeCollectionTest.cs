// <copyright file="RolePrivilegeCollectionTest.cs">Copyright ©  2017</copyright>
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AiTech.Security.Tests
{
    /// <summary>This class contains parameterized unit tests for RolePrivilegeCollection</summary>
    [PexClass(typeof(RolePrivilegeCollection))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class RolePrivilegeCollectionTest
    {
        /// <summary>Test stub for Can(String)</summary>
        //[PexMethod]
        //public bool CanTest(
        //    [PexAssumeUnderTest]RolePrivilegeCollection target,
        //    string privilege
        //)
        //{
        //    bool result = target.Can(privilege);
        //    return result;
        //    // TODO: add assertions to method RolePrivilegeCollectionTest.CanTest(RolePrivilegeCollection, String)
        //}

        ///// <summary>Test stub for .ctor(Role)</summary>
        //[PexMethod]
        //public RolePrivilegeCollection ConstructorTest(Role parentRole)
        //{
        //    RolePrivilegeCollection target = new RolePrivilegeCollection(parentRole);
        //    return target;
        //    // TODO: add assertions to method RolePrivilegeCollectionTest.ConstructorTest(Role)
        //}

        /// <summary>Test stub for LoadItemsFromDb()</summary>
        [PexMethod]
        public void LoadItemsFromDbTest([PexAssumeUnderTest]RolePrivilegeCollection target)
        {
            target.LoadItemsFromDb();
            // TODO: add assertions to method RolePrivilegeCollectionTest.LoadItemsFromDbTest(RolePrivilegeCollection)
        }
    }
}
