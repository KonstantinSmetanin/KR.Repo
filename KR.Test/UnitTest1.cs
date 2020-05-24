using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using KR.Core.Models;
using KR.Core.Mocks;
using System.Text.Json;
using System.IO;

namespace KR.Test
{

    public class TestData
    {
        public string testDataJsonPath = "C://Users/����������/source/repos/KR/KR.Test/TestData.json";
        public WorkersMock mock = new WorkersMock();

        public TestData()
        {
            mock.ChangeDatabasePath(testDataJsonPath);
            mock.LoadDB();
        }
    }

    [TestClass]
    public class UnitTest1
    {
        public static void AreEqual(object a, object b) =>
            Assert.AreEqual(
                JsonSerializer.Serialize(a),
                JsonSerializer.Serialize(b));

        [TestMethod]
        public void Younger30_Test()
        {
            TestData testData = new TestData();

            var toBe = testData.mock.Take(0, 10);
            var asIs = testData.mock.Younger30;

            AreEqual(toBe, asIs);
        }

        [TestMethod]
        public void GenderDivision_Test()
        {
            TestData testData = new TestData();

            var toBe = new Dictionary<string, double>();
            toBe.Add("Female", (5 * 40 + 7 * 60) / 12.0);
            toBe.Add("Male", (10 * 20 + 3 * 40) / 13.0);

            var asIs = testData.mock.GenderDivision;

            AreEqual(toBe, asIs);
        }

        [TestMethod]
        public void ThisYearEmployedWorkers_Test()
        {
            var testData = new TestData();

            var toBe = testData.mock.Take(0, 13);
            toBe.Reverse();                                     //�� ������� ������ ���� � ������� �������� �������� - �������� toBe
            var asIs = testData.mock.ThisYearEmployedWorkers;

            AreEqual(toBe, asIs);
        }

        [TestMethod]
        public void AgeDivision_Test()
        {
            var testData = new TestData();

            var sequence = new int[3][];
            sequence[0] = new int[] { 0, 10 };
            sequence[1] = new int[] { 10, 8 };
            sequence[2] = new int[] { 18, 7 };

            var toBe = new Dictionary<string, List<Worker>>();
            toBe.Add("������ 30", testData.mock.Take(0, 10));
            toBe.Add("�� 31 �� 50", testData.mock.Take(10, 8));
            toBe.Add("������ 51", testData.mock.Take(18, 7));

            var asIs = testData.mock.AgeDivision;

            AreEqual(toBe, asIs);
        }

        [TestMethod]
        public void EducationDivisionListResult_Test()
        {
            var testData = new TestData();
            var toBe = new Dictionary<string, List<Worker>>();

            toBe.Add("������",                  testData.mock.Take(10, 8));
            toBe.Add("������� �����������",     testData.mock.Take(18, 7));
            toBe.Add("�������",                 testData.mock.Take(0, 5));
            toBe.Add("�������� �������",        testData.mock.Take(5, 3));
            toBe.Add("���������",               testData.mock.Take(8, 2));

            var asIs = testData.mock.EducationDivisionListResult();

            AreEqual(toBe, asIs);
        }
    }
}
