/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using System.ServiceModel.Description;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Client;



using Microsoft.Xrm.Sdk.Messages;
using System.Runtime.Serialization;
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;


namespace ContactCreatePost
{
    public class ContactCreatePost : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                Entity contact = (Entity)context.InputParameters["Target"];

                if (contact.LogicalName == "contact")
                {

                    Entity followupTask = new Entity("task");
                    followupTask["subject"] = "Make first contact with the student through E-mail.";
                    followupTask["description"] = "Welcome New Admit Student";
                    followupTask["scheduledstart"] = DateTime.Now.AddDays(7);
                    followupTask["scheduledend"] = DateTime.Now.AddDays(7);
                    followupTask["category"] = context.PrimaryEntityName;
                    //followupTask["ownerid"] = "Baradi, Erson";

                    /*EntityReference owner = new EntityReference();
                    owner.Id =((EntityReference)
                    */
                    if (context.OutputParameters.Contains("id"))
                    {

                        Guid regardingObjectId = new Guid(context.OutputParameters["id"].ToString());
                        string regardingObjectType = "contact";
                        followupTask["regardingobjectid"] = new EntityReference(regardingObjectType, regardingObjectId);
                    }

                    IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                    IOrganizationService service = factory.CreateOrganizationService(context.UserId);
                    service.Create(followupTask);
//----------------------------------------------------------------
 /*                    AssignRequest assign = new AssignRequest
                        {

                           Assignee = new EntityReference(SystemUser.EntityLogicalName, new Guid("Guid of the system account")),

                           Target = new EntityReference("entity logical name", new Guid("Guid of the user"))

                        };

                       // Execute the Request

                       orgService.Execute(assign);*/
                }
            }
        }
    }  
}
