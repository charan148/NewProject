using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using CitizenWeb.BL;
using CitizenWeb.Models;
using CitizenWeb.DAL;

namespace CitizenWeb.Controllers
{
    /// <summary>LookupsController.This controller contains all the methods which are related to Lookups</summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/LookupsController")]
    [ApiController]
    public class LookupsController : ControllerBase
    {
        [Obsolete]
        private IHostingEnvironment _hostingEnvironment;

        [Obsolete]
        public LookupsController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>Gets the Lookups List.</summary>
        /// <returns>returns LookupsList Object.<returns>
        [Route("GetAllLookups")]
        [HttpGet]
        public List<Lookups> GetAllLookups()
        {
            Logging.LogDebugMessage("Method: GetAllLookups, MethodType: Get, Layer: LookupsController, Parameters: No Input Parameters");
            using (LookupsBL lookupsBL = new LookupsBL())
            {
                return lookupsBL.GetAllLookups();
            }
        }

        /// <summary> Get Loookup By LookupId </summary>
        /// <param name="LookupId">The Integer object</param>
        /// <returns>LookupObj object</returns>
        [Route("GetLookupById/{LookupId}")]
        [HttpGet]
        public LookupObj GetLookupById(int LookupId)
        {
            Logging.LogDebugMessage("Method: GetLookupById, MethodType: Get, Layer: LookupsController, Parameters: LookupId =" + LookupId.ToString());
            using (LookupsBL lookupsBL = new LookupsBL())
            {
                return lookupsBL.GetLookupById(LookupId);
            }
        }

        /// <summary> Get LoookupDetails By LookupDetailsId </summary>
        /// <param name="LookupDetailsId">The Integer object</param>
        /// <returns>LookupDetails object</returns>
        [Route("GetLookupDetailsById/{LookupDetailsId}")]
        [HttpGet]
        public LookupDetails GetLookupDetailsById(int LookupDetailsId)
        {
            Logging.LogDebugMessage("Method: GetLookupDetailsById, MethodType: Get, Layer: LookupsController, Parameters: LookupDetailsId =" + LookupDetailsId.ToString());
            using (LookupsBL lookupsBL = new LookupsBL())
            {
                return lookupsBL.GetLookupDetailsById(LookupDetailsId);
            }
        }

        /// <summary> Get Lookup Details by LookupId </summary>
        /// <param name="LookupId"></param>
        /// <returns>List of LookupDetails Object</returns>
        [Route("GetLookupDetailsByLookupId/{LookupId}")]
        [HttpGet]
        public List<LookupDetails> GetLookupDetailsByLookupId(int LookupId)
        {
            Logging.LogDebugMessage("Method: GetLookupDetailsByLookupId, MethodType: Get, Layer: LookupsController, Parameters: LookupId =" + LookupId.ToString());
            using (LookupsBL lookupsBL = new LookupsBL())
            {
                return lookupsBL.GetLookupDetailsByLookupId(LookupId);
            }
        }

        /// <summary> Save or Update Lookup </summary>
        /// <param name="lookup">Lookups Object</param>
        /// <returns>Lookups Object</returns>
        [Route("SaveLookup")]
        [HttpPost]
        public Lookups SaveLookup(Lookups lookups)
        {
            Logging.LogDebugMessage("Method: SaveLookup, MethodType: Post, Layer: LookupsController, Parameters:  lookup = " + JsonConvert.SerializeObject(lookups));
            using (LookupsBL lookupsBL = new LookupsBL())
            {
                return lookupsBL.SaveLookup(lookups);
            }
        }

        /// <summary> Save or Update Lookup </summary>
        /// <param name="lookup">Lookups Object</param>
        /// <returns>Lookups Object</returns>
        [Route("SaveLookupDetails")]
        [HttpPost]
        public LookupDetails SaveLookupDetails(LookupDetails lookupDetails)
        {
            Logging.LogDebugMessage("Method: SaveLookupDetails, MethodType: Post, Layer: LookupsController, Parameters:  lookupDetails = " + JsonConvert.SerializeObject(lookupDetails));
            using (LookupsBL lookupsBL = new LookupsBL())
            {
                return lookupsBL.SaveLookupDetails(lookupDetails);
            }
        }

        /// <summary>Delete Lookup </summary>
        /// <param name="deletedLookupsWithAdminUser">The DeletedLookupsWithAdminUser Object.</param>
        /// <returns>boolean value</returns>
        [Route("DeleteLookup")]
        [HttpPost]
        public bool DeleteLookup(DeletedLookupsWithAdminUser deletedLookupsWithAdminUser)
        {
            Logging.LogDebugMessage("Method: DeleteLookup, MethodType: Post, Layer: LookupsController, Parameters:  DeletedLookupsWithAdminUser = " + JsonConvert.SerializeObject(deletedLookupsWithAdminUser));

            using (LookupsBL lookupsBL = new LookupsBL())
            {
                return lookupsBL.DeleteLookup(deletedLookupsWithAdminUser);
            }

        }

        /// <summary>Delete LookupDetails</summary>
        /// <param name="deletedLookupDetailsWithAdminUser">The DeletedLookupDetailsWithAdminUser Object.</param>
        /// <returns>returns Lookup Details List.</returns>
        [Route("DeleteLookupDetails")]
        [HttpPost]
        public bool DeleteLookupDetails(DeletedLookupDetailsWithAdminUser deletedLookupDetailsWithAdminUser)
        {
            Logging.LogDebugMessage("Method: DeleteLookupDetails, MethodType: Post, Layer: LookupsController, Parameters:  Parameters:  DeletedLookupDetailsWithAdminUser = " + JsonConvert.SerializeObject(deletedLookupDetailsWithAdminUser));
            using (LookupsBL lookupsBL = new LookupsBL())
            {
                return lookupsBL.DeleteLookupDetails(deletedLookupDetailsWithAdminUser);
            }

        }


        /// <summary> Get Lookup Details by comma seperated Lookup Names </summary>
        /// <param name="LookupNamesString">String Object</param>
        /// <returns>List of LookupDetailsWithLookupName Object</returns>
        [Route("GetLookupDetailsByLookupNames/{LookupNamesString}")]
        [HttpGet]
        public List<LookupDetailsWithLookupName> GetLookupDetailsByLookupNames(string LookupNamesString)
        {
            Logging.LogDebugMessage("Method: GetLookupDetailsByLookupNames, MethodType: Get, Layer: LookupsController, Parameters: LookupNamesString =" + LookupNamesString);
            using (LookupsBL lookupsBL = new LookupsBL())
            {
                return lookupsBL.GetLookupDetailsByLookupNames(LookupNamesString);
            }
        }
        /// <summary>This method save the lookup and lookup details by designer</summary>
        /// <param name="designerLookup">The DesignerLookup Object.</param>
        /// <returns>returns Integer Object</returns>
        [Route("SaveLookupByDesiger")]
        [HttpPost]
        public int SaveLookupByDesiger(DesignerLookup designerLookup)
        {
            Logging.LogDebugMessage("Method: SaveLookupByDesiger, MethodType: Post, Layer: LookupsController, Parameters:  DesignerLookup = " + JsonConvert.SerializeObject(designerLookup));
            using (LookupsBL lookupsBL = new LookupsBL())
            {
                return lookupsBL.SaveLookupByDesiger(designerLookup);
            }
        }

        /// <summary> Get Lookup Details and Categories by comma seperated Lookup Names </summary>
        /// <param name="LookupNamesString">String Object</param>
        /// <returns>The DesignerLookupsWithCategories Object</returns>
        [Route("GetDesignerLookupsWithCategories/{LookupNamesString}")]
        [HttpGet]
        public DesignerLookupsWithCategories GetDesignerLookupsWithCategories(string LookupNamesString)
        {
            Logging.LogDebugMessage("Method: GetDesignerLookupsWithCategories, MethodType: Get, Layer: LookupsController, Parameters: LookupNamesString =" + LookupNamesString);
            using (LookupsBL lookupsBL = new LookupsBL())
            {
                return lookupsBL.GetDesignerLookupsWithCategories(LookupNamesString);
            }
        }
    }
}
