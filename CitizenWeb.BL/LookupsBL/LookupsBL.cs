using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CitizenWeb.DAL;
using CitizenWeb.Models;
using Newtonsoft.Json;

namespace CitizenWeb.BL
{

    /// <summary>LookupsBL.This business layer contains all the methods which are related to Setting and Getting Lookups.</summary>
    public class LookupsBL : IDisposable
    {
        private bool disposed = false;

        /// <summary>Gets the Lookups List.</summary>
        /// <returns>returns LookupsList Object.<returns>
        public List<Lookups> GetAllLookups()
        {
            Logging.LogDebugMessage("Method: GetAllLookups, MethodType: Get, Layer: LookupsBL, Parameters: No Input Parameters");
            try
            {
                using (LookupsDAL lookups = new LookupsDAL())
                {
                    return lookups.GetAllLookups();
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetAllLookups, Layer: LookupsBL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetAllLookups, Layer: LookupsBL, Stack Trace: " + ex.ToString());
                throw;
            }
        }

        /// <summary> Get Loookup By LookupId </summary>
        /// <param name="LookupId">The Integer object</param>
        /// <returns>LookupObj object</returns>
        public LookupObj GetLookupById(int LookupId)
        {
            Logging.LogDebugMessage("Method: GetLookupById, MethodType: Get, Layer: LookupsBL, Parameters: LookupId =" + LookupId.ToString());
            try
            {
                using (LookupsDAL lookups = new LookupsDAL())
                {
                    return lookups.GetLookupById(LookupId);
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetLookupById, Layer: LookupsBL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetLookupById, Layer: LookupsBL, Stack Trace: " + ex.ToString());
                throw;
            }
        }

        /// <summary> Get LoookupDetails By LookupDetailsId </summary>
        /// <param name="LookupDetailsId">The Integer object</param>
        /// <returns>LookupDetails object</returns>
        public LookupDetails GetLookupDetailsById(int LookupDetailsId)
        {
            Logging.LogDebugMessage("Method: GetLookupDetailsById, MethodType: Get, Layer: LookupsBL, Parameters: LookupDetailsId =" + LookupDetailsId.ToString());
            try
            {
                using (LookupsDAL lookups = new LookupsDAL())
                {
                    return lookups.GetLookupDetailsById(LookupDetailsId);
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetLookupDetailsById, Layer: LookupsBL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetLookupDetailsById, Layer: LookupsBL, Stack Trace: " + ex.ToString());
                throw;
            }
        }

        /// <summary> Get Lookup Details by LookupId </summary>
        /// <param name="LookupId"></param>
        /// <returns>List of LookupDetails Object</returns>
        public List<LookupDetails> GetLookupDetailsByLookupId(int LookupId)
        {
            Logging.LogDebugMessage("Method: GetLookupDetailsByLookupId, MethodType: Get, Layer: LookupsBL, Parameters: LookupId =" + LookupId.ToString());
            try
            {
                using (LookupsDAL lookups = new LookupsDAL())
                {
                    return lookups.GetLookupDetailsByLookupId(LookupId);
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetLookupDetailsByLookupId, Layer: LookupsBL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetLookupDetailsByLookupId, Layer: LookupsBL, Stack Trace: " + ex.ToString());
                throw;
            }
        }

        /// <summary> Save or Update Lookup </summary>
        /// <param name="lookup">Lookups Object</param>
        /// <returns>Lookups Object</returns>
        public Lookups SaveLookup(Lookups lookup)
        {
            Logging.LogDebugMessage("Method: SaveLookup, MethodType: Post, Layer: LookupsBL, Parameters:  lookup = " + JsonConvert.SerializeObject(lookup));
            try
            {
                using (LookupsDAL lookups = new LookupsDAL())
                {
                    return lookups.SaveLookup(lookup);
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: SaveLookup, Layer: LookupsBL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: SaveLookup, Layer: LookupsBL, Stack Trace: " + ex.ToString());
                throw;
            }
        }

        /// <summary> Save or Update Lookup Details </summary>
        /// <param name="lookupDetail">LookupDetails Object</param>
        /// <returns>LookupDetails Object</returns>
        public LookupDetails SaveLookupDetails(LookupDetails lookupDetail)
        {
            Logging.LogDebugMessage("Method: SaveLookupDetails, MethodType: Post, Layer: LookupsBL, Parameters:  lookupDetails = " + JsonConvert.SerializeObject(lookupDetail));
            try
            {
                using (LookupsDAL lookups = new LookupsDAL())
                {
                    return lookups.SaveLookupDetails(lookupDetail);
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: SaveLookupDetails, Layer: LookupsBL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: SaveLookupDetails, Layer: LookupsBL, Stack Trace: " + ex.ToString());
                throw;
            }
        }

        /// <summary>Delete Lookup </summary>
        /// <param name="deletedLookupsWithAdminUser">The DeletedLookupsWithAdminUser Object.</param>
        /// <returns>boolean value</returns>
        public bool DeleteLookup(DeletedLookupsWithAdminUser deletedLookupsWithAdminUser)
        {
            Logging.LogDebugMessage("Method: DeleteLookup, MethodType: Post, Layer: LookupsBL, Parameters:  DeletedLookupsWithAdminUser = " + JsonConvert.SerializeObject(deletedLookupsWithAdminUser));
            try
            {
                using (LookupsDAL lookupsDAL = new LookupsDAL())
                {
                    return lookupsDAL.DeleteLookup(deletedLookupsWithAdminUser);
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: DeleteLookup, Layer: LookupsBL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: DeleteLookup, Layer: LookupsBL, Stack Trace: " + ex.ToString());
                throw;
            }
        }


        /// <summary>Delete LookupDetails</summary>
        /// <param name="deletedLookupDetailsWithAdminUser">The DeletedLookupDetailsWithAdminUser Object.</param>
        /// <returns>returns Lookup Details List.</returns>
        public bool DeleteLookupDetails(DeletedLookupDetailsWithAdminUser deletedLookupDetailsWithAdminUser)
        {
            Logging.LogDebugMessage("Method: DeleteLookupDetails, MethodType: Post, Layer: LookupsBL, Parameters:  DeletedLookupDetailsWithAdminUser = " + JsonConvert.SerializeObject(deletedLookupDetailsWithAdminUser));
            try
            {
                using (LookupsDAL lookupsDAL = new LookupsDAL())
                {
                    return lookupsDAL.DeleteLookupDetails(deletedLookupDetailsWithAdminUser);
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: DeleteLookupDetails, Layer: LookupsBL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: DeleteLookupDetails, Layer: LookupsBL, Stack Trace: " + ex.ToString());
                throw;
            }
        }


        /// <summary> Get Lookup Details by comma seperated Lookup Names </summary>
        /// <param name="LookupNamesString">String Object</param>
        /// <returns>List of LookupDetailsWithLookupName Object</returns>
        public List<LookupDetailsWithLookupName> GetLookupDetailsByLookupNames(string LookupNamesString)
        {
            Logging.LogDebugMessage("Method: GetLookupDetailsByLookupNames, MethodType: Get, Layer: LookupsBL, Parameters: LookupNamesString =" + LookupNamesString);
            try
            {
                using (LookupsDAL lookups = new LookupsDAL())
                {
                    return lookups.GetLookupDetailsByLookupNames(LookupNamesString);
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetLookupDetailsByLookupNames, Layer: LookupsBL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetLookupDetailsByLookupNames, Layer: LookupsBL, Stack Trace: " + ex.ToString());
                throw;
            }
        }
        /// <summary>This method save the lookup and lookup details by designer</summary>
        /// <param name="designerLookup">The DesignerLookup Object.</param>
        /// <returns>returns Integer Object</returns>
        public int SaveLookupByDesiger(DesignerLookup designerLookup)
        {
            Logging.LogDebugMessage("Method: SaveLookupByDesiger, MethodType: Post, Layer: LookupsBL, Parameters:  DesignerLookup = " + JsonConvert.SerializeObject(designerLookup));
            try
            {
                using (LookupsDAL lookups = new LookupsDAL())
                {
                    return lookups.SaveLookupByDesiger(designerLookup);
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: SaveLookupByDesiger, Layer: LookupsBL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: SaveLookupByDesiger, Layer: LookupsBL, Stack Trace: " + ex.ToString());
                throw;
            }
        }

        /// <summary> Get Lookup Details and Categories by comma seperated Lookup Names </summary>
        /// <param name="LookupNamesString">String Object</param>
        /// <returns>The DesignerLookupsWithCategories Object</returns>
        public DesignerLookupsWithCategories GetDesignerLookupsWithCategories(string LookupNamesString)
        {
            Logging.LogDebugMessage("Method: GetDesignerLookupsWithCategories, MethodType: Get, Layer: LookupsBL, Parameters: LookupNamesString =" + LookupNamesString);
            try
            {
                using (LookupsDAL lookups = new LookupsDAL())
                {
                    return lookups.GetDesignerLookupsWithCategories(LookupNamesString);
                }
            }
            catch (SqlException sqlEx)
            {
                Logging.LogErrorMessage("Method: GetDesignerLookupsWithCategories, Layer: LookupsBL, Stack Trace: " + sqlEx.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Method: GetDesignerLookupsWithCategories, Layer: LookupsBL, Stack Trace: " + ex.ToString());
                throw;
            }
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
