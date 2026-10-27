
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using Microsoft.IdentityModel.Claims;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace EdwardsIndexAppDataStoreService
{
    public static class DataStoreFactory
    {
        static bool forceCreateSchema;

        public static bool ForceCreateSchema
        {
            get
            {
                return DataStoreFactory.forceCreateSchema;
            }
            set
            {
                DataStoreFactory.forceCreateSchema = value;
            }
        }

        public static IDataStore CreateDataStore()
        {
            IDataStore dataStoreSqLite;
            forceCreateSchema = false;
            try
            {
				
			
                if (!Directory.Exists(@"C:\EdwardsIndexAppDataStoreService"))
                {
                    Directory.CreateDirectory(@"C:\EdwardsIndexAppDataStoreService");
                    forceCreateSchema = true;
                }

                dataStoreSqLite = DevExpress.Xpo.XpoDefault.GetConnectionProvider(

                DevExpress.Xpo.DB.SQLiteConnectionProvider.GetConnectionString(Path.Combine(@"C:\EdwardsIndexAppDataStoreService", @"EdwardsIndexApp.db3")),

                DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            }
            catch (Exception e)
            {
                Trace.WriteLine("GetConnectionProvider could not get created: " + e.Message);
                throw;
            }

            return dataStoreSqLite;
        }
    }

    //<sbStartService>
    [ServiceBehaviorAttribute(InstanceContextMode = InstanceContextMode.Single)]
    public class EdwardsIndexAppDataStore : DataStoreService
    {
		public EdwardsIndexAppDataStore(IDataStore dataStore)
            : base(dataStore)
		{
		}
		
        public EdwardsIndexAppDataStore()
            : base(DataStoreFactory.CreateDataStore())
        {
            if (DataStoreFactory.ForceCreateSchema)
            {

					XPDictionary dict = new ReflectionDictionary();
					DBTable[] tables = dict.GetDataStoreSchema(typeof(EdwardsIndexAppCommon.XPO.IndexMarker).Assembly); //get all persistent classes from the assembly
					UpdateSchema(false, tables);

            }
        }
		
		protected InsertStatement CreateInsertOwnershipEntryStatement(string tableName, string ownershipColumn, string linkedColumn, int ownershipId, OperationResult<ModificationResult> result) {
            var insertStatement_user_anwendungen = new InsertStatement();
            insertStatement_user_anwendungen.Table.Name = tableName;
            insertStatement_user_anwendungen.Operands.Add(new QueryOperand
            {
                        ColumnName = ownershipColumn,
                        ColumnType = DBColumnType.Int32
            });
            insertStatement_user_anwendungen.Operands.Add(new QueryOperand
            {
                        ColumnName = linkedColumn,
                        ColumnType = DBColumnType.Int32
            });

            insertStatement_user_anwendungen.Parameters.Add(new OperandValue
            {
                        Value = ownershipId,
                        XmlValue = ownershipId
            });

            insertStatement_user_anwendungen.Parameters.Add(new OperandValue
            {
                        Value = result.Result.Identities.First().Value,
                        XmlValue = result.Result.Identities.First().Value
            });

            insertStatement_user_anwendungen.IdentityColumn = "ID";
            insertStatement_user_anwendungen.IdentityColumnType = DBColumnType.Int32;
            insertStatement_user_anwendungen.IdentityParameter = new ParameterValue();
            insertStatement_user_anwendungen.IdentityParameter.Tag = 0;
            insertStatement_user_anwendungen.IdentityParameter.Value = null;

            return insertStatement_user_anwendungen;
		}
		
        public override OperationResult<ModificationResult> ModifyData(ModificationStatement[] dmlStatements)
        {
            List<ModificationStatement> interceptedStatements = new List<ModificationStatement>();

            foreach (var stmt in dmlStatements)
            {
                if (stmt is InsertStatement)
                {
					// Ctreate links for entities who are linked directly to the ownership entity
                    
					switch (stmt.Table.Name)
                    {

                        default:
                            interceptedStatements.Add(stmt);
                            break;
                    }
                }
                else
                {
                    interceptedStatements.Add(stmt);
                }
            }

            var result = base.ModifyData(interceptedStatements.ToArray());

            return result;

        }

        // https://www.devexpress.com/Support/Center/Question/Details/Q266294
        public override OperationResult<SelectedData> SelectData(SelectStatement[] selects)
        {
            Trace.WriteLine(string.Format("SelectData called"));
            if (selects != null)
            {
                Trace.WriteLine(string.Format("SelectData for {0} statements", selects.Count()));

                int AliasNumber = 0;

                foreach (var sel in selects)
                {
                    AppendSecurityFilter(sel, ref AliasNumber);

                    string select = "";

                    try
                    {
                        select = sel.ToString();
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine(string.Format("Obtaining select statement failed: ", e));
                    }

                    Trace.WriteLine(string.Format("SelectData for statement produced SQL: {0}", select));
                }
            }

            try
            {
                return base.SelectData(selects);
            }
            catch (Exception e)
            {
                Trace.WriteLine(string.Format("SelectData failed: ", e));
                throw;
            }
        }

        public override OperationResult<byte[]> WarpSelectData(SelectStatement[] selects)
        {
            Trace.WriteLine(string.Format("WarpSelectData called"));
            if (selects != null)
            {
                Trace.WriteLine(string.Format("WarpSelectData for {0} statements", selects.Count()));

                int AliasNumber = 1;

                foreach (var sel in selects)
                {
                    AppendSecurityFilter(sel, ref AliasNumber);

                    string select = "";

                    try
                    {
                        select = sel.ToString();
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine(string.Format("Obtaining select statement failed: ", e));
                    }

                    Trace.WriteLine(string.Format("WarpSelectData for statement produced SQL: {0}", select));
                }
            }

            try
            {
                return base.WarpSelectData(selects);
            }
            catch (Exception e)
            {
                Trace.WriteLine(string.Format("SelectData failed: ", e));
                throw;
            }
        }

        private CriteriaOperator CreateSubQueryOwnerFilter(ref int AliasNumber, JoinNode statement, string innerTableName, string innerTableAlias, string innerOperandColumn, string outerOperandColumn, string filterCriteriaColumn = null, int? filterCriteriaValue = null)
        {
            var innerStatement = CreateInnerSelect(statement,
                                                   innerTableName, innerTableAlias,
                                                   outerOperandColumn, innerOperandColumn,
                                                   filterCriteriaColumn, filterCriteriaValue);

            CriteriaOperator additonalCondition = new QuerySubQueryContainer(innerStatement, null, Aggregate.Exists);

            AliasNumber++;

            if (!filterCriteriaValue.HasValue)
                AppendSecurityFilter(innerStatement, ref AliasNumber);

            return additonalCondition;
        }

        private CriteriaOperator CreateSecurityFilter(JoinNode statement, ref int AliasNumber)
        {
            switch (statement.Table.Name)
            {

                default:
                    return null;
			}
        }

        private static SelectStatement CreateInnerSelect(JoinNode statement, 
			string innerTableName, string innerTableAlias, 
			string outerOperandColumn, string innerOperandColumn,
			string filterCriteriaColumn, int? filterCriteriaValue)
        {
            var innerStatement = new SelectStatement();

            innerStatement.Alias = innerTableAlias;
            innerStatement.Table.Name = innerTableName;

            var innerOperand = new QueryOperand(innerOperandColumn, innerStatement.Alias);
            var outerOperand = new QueryOperand(outerOperandColumn, statement.Alias);

            GroupOperator innerSelectCondition = new GroupOperator(GroupOperatorType.And);

            if (filterCriteriaValue.HasValue)
            {
                innerSelectCondition.Operands.Add(new BinaryOperator(new QueryOperand(filterCriteriaColumn, innerStatement.Alias), new ConstantValue(filterCriteriaValue), BinaryOperatorType.Equal));
            }

            innerSelectCondition.Operands.Add(new BinaryOperator(innerOperand, outerOperand, BinaryOperatorType.Equal));

            innerStatement.Condition = innerSelectCondition;

            return innerStatement;
        }
		
        private void AppendSecurityFilter(JoinNode sel, ref int AliasNumber)
        {
            try
            {
                CriteriaOperator securityFilter = CreateSecurityFilter(sel, ref AliasNumber);

                if (securityFilter != null)
                {
                    if (sel.Condition == null)
                    {
                        Trace.WriteLine(string.Format("Apply first securityFilter: {0}, JoinType: {1}", securityFilter.LegacyToString(), sel.Type));
                        sel.Condition = securityFilter;
                    }
                    else
                    {
                        Trace.WriteLine(string.Format("Apply another securityFilter: {0}", securityFilter.LegacyToString()));
                        sel.Condition = new GroupOperator(GroupOperatorType.And, sel.Condition, securityFilter);
                    }
                }

                foreach (var subNode in sel.SubNodes)
                {
                    AppendSecurityFilter(subNode, ref AliasNumber);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(string.Format("AppendSecurityFilter failed: {0}", e));
            }
        }
    }
//EdwardsIndexApp

    public class EdwardsIndexAppDataStoreClaimsAuthorizationManager : ClaimsAuthorizationManager  
    {
        public EdwardsIndexAppDataStoreClaimsAuthorizationManager() 
        {

        }

        public override bool CheckAccess(Microsoft.IdentityModel.Claims.AuthorizationContext context)
        {
            return base.CheckAccess(context);
        }
    }

    public class EdwardsIndexAppDataStoreServiceAuthorizationManager : IdentityModelServiceAuthorizationManager
    {
        IClaimsIdentity GetClaimsIdentity(OperationContext operationContext) 
        {
            ServiceSecurityContext securityContext = operationContext.ServiceSecurityContext;

            var identity = operationContext.ServiceSecurityContext.PrimaryIdentity as IClaimsIdentity;
            if (identity == null)
            {
                // If there is more than 1 identity, for example if there is also a certificate then PrimaryIdentity will be null.
                if (securityContext.AuthorizationContext.Properties.ContainsKey("Principal"))
                {
                    var principal = securityContext.AuthorizationContext.Properties["Principal"] as IClaimsPrincipal;
                    if (principal != null)
                    {
                        identity = principal.Identity as IClaimsIdentity;
                    }
                }

                if (identity == null)
                {
                    //throw new InvalidOperationException("PrimaryIdentity identity is not an IClaimsIdentity");
                }
            }

            return identity;
        }

        // Override AccessCheck to enforce access control requirements.
        public override bool CheckAccess(OperationContext operationContext)
        {
            System.IdentityModel.Policy.AuthorizationContext authContext =
            operationContext.ServiceSecurityContext.AuthorizationContext;

            //ClaimsPrincipalPermission.CheckAccess("foo", "bar");

            var identity = GetClaimsIdentity(operationContext);

            try
            {
                TraceSource trace = new TraceSource("EdwardsIndexAppDataStoreService");
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                string address = endpoint.Address;

                trace.TraceInformation(string.Format("EdwardsIndexAppDataStoreServiceAuthorizationManager.CheckAccess() Called. Client IP = {0}", address));
            }
            catch (Exception e)
            {

            }

            if (identity.Claims == null)
                return false;

            // Doesn't make sense
            //if (identity.Claims.Count != 4)
            //    return false;

            // Because checks for specific claims are made
            bool rolesFound = false;
            bool customFound = false;

            // Check for generally required claims
            foreach (var myClaim in identity.Claims)
            {
                if (myClaim.ClaimType == "http://schemas.microsoft.com/xsi/2005/05/role_meins/role")
                {
                    rolesFound = true;
                }

                //if (myClaim.ClaimType == "MyClaimType")
                //{
                customFound = true;
                //}
            }

            if (rolesFound && customFound)
            {
                //TODO: Map actions against roles to be required from a database.
                string action = operationContext.IncomingMessageHeaders.Action;

                //operationContext.ServiceSecurityContext.AuthorizationContext.Properties["Principal"] = Thread.CurrentPrincipal;

                return true;
            }

            return false;
        }

    }

	
}
