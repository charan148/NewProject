using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using CitizenWeb.Models;
using Newtonsoft.Json;

namespace CitizenWeb.DAL
{

    /// <summary>LookupsDAL.This data access layer contains all the methods which are related to Setting and Getting Lookups.</summary>
    public class LookupsDAL : IDisposable
    {
        /// <summary>
        /// Gets or sets connection string.
        /// </summary>
        private string ConnectionString { get; set; }
        AppConstants appConstants = new AppConstants();
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="LookupsDAL"/> class.Gets the Lookups List.</summary>
        /// <returns>returns UsersList Object.<returns>
        public LookupsDAL()
        {
            this.ConnectionString = AppSettings.ConnectionString.ToString();
        }

        /// <summary>Gets the Lookups List.</summary>
        /// <returns>returns LookupsList Object.<returns>
        public List<Lookups> GetAllLookups()
        {
            Logging.LogDebugMessage("Method: GetAllLookups, MethodType: Get, Layer: LookupsDAL, Parameters: No Input Parameters");
            var dataSet = new DataSet();
            var lookups = new List<Lookups>();

            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.ConnectionString);
                    command.CommandText = "USP_GetAllLookups";
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {

                            lookups = EntityCollectionHelper.ConvertTo<Lookups>(dataSet.Tables[0]).ToList();

                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetAllLookups, Layer: LookupsDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetAllLookups, Layer: LookupsDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return lookups;

        }

        /// <summary> Get Loookup By LookupId </summary>
        /// <param name="LookupId">The Integer object</param>
        /// <returns>LookupObj object</returns>
        public LookupObj GetLookupById(int LookupId)
        {
            Logging.LogDebugMessage("Method: GetLookupById, MethodType: Get, Layer: LookupsDAL, Parameters: LookupId = " + LookupId.ToString());
            var dataSet = new DataSet();
            LookupObj lookupObj = new LookupObj();

            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.ConnectionString);
                    command.CommandText = "USP_GetLookupById";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupId", Value = LookupId });
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {

                            lookupObj = EntityCollectionHelper.ConvertTo<LookupObj>(dataSet.Tables[0]).FirstOrDefault();
                            lookupObj.lookupDetails = EntityCollectionHelper.ConvertTo<LookupDetails>(dataSet.Tables[1]).ToList();
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetLookupById, Layer: LookupsDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetLookupById, Layer: LookupsDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return lookupObj;
        }

        /// <summary> Get LoookupDetails By LookupDetailsId </summary>
        /// <param name="LookupDetailsId">The Integer object</param>
        /// <returns>LookupDetails object</returns>
        public LookupDetails GetLookupDetailsById(int LookupDetailsId)
        {
            Logging.LogDebugMessage("Method: GetLookupDetailsById, MethodType: Get, Layer: LookupsDAL, Parameters: LookupDetailsId = " + LookupDetailsId.ToString());
            var dataSet = new DataSet();
            LookupDetails lookupDetails = new LookupDetails();

            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.ConnectionString);
                    command.CommandText = "USP_GetLookupDetailsById";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupDetailsId", Value = LookupDetailsId });
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {
                            lookupDetails = EntityCollectionHelper.ConvertTo<LookupDetails>(dataSet.Tables[0]).FirstOrDefault();
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetLookupDetailsById, Layer: LookupsDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetLookupDetailsById, Layer: LookupsDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return lookupDetails;
        }

        /// <summary> Get Lookup Details by LookupId </summary>
        /// <param name="LookupId"></param>
        /// <returns>List of LookupDetails Object</returns>
        public List<LookupDetails> GetLookupDetailsByLookupId(int LookupId)
        {
            Logging.LogDebugMessage("Method: GetLookupDetailsById, MethodType: Get, Layer: LookupsDAL, Parameters: LookupId = " + LookupId.ToString());
            var dataSet = new DataSet();
            List<LookupDetails> lookupDetails = new List<LookupDetails>();

            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.ConnectionString);
                    command.CommandText = "USP_GetLookupDetailsByLookupId";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupId", Value = LookupId });
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {
                            lookupDetails = EntityCollectionHelper.ConvertTo<LookupDetails>(dataSet.Tables[0]).ToList();
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetLookupDetailsByLookupId, Layer: LookupsDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetLookupDetailsByLookupId, Layer: LookupsDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return lookupDetails;
        }

        /// <summary> Save or Update Lookup </summary>
        /// <param name="lookup">Lookups Object</param>
        /// <returns>Lookups Object</returns>
        public Lookups SaveLookup(Lookups lookup)
        {
            var dataset = new DataSet();
            Lookups lookups = new Lookups();
            Logging.LogDebugMessage("Method: SaveLookup, MethodType: Post, Layer: LookupsDAL, Parameters:  lookup = " + JsonConvert.SerializeObject(lookup));
            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.ConnectionString);
                    command.CommandText = "USP_SaveLookup";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupId", Value = lookup.LookupId });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupName", Value = lookup.LookupName });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupDescription", Value = lookup.LookupDescription });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupLongDescription", Value = lookup.LookupLongDescription });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupShortDescription", Value = lookup.LookupShortDescription });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupDisplayDescription", Value = lookup.LookupDisplayDescription });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@IsUsedInDisplay", Value = lookup.IsUsedInDisplay });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@EffectiveFrom", Value = lookup.EffectiveFrom });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@EffectiveTo", Value = lookup.EffectiveTo });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@CreatedByUserID", Value = lookup.CreateUserId });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RowStatus", Value = lookup.RowStatus });


                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataset);
                        if (dataset != null)
                        {
                            lookups = EntityCollectionHelper.ConvertTo<Lookups>(dataset.Tables[0]).FirstOrDefault();
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: SaveLookup, Layer: LookupsDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: SaveLookup, Layer: LookupsDAL, Stack Trace: " + ex.ToString());
                throw;
            }
            return lookups;
        }

        /// <summary> Save or Update Lookup Details </summary>
        /// <param name="lookupDetail">LookupDetails Object</param>
        /// <returns>LookupDetails Object</returns>
        public LookupDetails SaveLookupDetails(LookupDetails lookupDetail)
        {
            var dataset = new DataSet();
            LookupDetails lookupDetails = new LookupDetails();
            Logging.LogDebugMessage("Method: SaveLookupDetails, MethodType: Post, Layer: LookupsDAL, Parameters:  lookupDetail = " + JsonConvert.SerializeObject(lookupDetail));
            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.ConnectionString);
                    command.CommandText = "USP_SaveLookupDetails";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupDetailsId", Value = lookupDetail.LookupDetailsId });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupId", Value = lookupDetail.LookupId });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupDetailsDescription", Value = lookupDetail.LookupDetailsDescription });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupDetailsValue", Value = lookupDetail.LookupDetailsValue });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupDetailsLongDescription", Value = lookupDetail.LookupDetailsLongDescription });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupDetailsSequenceOrder", Value = lookupDetail.LookupDetailsSequenceOrder });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupDetailsCrossWalkValue", Value = lookupDetail.LookupDetailsCrossWalkValue });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupDetailsCategory", Value = lookupDetail.LookupDetailsCategory });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupDetailsType", Value = lookupDetail.LookupDetailsType });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupDetailsSubSequenceOrder", Value = lookupDetail.LookupDetailsSubSequenceOrder });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupDetailsSubCategory", Value = lookupDetail.LookupDetailsSubCategory });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupDetailsSubType", Value = lookupDetail.LookupDetailsSubType });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupDetailsShortDescription", Value = lookupDetail.LookupDetailsShortDescription });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupDetailsDisplayDescription", Value = lookupDetail.LookupDetailsDisplayDescription });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@IsUsedInDisplay", Value = lookupDetail.IsUsedInDisplay });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupDetailsEffectiveFrom", Value = lookupDetail.LookupDetailsEffectiveFrom });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupDetailsEffectiveTo", Value = lookupDetail.LookupDetailsEffectiveTo });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@CreatedByUserID", Value = lookupDetail.CreateUserId });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@RowStatus", Value = lookupDetail.RowStatus });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataset);
                        if (dataset != null)
                        {
                            lookupDetails = EntityCollectionHelper.ConvertTo<LookupDetails>(dataset.Tables[0]).FirstOrDefault();
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: SaveLookupDetails, Layer: LookupsDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: SaveLookupDetails, Layer: LookupsDAL, Stack Trace: " + ex.ToString());
                throw;
            }
            return lookupDetails;
        }

        /// <summary>Delete Lookup </summary>
        /// <param name="deletedLookupsWithAdminUser">The DeletedLookupsWithAdminUser Object.</param>
        /// <returns>boolean value</returns>
        public bool DeleteLookup(DeletedLookupsWithAdminUser deletedLookupsWithAdminUser)
        {
            var dataset = new DataSet();
            var response = false;
            Logging.LogDebugMessage("Method: DeleteLookup, MethodType: Post, Layer: LookupsDAL, Parameters: Parameters: DeletedLookupsWithAdminUser = " + JsonConvert.SerializeObject(deletedLookupsWithAdminUser));
            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.ConnectionString);
                    command.CommandText = "USP_DeleteLookup";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupIds", Value = deletedLookupsWithAdminUser.LookupIds });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@AdminUserID", Value = deletedLookupsWithAdminUser.AdminUserID });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataset);
                        if (dataset != null)
                        {
                            response = true;
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: DeleteLookup, Layer: LookupsDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: DeleteLookup, Layer: LookupsDAL, Stack Trace: " + ex.ToString());
                throw;
            }
            return response;
        }

        /// <summary>Delete LookupDetails</summary>
        /// <param name="deletedLookupDetailsWithAdminUser">The DeletedLookupDetailsWithAdminUser Object.</param>
        /// <returns>returns Lookup Details List.</returns>
        public bool DeleteLookupDetails(DeletedLookupDetailsWithAdminUser deletedLookupDetailsWithAdminUser)
        {
            var dataset = new DataSet();
            var response = false;
            Logging.LogDebugMessage("Method: DeleteLookupDetails, MethodType: Post, Layer: LookupsDAL, Parameters:  Parameters: DeletedLookupDetailsWithAdminUser = " + JsonConvert.SerializeObject(deletedLookupDetailsWithAdminUser));
            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.ConnectionString);
                    command.CommandText = "USP_DeleteLookupdetails";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupDetailsIds", Value = deletedLookupDetailsWithAdminUser.LookupDetailsIds });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@AdminUserID", Value = deletedLookupDetailsWithAdminUser.AdminUserID });

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataset);
                        if (dataset != null)
                        {
                            response = true;
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: DeleteLookupDetails, Layer: LookupsDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: DeleteLookupDetails, Layer: LookupsDAL, Stack Trace: " + ex.ToString());
                throw;
            }
            return response;
        }

        /// <summary> Get Lookup Details by comma seperated Lookup Names </summary>
        /// <param name="LookupNamesString">String Object</param>
        /// <returns>List of LookupDetailsWithLookupName Object</returns>
        public List<LookupDetailsWithLookupName> GetLookupDetailsByLookupNames(string LookupNamesString)
        {
            Logging.LogDebugMessage("Method: GetLookupDetailsByLookupNames, MethodType: Get, Layer: LookupsDAL, Parameters: LookupNamesString = " + LookupNamesString);
            var dataSet = new DataSet();
            List<LookupDetailsWithLookupName> lookupDetails = new List<LookupDetailsWithLookupName>();

            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.ConnectionString);
                    command.CommandText = "USP_GetLookupDetailsByLookupNames";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupNames", Value = LookupNamesString });
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {
                            lookupDetails = EntityCollectionHelper.ConvertTo<LookupDetailsWithLookupName>(dataSet.Tables[0]).ToList();
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetLookupDetailsByLookupNames, Layer: LookupsDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetLookupDetailsByLookupNames, Layer: LookupsDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return lookupDetails;
        }
        /// <summary>This method save the lookup and lookup details by designer</summary>
        /// <param name="designerLookup">The DesignerLookup Object.</param>
        /// <returns>returns Integer Object</returns>
        public int SaveLookupByDesiger(DesignerLookup designerLookup)
        {
            var dataset = new DataSet();
            int lookupId = 0;
            Logging.LogDebugMessage("Method: SaveLookupByDesiger, MethodType: Post, Layer: LookupsDAL, Parameters:  Parameters: DesignerLookup = " + JsonConvert.SerializeObject(designerLookup));
            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.ConnectionString);
                    command.CommandText = "USP_SaveLookupByDesigner";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupName", Value = designerLookup.LookupName });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupDescription", Value = designerLookup.LookupDescription });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupShortDescription", Value = designerLookup.LookupShortDescription });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@CreateUserId", Value = designerLookup.CreateUserId });
                    DataTable dataTable = new DataTable();
                    dataTable = EntityCollectionHelper.ConvertTo<DesignerLookupDetails>(designerLookup.designerLookupDetails);
                    SqlParameter structuredParamOne = new SqlParameter("@LookupDetailsType", SqlDbType.Structured);
                    structuredParamOne.Value = dataTable;
                    command.Parameters.Add(structuredParamOne);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataset);
                        if (dataset != null && dataset.Tables.Count > 0)
                        {
                            lookupId = Convert.ToInt32(dataset.Tables[0].Rows[0][0]);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: SaveLookupByDesiger, Layer: LookupsDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: SaveLookupByDesiger, Layer: LookupsDAL, Stack Trace: " + ex.ToString());
                throw;
            }
            return lookupId;
        }

        /// <summary> Get Lookup Details and Categories by comma seperated Lookup Names </summary>
        /// <param name="LookupNamesString">String Object</param>
        /// <returns>The DesignerLookupsWithCategories Object</returns>
        public DesignerLookupsWithCategories GetDesignerLookupsWithCategories(string LookupNamesString)
        {
            Logging.LogDebugMessage("Method: GetDesignerLookupsWithCategories, MethodType: Get, Layer: LookupsDAL, Parameters: LookupNamesString = " + LookupNamesString);
            var dataSet = new DataSet();
            DesignerLookupsWithCategories designerLookupsWithCategories = new DesignerLookupsWithCategories();
            List<LookupDetailsWithLookupName> lookupDetails = new List<LookupDetailsWithLookupName>();
            List<DesignerCategory> designerCategories = new List<DesignerCategory>();

            try
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(this.ConnectionString);
                    command.CommandText = "USP_GetDesignerLookupsWithCategories";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "@LookupNames", Value = LookupNamesString });
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {
                            lookupDetails = EntityCollectionHelper.ConvertTo<LookupDetailsWithLookupName>(dataSet.Tables[0]).ToList();
                        }
                        if (dataSet != null && dataSet.Tables.Count > 1)
                        {
                            designerCategories = EntityCollectionHelper.ConvertTo<DesignerCategory>(dataSet.Tables[1]).ToList();
                        }
                        designerLookupsWithCategories.lookupDetailsWithLookupNames = lookupDetails;
                        designerLookupsWithCategories.designerCategories = designerCategories;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetDesignerLookupsWithCategories, Layer: LookupsDAL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetDesignerLookupsWithCategories, Layer: LookupsDAL, Stack Trace: " + ex.ToString());
                throw;
            }

            return designerLookupsWithCategories;
        }

        /// <summary>
        /// This method disposing unmanaged code like Stream/File etc.
        /// https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.marshalbyvaluecomponent.dispose?view=netframework-4.8#System_ComponentModel_MarshalByValueComponent_Dispose
        /// </summary>
        /// <param name="disposing">Dispose Parameter is going to be only used in case to free up the unmanaged resources.</param>
        public virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            // Dispose of any unmanaged resources not wrapped in safe handles.
            this.disposed = true;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
        }
    }
}
