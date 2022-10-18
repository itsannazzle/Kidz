using System.Text;
using System.Text.Json;
using Kidz.Constants;
using Kidz.DatabaseConnection;
using Kidz.Models;
using Kidz.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Kidz.Controllers
{
    [ApiController]
    public class InternalController : BaseController
    {


        string hostUrl = WebAddressConstant.STRING_SCHME_HTTPS + WebAddressConstant.STRING_SCHME_LOCALHOST + WebAddressConstant.STRING_PORT_BANGUNRUANG;

        public InternalController(DatabaseContex databaseContex) : base(databaseContex)
        {

        }

        [HttpPost]
        [Route("[controller]/validateUserToken")]
        public ResponseModel validateUserToken(RequestModel modelRequest)
        {
            ResponseModel modelResponse = new ResponseModel();
            string stringRequestData = base64Decode(modelRequest.Data);

            if(!(string.IsNullOrEmpty(stringRequestData)))
            {
                UserModel modelUserData = JsonSerializer.Deserialize<UserModel>(stringRequestData);
                UserModel modelUser = _userQuery.selectUserByToken(modelUserData);

                if( modelUser != null)
                {
                    modelResponse.ServiceResponseCode = ServiceResponseCodeConstant.STRING_RESPONSECODE_MODULE_SUCCESS;
                    modelResponse.MessageContent = StringConstant.STRING_MESSAGE_CONTENT_SUCCESS;
                    modelResponse.MessageTitle = StringConstant.STRING_MESSAGE_TITLE_SUCCESS;
                    modelResponse.Data = JsonSerializer.Serialize(modelUser);
                }
                else
                {
                    modelResponse.ServiceResponseCode = ServiceResponseCodeConstant.STRING_RESPONSECODE_MODULE_FAIL;
                    modelResponse.MessageContent = StringConstant.STRING_MESSAGE_CONTENT_FAIL;
                    modelResponse.MessageTitle = StringConstant.STRING_MESSAGE_TITLE_FAIL;
                    modelResponse.Data = JsonSerializer.Serialize(modelUser);
                }
            }
            else
            {
                modelResponse.ServiceResponseCode = ServiceResponseCodeConstant.STRING_RESPONSECODE_MODULE_SUCCESS;
                modelResponse.MessageContent = StringConstant.STRING_MESSAGE_CONTENT_JSONSERIALIZE_FAIL;
                modelResponse.MessageTitle = StringConstant.STRING_MESSAGE_TITLE_FAIL;
            }


            return modelResponse;
        }
    }
}