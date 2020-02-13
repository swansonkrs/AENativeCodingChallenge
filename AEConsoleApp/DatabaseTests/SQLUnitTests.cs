using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseTests
{
    [TestClass()]
    public class SQLUnitTests : SqlDatabaseTestClass
    {

        public SQLUnitTests()
        {
            InitializeComponent();
        }

        [TestInitialize()]
        public void TestInitialize()
        {
            base.InitializeTest();
        }
        [TestCleanup()]
        public void TestCleanup()
        {
            base.CleanupTest();
        }

        #region Designer support code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_sp_recent_responsesTest_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLUnitTests));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_sp_recent_responsesTest_PretestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction testInitializeAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_sp_recent_responsesTest_PosttestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction vw_avg_per_hourTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition2;
            this.dbo_sp_recent_responsesTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.vw_avg_per_hourTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            dbo_sp_recent_responsesTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            dbo_sp_recent_responsesTest_PretestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            testInitializeAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            rowCountCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            dbo_sp_recent_responsesTest_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            vw_avg_per_hourTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            rowCountCondition2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            // 
            // dbo_sp_recent_responsesTest_TestAction
            // 
            dbo_sp_recent_responsesTest_TestAction.Conditions.Add(rowCountCondition1);
            resources.ApplyResources(dbo_sp_recent_responsesTest_TestAction, "dbo_sp_recent_responsesTest_TestAction");
            // 
            // dbo_sp_recent_responsesTestData
            // 
            this.dbo_sp_recent_responsesTestData.PosttestAction = dbo_sp_recent_responsesTest_PosttestAction;
            this.dbo_sp_recent_responsesTestData.PretestAction = dbo_sp_recent_responsesTest_PretestAction;
            this.dbo_sp_recent_responsesTestData.TestAction = dbo_sp_recent_responsesTest_TestAction;
            // 
            // dbo_sp_recent_responsesTest_PretestAction
            // 
            resources.ApplyResources(dbo_sp_recent_responsesTest_PretestAction, "dbo_sp_recent_responsesTest_PretestAction");
            // 
            // testInitializeAction
            // 
            resources.ApplyResources(testInitializeAction, "testInitializeAction");
            // 
            // rowCountCondition1
            // 
            rowCountCondition1.Enabled = true;
            rowCountCondition1.Name = "rowCountCondition1";
            rowCountCondition1.ResultSet = 1;
            rowCountCondition1.RowCount = 5;
            // 
            // dbo_sp_recent_responsesTest_PosttestAction
            // 
            resources.ApplyResources(dbo_sp_recent_responsesTest_PosttestAction, "dbo_sp_recent_responsesTest_PosttestAction");
            // 
            // vw_avg_per_hourTestData
            // 
            this.vw_avg_per_hourTestData.PosttestAction = null;
            this.vw_avg_per_hourTestData.PretestAction = null;
            this.vw_avg_per_hourTestData.TestAction = vw_avg_per_hourTest_TestAction;
            // 
            // vw_avg_per_hourTest_TestAction
            // 
            vw_avg_per_hourTest_TestAction.Conditions.Add(rowCountCondition2);
            resources.ApplyResources(vw_avg_per_hourTest_TestAction, "vw_avg_per_hourTest_TestAction");
            // 
            // rowCountCondition2
            // 
            rowCountCondition2.Enabled = true;
            rowCountCondition2.Name = "rowCountCondition2";
            rowCountCondition2.ResultSet = 1;
            rowCountCondition2.RowCount = 3;
            // 
            // SQLUnitTests
            // 
            this.TestInitializeAction = testInitializeAction;
        }

        #endregion


        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        #endregion

        [TestMethod()]
        public void dbo_sp_recent_responsesTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_sp_recent_responsesTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }
        [TestMethod()]
        public void vw_avg_per_hourTest()
        {
            SqlDatabaseTestActions testActions = this.vw_avg_per_hourTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        private SqlDatabaseTestActions dbo_sp_recent_responsesTestData;
        private SqlDatabaseTestActions vw_avg_per_hourTestData;
    }
}
