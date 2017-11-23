using Bb.Sdk;
using Bb.Sdk.Decompiler.IlParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.CodeDom;
using System.Diagnostics;
using System.Text;

namespace Black.Beard.Sdk.Decompiler.UnitTests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestAssign()
        {

            Assert.AreEqual(Method(ClassTest.AssignMultiple), @"var0 = 1;var1 = 1;var2 = 1;var3 = 1;var4 = 1;var5 = 1;var6 = 1;var7 = 1;var8 = 1;var0 = var1;var0 = var2;var0 = var3;var0 = var4;var0 = var5;var0 = var6;var0 = var7;var0 = var8;var7 = var8;");
            Assert.AreEqual(Method(ClassTest.AssignInt0), @"var0 = 0;");
            Assert.AreEqual(Method(ClassTest.AssignTxt), @"var0 = ""test"";");
            Assert.AreEqual(Method(ClassTest.AssignAddTxt), @"var0 = "" test "";var1 = string.Concat(""hello "", var0, ""world"");");
            Assert.AreEqual(Method(ClassTest.AssignInt1), @"var0 = 1;");
            Assert.AreEqual(Method(ClassTest.AssignInt2), @"var0 = 2;");
            Assert.AreEqual(Method(ClassTest.AssignInt3), @"var0 = 3;");
            Assert.AreEqual(Method(ClassTest.AssignInt4), @"var0 = 4;");
            Assert.AreEqual(Method(ClassTest.AssignInt5), @"var0 = 5;");
            Assert.AreEqual(Method(ClassTest.AssignInt6), @"var0 = 6;");
            Assert.AreEqual(Method(ClassTest.AssignInt7), @"var0 = 7;");
            Assert.AreEqual(Method(ClassTest.AssignInt8), @"var0 = 8;");
            Assert.AreEqual(Method(ClassTest.AssignInt101), @"var0 = 101;");
            Assert.AreEqual(Method(ClassTest.AssignIntmax), @"var0 = 2147483647;");
            Assert.AreEqual(Method(ClassTest.AssignLong0), @"var0 = ((long)(0));");
            Assert.AreEqual(Method(ClassTest.Assignlong), @"var0 = ((long)(-2147483549));");
            Assert.AreEqual(Method(ClassTest.AssignDecimal), @"var0 = 21474.83747m;");
            Assert.AreEqual(Method(ClassTest.AssignShort), @"var0 = 6;");
            Assert.AreEqual(Method(ClassTest.AssignFloat), @"var0 = 1F;");
            Assert.AreEqual(Method(ClassTest.AssignDouble), @"var0 = 1D;");
            Assert.AreEqual(Method(ClassTest.AssignValueType), @"var0 = new System.DateTime(2017, 2, 10);");
            Assert.AreEqual(Method(ClassTest.AssignMultiValueType), @"var0 = new System.DateTime(2017, 2, 10);var1 = new System.DateTime(2017, 2, 10);var2 = new System.DateTime(2017, 2, 10);var3 = new System.DateTime(2017, 2, 10);var4 = new System.DateTime(2017, 2, 10);var5 = new System.DateTime(2017, 2, 10);var6 = new System.DateTime(2017, 2, 10);var7 = new System.DateTime(2017, 2, 10);var8 = new System.DateTime(2017, 2, 10);");
        }

        [TestMethod]
        public void TestOperators()
        {
            Assert.AreEqual(Method(ClassTest.AssignAdd), @"var0 = 1;var0 = (1 + var0);");
            Assert.AreEqual(Method(ClassTest.AssignSubstract), @"var0 = 1;var1 = (1 - var0);");
            Assert.AreEqual(Method(ClassTest.AssignDivide), @"var0 = 1;var1 = (1 / var0);");
            Assert.AreEqual(Method(ClassTest.AssignModulo), @"var0 = 1;var0 = (var0 % 5);");
            Assert.AreEqual(Method(ClassTest.AssignTimes), @"var0 = 1;var1 = (2 * var0);");
        }

        [TestMethod]
        public void TestCallMethods()
        {
            Assert.AreEqual(Method(ClassTest.CallOperator), @"var1 = new System.DateTime(2017, 2, 10);var0 = var1;");
            Assert.AreEqual(Method(ClassTest.CallOperatorWithTypeof), @"var0 = System.Convert.ChangeType(((int)(1)), typeof(string));");
            Assert.AreEqual(Method(ClassTest.CallOperatorWithOutWay), @"var1 = int.TryParse(""6"", out var0);");
        }

        [TestMethod]
        public void TestIf()
        {
            Assert.AreEqual(Method(ClassTest.IfEqual), @"var0 = 10;var1 = (var0 == 0);if (var1) {    var0 = 2;}else {    var0 = 4;}");
            // Assert.AreEqual(Method(ClassTest.IfPlusGrand), @"var0 = 10;var1 = (var0 > 0);if (var1) {    var0 = 2;}else {    var0 = 4;}");
            //Assert.AreEqual(Method(ClassTest.IfPlusGrandOuEgale), @"var0 = 10;var1 = (var0 >= 0);if (var1) {    var0 = 2;}else {    var0 = 4;}");
            //Assert.AreEqual(Method(ClassTest.IfPlusGrandOuEgale2), @"var0 = 10;var1 = (var0 >= 25);if (var1) {    var0 = 2;}else {    var0 = 4;}");
            //Assert.AreEqual(Method(ClassTest.IfPlusGrandOuEgale3), @"var1 = (Black.Beard.Sdk.Decompiler.UnitTests.ClassTest.Test(""a"") >= Black.Beard.Sdk.Decompiler.UnitTests.ClassTest.Test(""b""));if (var1) {    var0 = 2;}else {    var0 = 4;}");
            //Assert.AreEqual(Method(ClassTest.IfPlusPetit), @"var0 = 10;var1 = (var0 < 0);if (var1) {    var0 = 2;}else {    var0 = 4;}");
            //Assert.AreEqual(Method(ClassTest.IfPlusPetitOuEgale), @"var0 = 10;var1 = (var0 <= 0);if (var1) {    var0 = 2;}else {    var0 = 4;}");
            //Assert.AreEqual(Method(ClassTest.IfPlusPetitOuEgale2), @"var0 = 10;var1 = (var0 <= 25);if (var1) {    var0 = 2;}else {    var0 = 4;}");
            //Assert.AreEqual(Method(ClassTest.If2), @"var0 = 0;var2 = int.TryParse("6", out var1);if (var2) {    var0 = 2;}else {    var0 = 4;}");
            //Assert.AreEqual(Method(ClassTest.IfAnd), @"var0 = 10;var1 = 15;var2 = ((var0 & var1) == 10);if (var2) {    var0 = 2;}else {    var0 = 4;}");
            //Assert.AreEqual(Method(ClassTest.IfOr), @"var0 = 10;var1 = 15;var2 = ((var0 | var1) == 10);if (var2) {    var0 = 2;}else {    var0 = 4;}");
            //Assert.AreEqual(Method(ClassTest.IfEqualWithAnd), @"var0 = 10;var1 = 10;var2 = ((var0 == 0) & (var1 == 10));if (var2) {    var0 = 2;}else {    var0 = 4;}");
            //Assert.AreEqual(Method(ClassTest.IfEqualWithOr), @"var0 = 10;var1 = 10;var2 = ((var0 == 0) | (var1 == 10));if (var2) {    var0 = 2;}else {    var0 = 4;}");
            Assert.AreEqual(Method(ClassTest.IfEqualWithAndAlso), @"");
            Assert.AreEqual(Method(ClassTest.IfEqualWithXOr), @"");


        }

        [TestMethod]
        public void TestTryCatchs()
        {

            Assert.AreEqual(Method(ClassTest.TestTry), @"");

        }

        [TestMethod]
        public void TestWhile()
        {

        }


        private static string Method(Action action)
        {
            CodeMemberMethod method = action.Method.GetSourceCode();
            StringBuilder sb = new StringBuilder();
            foreach (var item in method.Statements)
                sb.Append(item.ToString());
            var txt = sb.ToString();
            Debug.WriteLine(txt);
            txt = txt.Trim().Replace("\r", "").Replace("\n", "");
            return txt;
        }


    }
}
