using System;
using System.Collections.Generic;
using System.Text;

namespace Black.Beard.Sdk.Decompiler.UnitTests
{


    public class ClassTest
    {

        #region Assign

        public static void AssignTxt()
        {
            var txt = "test";
        }

        public static void AssignAddTxt()
        {
            string a = " test ";
            var txt = "hello " + a + "world";
        }

        public static void AssignInt0()
        {
            var i = 0;
        }

        public static void AssignInt1()
        {
            var i = 1;
        }

        public static void AssignInt2()
        {
            var i = 2;
        }

        public static void AssignInt3()
        {
            var i = 3;
        }

        public static void AssignInt4()
        {
            var i = 4;
        }

        public static void AssignInt5()
        {
            var i = 5;
        }

        public static void AssignInt6()
        {
            var i = 6;
        }

        public static void AssignInt7()
        {
            var i = 7;
        }

        public static void AssignInt8()
        {
            var i = 8;
        }

        public static void AssignInt101()
        {
            var i = 101;
        }

        public static void AssignIntmax()
        {
            var i = 2147483647;
        }

        public static void AssignLong0()
        {
            var i = 0L;
        }

        public static void AssignShort()
        {
            Int16 i = 6;
        }

        public static void Assignlong()
        {
            var i = 2147483747L;
        }

        public static void AssignDecimal()
        {
            var i = 21474.83747m;
        }

        public static void AssignAdd()
        {
            int a = 1;
            a = a + 1;
        }

        public static void AssignSubstract()
        {
            int a = 1;
            var i = a - 1;
        }

        public static void AssignDivide()
        {
            int a = 1;
            var i = a / 1;
        }

        public static void AssignModulo()
        {
            int a = 1;
            a = 5 % a;
        }

        public static void AssignMultiple()
        {
            var var0 = 1;
            var var1 = 1;
            var var2 = 1;
            var var3 = 1;
            var var4 = 1;
            var var5 = 1;
            var var6 = 1;
            var var7 = 1;
            var var8 = 1;

            var0 = var1;
            var0 = var2;
            var0 = var3;
            var0 = var4;
            var0 = var5;
            var0 = var6;
            var0 = var7;
            var0 = var8;
            var7 = var8;
        }

        public static void AssignFloat()
        {
            var i = 1F;
        }

        public static void AssignDouble()
        {
            var i = 1D;
        }

        public static void AssignValueType()
        {
            var var0 = new DateTime(2017, 2, 10);
        }

        public static void AssignMultiValueType()
        {
            var var0 = new DateTime(2017, 2, 10);
            var var1 = new DateTime(2017, 2, 10);
            var var2 = new DateTime(2017, 2, 10);
            var var3 = new DateTime(2017, 2, 10);
            var var4 = new DateTime(2017, 2, 10);
            var var5 = new DateTime(2017, 2, 10);
            var var6 = new DateTime(2017, 2, 10);
            var var7 = new DateTime(2017, 2, 10);
            var var8 = new DateTime(2017, 2, 10);
        }

        public static void AssignTimes()
        {
            int a = 1;
            var i = a * 2;
        }

        #endregion Assign

        public static void CallOperator()
        {
            var var0 = new DateTime(2017, 2, 10).AddHours(1);
        }

        public static void CallOperatorWithTypeof()
        {
            var str = Convert.ChangeType(1, typeof(string));
        }

        public static void CallOperatorWithOutWay()
        {
            int i;
            var str = int.TryParse("6", out i);
        }


        public static void IfEqual()
        {
            int i = 10;
            if (i == 0)
                i = 2;
            else
                i = 4;
        }

        public static void IfPlusGrand()
        {
            int i = 10;
            if (i > 0)
                i = 2;
            else
                i = 4;
        }

        public static void IfPlusPetit()
        {
            int i = 10;
            if (i < 0)
                i = 2;
            else
                i = 4;
        }

        public static void IfPlusGrandOuEgale()
        {

            /*
            0 < 0 -> 0 == 0 -> 1             0 >= 0 -> 1
            1 < 1 -> 0 == 0 -> 1             1 >= 1 -> 1
            0 < 1 -> 1 == 0 -> 0             0 >= 1 -> 0
            1 < 0 -> 0 == 0 -> 1             1 >= 0 -> 1
            */

            int i = 10;
            if (i >= 0)
                i = 2;
            else
                i = 4;
        }

        public static void IfPlusGrandOuEgale2()
        {

            /*
            10 < 25 -> 1 == 0 -> 0      10 >= 25 -> 0
            25 < 10 -> 0 == 0 -> 1      25 >= 10 -> 1
            10 < 10 -> 0 == 0 -> 1      10 >= 10 -> 1
            25 < 10 -> 0 == 0 -> 1      25 >= 10 -> 1
            */

            int i = 10;
            if (i >= 25)
                i = 2;
            else
                i = 4;
        }

        public static void IfPlusGrandOuEgale3()
        {
            
            /*
            10 < 25 -> 1 == 0 -> 0      10 >= 25 -> 0
            25 < 10 -> 0 == 0 -> 1      25 >= 10 -> 1
            10 < 10 -> 0 == 0 -> 1      10 >= 10 -> 1
            25 < 10 -> 0 == 0 -> 1      25 >= 10 -> 1
            */

            int i;
            if (Test("a") >= Test("b"))
                i = 2;
            else
                i = 4;
        }

        public static void IfPlusPetitOuEgale()
        {

            /*
            0 > 0 -> 0 == 0 -> 1          0 <= 0 -> 1
            1 > 1 -> 0 == 0 -> 1          1 <= 1 -> 1
            0 > 1 -> 0 == 0 -> 1          0 <= 1 -> 1
            1 > 0 -> 1 == 0 -> 0          1 <= 0 -> 0
            */

            int i = 10;
            if (i <= 0)
                i = 2;
            else
                i = 4;
        }

        public static void IfPlusPetitOuEgale2()
        {

            /*
            10 > 25 -> 0 == 0 -> 1      10 <= 25 -> 1
            25 > 10 -> 1 == 0 -> 0      25 <= 10 -> 0
            10 > 10 -> 0 == 0 -> 1      10 <= 10 -> 1
            25 > 10 -> 1 == 0 -> 0      25 <= 10 -> 0
            */


            int i = 10;
            if (i <= 25)
                i = 2;
            else
                i = 4;
        }

        public static void If2()
        {
            int i = 0;
            int a;
            if (int.TryParse("6", out a))
                i = 2;
            else
                i = 4;
        }

        public static void IfAnd()
        {
            int i = 10;
            int a = 15;
            if ((i & a) == 10)
                i = 2;
            else
                i = 4;
        }

        public static void IfOr()
        {
            int i = 10;
            int a = 15;
            if ((i | a) == 10)
                i = 2;
            else
                i = 4;
        }

        public static void IfEqualWithAnd()
        {
            int i = 10;
            int j = 10;
            if (i == 0 & j == 10)
                i = 2;
            else
                i = 4;
        }

        public static void IfEqualWithOr()
        {
            int i = 10;
            int j = 10;
            if (i == 0 | j == 10)
                i = 2;
            else
                i = 4;
        }

        public static void IfEqualWithAndAlso()
        {
            int i = 10;
            int j = 10;

            if (i == j && j == 10)
                i = 2;
            else
                i = 4;
        }

        public static void IfEqualWithXOr()
        {

            //IL_0000: nop
            //IL_0001: ldc.i4.s   10
            //IL_0003: stloc.0
            //IL_0004: ldc.i4.s   10
            //IL_0006: stloc.1

            //IL_0007: ldloc.0
            //IL_0008: ldloc.1
            //IL_0009: bne.un.s IL_0012
            
            //IL_000b: ldloc.1
            //IL_000c: ldc.i4.s   10
            //IL_000e: ceq
            //IL_0010: br.s IL_0013
            //IL_0012: ldc.i4.0
            //IL_0013: stloc.2

            //IL_0014: ldloc.2
            //IL_0015: brfalse.s IL_001b
            //IL_0017: ldc.i4.2
            //IL_0018: stloc.0
            //IL_0019: br.s IL_001d
            //IL_001b: ldc.i4.4
            //IL_001c: stloc.0
            //IL_001d: ret

            int i = 10;
            int j = 10;
            if (i == 0 || j == 10)
                i = 2;
            else
                i = 4;
        }





        public static void TestTry()
        {
            int var0 = 0;
            try
            {
                var0 = Test("toto");
            }
            catch (ExecutionEngineException e2)
            {
                throw e2;
            }
            catch (SystemException e1)
            {
                throw e1;
            }
            catch (Exception e3)
            {
                throw e3;
            }

            if (var0 ==0)
            {
                throw new Exception();
            }

        }

        private static int Test(string name)
        {
            return name.GetHashCode();
        }

    }


}
